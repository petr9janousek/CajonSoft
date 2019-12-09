namespace cajonConfig
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.status_label = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.connect_button = new System.Windows.Forms.Button();
            this.writeColor_button = new System.Windows.Forms.Button();
            this.readColor_button = new System.Windows.Forms.Button();
            this.writeProg_button = new System.Windows.Forms.Button();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.status_label);
            this.panel1.Controls.Add(this.progress);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(6, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 122);
            this.panel1.TabIndex = 5;
            // 
            // status_label
            // 
            this.status_label.Location = new System.Drawing.Point(4, 100);
            this.status_label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.status_label.MinimumSize = new System.Drawing.Size(102, 0);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(102, 14);
            this.status_label.TabIndex = 15;
            this.status_label.Text = "Připojte se/Nahrajte";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(111, 97);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(132, 18);
            this.progress.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Barva pravé LED:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Barva střední LED:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Barva levé LED:";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(111, 62);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(132, 21);
            this.comboBox3.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(111, 35);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(132, 21);
            this.comboBox2.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.DropDownHeight = 147;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new System.Drawing.Point(111, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::cajonConfig.Properties.Resources.jh_logo;
            this.pictureBox1.Location = new System.Drawing.Point(255, -21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 219);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // connect_button
            // 
            this.connect_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.connect_button.FlatAppearance.BorderSize = 0;
            this.connect_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connect_button.Image = global::cajonConfig.Properties.Resources.toggle_1;
            this.connect_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connect_button.Location = new System.Drawing.Point(3, 3);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(87, 29);
            this.connect_button.TabIndex = 3;
            this.connect_button.Text = "Odpojeno";
            this.connect_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.connect_button, "Připojí odpojí program ke cajonu");
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // writeColor_button
            // 
            this.writeColor_button.FlatAppearance.BorderSize = 0;
            this.writeColor_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.writeColor_button.Image = global::cajonConfig.Properties.Resources.upload;
            this.writeColor_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.writeColor_button.Location = new System.Drawing.Point(209, 3);
            this.writeColor_button.Name = "writeColor_button";
            this.writeColor_button.Size = new System.Drawing.Size(102, 29);
            this.writeColor_button.TabIndex = 5;
            this.writeColor_button.Text = "Uložit barvy";
            this.writeColor_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.writeColor_button, "Nahraje nové barvy do cajonu");
            this.writeColor_button.UseVisualStyleBackColor = true;
            this.writeColor_button.Click += new System.EventHandler(this.writeColor_button_Click);
            // 
            // readColor_button
            // 
            this.readColor_button.FlatAppearance.BorderSize = 0;
            this.readColor_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.readColor_button.Image = global::cajonConfig.Properties.Resources.download;
            this.readColor_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.readColor_button.Location = new System.Drawing.Point(91, 3);
            this.readColor_button.Name = "readColor_button";
            this.readColor_button.Size = new System.Drawing.Size(117, 29);
            this.readColor_button.TabIndex = 4;
            this.readColor_button.Text = "Stáhnout barvy";
            this.readColor_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.readColor_button, "Obdrží barvy, které jsou v cajonu ");
            this.readColor_button.UseVisualStyleBackColor = true;
            this.readColor_button.Click += new System.EventHandler(this.readColor_button_Click);
            // 
            // writeProg_button
            // 
            this.writeProg_button.FlatAppearance.BorderSize = 0;
            this.writeProg_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.writeProg_button.Image = global::cajonConfig.Properties.Resources.download;
            this.writeProg_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.writeProg_button.Location = new System.Drawing.Point(312, 3);
            this.writeProg_button.Name = "writeProg_button";
            this.writeProg_button.Size = new System.Drawing.Size(118, 29);
            this.writeProg_button.TabIndex = 6;
            this.writeProg_button.Text = "Nahrát program";
            this.writeProg_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.writeProg_button, "Nahraje do nového cajonu program (NEJDŘÍVE VYBERTE PORT)");
            this.writeProg_button.UseVisualStyleBackColor = true;
            this.writeProg_button.Click += new System.EventHandler(this.writeProg_button_Click);
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(433, 8);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(59, 21);
            this.comboBox4.TabIndex = 7;
            this.toolTip1.SetToolTip(this.comboBox4, "Vyberte port zařízení do kterého chcete nahrát program");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.comboBox4);
            this.panel2.Controls.Add(this.writeProg_button);
            this.panel2.Controls.Add(this.connect_button);
            this.panel2.Controls.Add(this.writeColor_button);
            this.panel2.Controls.Add(this.readColor_button);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 37);
            this.panel2.TabIndex = 6;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(504, 171);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cajon config";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button writeProg_button;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button writeColor_button;
        private System.Windows.Forms.Button readColor_button;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.ProgressBar progress;
    }
}

