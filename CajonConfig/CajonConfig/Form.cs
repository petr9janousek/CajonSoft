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

            SetCombo(comboBox1);
            SetCombo(comboBox2);
            SetCombo(comboBox3);
            cajon = new Device();

            //buttonsHover();
            if (IntPtr.Size == 8)
                this.Text += " [x64]";
        }

        void SetCombo(ComboBox c)
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

            c.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            c.DataSource = list;
            c.ValueMember = "Color";
            c.DisplayMember = "Text";
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

                ComboColorItem item = ((ComboBox)sender).Items[e.Index] as ComboColorItem;
                Font f = this.Font;
                Brush b = new SolidBrush(item.Color);
                g.DrawString(item.Text, f, Brushes.Black, rect.X + 14, rect.Top);
                g.FillRectangle(b, rect.X + 2, rect.Y + 2, 10, 10);
                g.DrawRectangle(new Pen(Color.Black), rect.X + 2, rect.Y + 2, 10, 10);
                if (item.Color == Color.Empty)
                {
                    g.DrawLine(new Pen(Color.Black, 2), rect.X + 2, rect.Y + 2, rect.X + 11, rect.Y + 11);
                }

            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ConnectAsync();
        }

        private void connect_button_Click(object sender, EventArgs e)
        {
            ConnectAsync();
        }

        private void upload_button_Click(object sender, EventArgs e)
        {
            UploadAsync();
        }

        private void download_button_Click(object sender, EventArgs e)
        {
            DownloadAsync();
        }

        private async void ConnectAsync()
        {
            progress.Style = ProgressBarStyle.Marquee;
            status_label.Text = "Připojuji...";
            try
            {
                await Task.Run(() =>
                {
                    cajon.Connect();
                });
            }
            catch (Exception e)
            {
                string message = "Cajon nebyl nalezen mezi připojenými zařízeními." +
                                 "\nZkontrolujte, že cajon je zapojen správně a zkuste to znovu." +
                                 "\nPOZOR: Po připojení vyčkejte alespoň 10 sekund.";
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

        private async void DownloadAsync()
        {
            if (cajon.ValidConnection)
            {
                progress.Style = ProgressBarStyle.Marquee;
                status_label.Text = "Stahuji data...";
                try
                {
                    await Task.Run(() =>
                    {
                        cajon.Download();
                    });
                }
                catch (TimeoutException t)
                {
                    string message = "Nepodařilo se potvrdit přijetí zprávy, zkuste to znovu" +
                                     "\nPOZOR: Pokud tuto zprávu nevidíte poprve, zkuste zařízení odpojit a znovu připojit";
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

        private async void UploadAsync()
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
                        cajon.Upload();
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
    }

    public class ComboColorItem
    {
        public Color Color { get; set; }
        public string Text { get; set; }
    }


}
