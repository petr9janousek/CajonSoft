using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace cajonConfig
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Device cajon;

        public Form()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            cajon = new Device();

            SetCombo(comboBox1, true);
            SetCombo(comboBox2, true);
            SetCombo(comboBox3, true);
            SetCombo(comboBox4, false);

            //buttonsHover();
            if (IntPtr.Size == 8)
                this.Text += " [x64]";
        }

        void SetCombo(ComboBox c, bool addColors)
        {
            if (addColors)
            {
                BindingList<ComboColorItem> list = new BindingList<ComboColorItem>();
                list.Add(new ComboColorItem()
                {
                    Color = Color.Empty,
                    Text = "Nevybráno"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Red,
                    Text = "Červená"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Green,
                    Text = "Zelená"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Blue,
                    Text = "Modrá"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Cyan,
                    Text = "Tyrkysová"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Magenta,
                    Text = "Fialová"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.Yellow,
                    Text = "Žlutá"
                });
                list.Add(new ComboColorItem()
                {
                    Color = Color.White,
                    Text = "Bílá"
                });
                c.DataSource = list;
                c.ValueMember = "Color";
                c.DisplayMember = "Text";
            }
            else
            {
                c.DataSource = cajon.availablePorts;
            }
            c.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            c.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                Color backColor = Color.White;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    g.FillRectangle(new SolidBrush(SystemColors.ControlLight), rect);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.White), rect);
                }
                if (((ComboBox)sender).Items[e.Index] is ComboColorItem)
                {
                    ComboColorItem item = ((ComboBox)sender).Items[e.Index] as ComboColorItem;
                    Font f = this.Font;
                    Brush b = new SolidBrush(item.Color);
                    g.FillRectangle(b, rect.X + 2, rect.Y + 2, 10, 10);
                    g.DrawRectangle(new Pen(Color.Black), rect.X + 2, rect.Y + 2, 10, 10);
                    g.DrawString(item.Text, f, Brushes.Black, rect.X + 14, rect.Top);
                    if (item.Color == Color.Empty)
                    {
                        g.DrawLine(new Pen(Color.Black, 2), rect.X + 2, rect.Y + 2, rect.X + 11, rect.Y + 11);
                    }
                }
                else
                {
                    string item = ((ComboBox)sender).Items[e.Index] as string;
                    Font f = this.Font;
                    g.DrawString(item, f, Brushes.Black, rect.X, rect.Top);
                }


            }
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            ConnectAsync();
        }

        private void writeColor_button_Click(object sender, EventArgs e)
        {
            writeColorAsync();
        }

        private void readColor_button_Click(object sender, EventArgs e)
        {
            readColorAsync();
        }

        private void writeProg_button_Click(object sender, EventArgs e)
        {
            writeProgAsync();
        }

        private async void ConnectAsync()
        {
            progress.Style = ProgressBarStyle.Marquee;
            status_label.Text = "Připojuji...";
            try
            {
                await Task.Run(() =>
                {
                    cajon.Connect(true);
                });
            }
            catch (Exception e)
            {
                string message = "Cajon se nepodařilo připojit. Zkuste to znovu" +
                                 "\nPOZOR: Připojit se jde pouze ke cajonu, do kterého již byl nahrán program.";
                MessageBox.Show(message, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cajon.ValidConnection)
            {
                status_label.Text = "Připojeno";
                connect_button.Text = "Připojeno";
                connect_button.Image = cajonConfig.Properties.Resources.toggle_2;
            }
            else
            {
                status_label.Text = "Odpojeno";
                connect_button.Text = "Odpojeno";
                connect_button.Image = cajonConfig.Properties.Resources.toggle_1;
            }
            progress.Style = ProgressBarStyle.Blocks;
        }

        private async void readColorAsync()
        {
            if (cajon.ValidConnection)
            {
                progress.Style = ProgressBarStyle.Marquee;
                status_label.Text = "Stahuji data...";
                try
                {
                    await Task.Run(() =>
                    {
                        cajon.readColor();
                    });
                }
                catch (TimeoutException t)
                {
                    string message = "Nepodařilo se potvrdit přijetí zprávy, zkuste to znovu" +
                                     "\nPOZOR: Pokud tuto zprávu nevidíte poprve, zkuste zařízení odpojit a znovu připojit.";
                    MessageBox.Show(message, t.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                comboBox1.SelectedIndex = cajon.Colors[0];
                comboBox2.SelectedIndex = cajon.Colors[1];
                comboBox3.SelectedIndex = cajon.Colors[2];

                status_label.Text = "Staženo";
                progress.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                status_label.Text = "Cajon není připojen";
            }
        }

        private async void writeColorAsync()
        {
            if (cajon.ValidConnection)
            {
                progress.Style = ProgressBarStyle.Marquee;
                status_label.Text = "Nahrávám data...";

                cajon.Colors[0] = Convert.ToByte(comboBox1.SelectedIndex);
                cajon.Colors[1] = Convert.ToByte(comboBox2.SelectedIndex);
                cajon.Colors[2] = Convert.ToByte(comboBox3.SelectedIndex);
                try
                {
                    await Task.Run(() =>
                    {
                        cajon.writeColor();
                    });
                }
                catch (TimeoutException t)
                {
                    string message = "Cajon nepotvrdil přijetí zprávy, zkuste to znovu" +
                                     "\nPOZOR: Pokud tuto zprávu nevidíte poprve, zkuste zařízení odpojit a znovu připojit";
                    MessageBox.Show(message, t.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                status_label.Text = "Nahráno";
                progress.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                status_label.Text = "Cajon není připojen";
            }
        }

        private async void writeProgAsync()
        {
            progress.Style = ProgressBarStyle.Marquee;
            status_label.Text = "Nahrávám program...";

            if (cajon.ValidConnection)
            {
                cajon.Connect(false);
                connect_button.Image = cajonConfig.Properties.Resources.toggle_1;
            }
            
            try
            {
                await Task.Run(() =>
                {
                    cajon.writeProgram();
                });
            }
            catch (InvalidOperationException s)
            {
                string message = "Pro nahrání programu musí být program ve stavu odpojeno";
                MessageBox.Show(message, s.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            status_label.Text = "Nahráno";
            progress.Style = ProgressBarStyle.Blocks;

        }
    }

    public class ComboColorItem
    {
        public Color Color { get; set; }
        public string Text { get; set; }
    }


}
