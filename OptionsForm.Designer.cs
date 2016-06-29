namespace AutoCollage
{
    partial class PreferencesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.customBorderWidthBox = new System.Windows.Forms.NumericUpDown();
            this.borderColorLabel = new System.Windows.Forms.Label();
            this.borderColorButton = new System.Windows.Forms.Button();
            this.borderPx = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.borderCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pixelSize = new System.Windows.Forms.Label();
            this.customSizeBox = new System.Windows.Forms.NumericUpDown();
            this.sizePx = new System.Windows.Forms.Label();
            this.customSizeRadio = new System.Windows.Forms.RadioButton();
            this.extraLargeRadio = new System.Windows.Forms.RadioButton();
            this.largeRadio = new System.Windows.Forms.RadioButton();
            this.mediumRadio = new System.Windows.Forms.RadioButton();
            this.smallRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.customCountBox = new System.Windows.Forms.NumericUpDown();
            this.customCountRadio = new System.Windows.Forms.RadioButton();
            this.fiftyRadio = new System.Windows.Forms.RadioButton();
            this.twentyRadio = new System.Windows.Forms.RadioButton();
            this.tenRadio = new System.Windows.Forms.RadioButton();
            this.fiveRadio = new System.Windows.Forms.RadioButton();
            this.oneRadio = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.orientationBothRadio = new System.Windows.Forms.RadioButton();
            this.orientationHRadio = new System.Windows.Forms.RadioButton();
            this.orientationVRadio = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customBorderWidthBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customSizeBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customCountBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.27624F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.72376F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(293, 389);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.customBorderWidthBox);
            this.groupBox3.Controls.Add(this.borderColorLabel);
            this.groupBox3.Controls.Add(this.borderColorButton);
            this.groupBox3.Controls.Add(this.borderPx);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.borderCheckBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(287, 64);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Border Style";
            // 
            // customBorderWidthBox
            // 
            this.customBorderWidthBox.Enabled = false;
            this.customBorderWidthBox.Location = new System.Drawing.Point(108, 27);
            this.customBorderWidthBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.customBorderWidthBox.Name = "customBorderWidthBox";
            this.customBorderWidthBox.Size = new System.Drawing.Size(38, 20);
            this.customBorderWidthBox.TabIndex = 6;
            // 
            // borderColorLabel
            // 
            this.borderColorLabel.AutoSize = true;
            this.borderColorLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.borderColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.borderColorLabel.Enabled = false;
            this.borderColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.borderColorLabel.Location = new System.Drawing.Point(225, 25);
            this.borderColorLabel.Name = "borderColorLabel";
            this.borderColorLabel.Size = new System.Drawing.Size(35, 22);
            this.borderColorLabel.TabIndex = 5;
            this.borderColorLabel.Text = "      ";
            // 
            // borderColorButton
            // 
            this.borderColorButton.Enabled = false;
            this.borderColorButton.Location = new System.Drawing.Point(176, 25);
            this.borderColorButton.Name = "borderColorButton";
            this.borderColorButton.Size = new System.Drawing.Size(43, 23);
            this.borderColorButton.TabIndex = 4;
            this.borderColorButton.Text = "Color";
            this.borderColorButton.UseVisualStyleBackColor = true;
            this.borderColorButton.Click += new System.EventHandler(this.borderColorButton_Click);
            // 
            // borderPx
            // 
            this.borderPx.AutoSize = true;
            this.borderPx.Enabled = false;
            this.borderPx.Location = new System.Drawing.Point(143, 30);
            this.borderPx.Name = "borderPx";
            this.borderPx.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.borderPx.Size = new System.Drawing.Size(21, 13);
            this.borderPx.TabIndex = 3;
            this.borderPx.Text = "px";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width:";
            // 
            // borderCheckBox
            // 
            this.borderCheckBox.AutoSize = true;
            this.borderCheckBox.Location = new System.Drawing.Point(10, 29);
            this.borderCheckBox.Name = "borderCheckBox";
            this.borderCheckBox.Size = new System.Drawing.Size(57, 17);
            this.borderCheckBox.TabIndex = 0;
            this.borderCheckBox.Text = "Border";
            this.borderCheckBox.UseVisualStyleBackColor = true;
            this.borderCheckBox.CheckedChanged += new System.EventHandler(this.borderCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pixelSize);
            this.groupBox1.Controls.Add(this.customSizeBox);
            this.groupBox1.Controls.Add(this.sizePx);
            this.groupBox1.Controls.Add(this.customSizeRadio);
            this.groupBox1.Controls.Add(this.extraLargeRadio);
            this.groupBox1.Controls.Add(this.largeRadio);
            this.groupBox1.Controls.Add(this.mediumRadio);
            this.groupBox1.Controls.Add(this.smallRadio);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // pixelSize
            // 
            this.pixelSize.AutoSize = true;
            this.pixelSize.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pixelSize.Location = new System.Drawing.Point(189, 62);
            this.pixelSize.Name = "pixelSize";
            this.pixelSize.Size = new System.Drawing.Size(63, 15);
            this.pixelSize.TabIndex = 8;
            this.pixelSize.Text = "(1000px)";
            // 
            // customSizeBox
            // 
            this.customSizeBox.Enabled = false;
            this.customSizeBox.Location = new System.Drawing.Point(76, 55);
            this.customSizeBox.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.customSizeBox.Name = "customSizeBox";
            this.customSizeBox.Size = new System.Drawing.Size(52, 20);
            this.customSizeBox.TabIndex = 7;
            this.customSizeBox.ValueChanged += new System.EventHandler(this.customSizeBox_ValueChanged);
            // 
            // sizePx
            // 
            this.sizePx.AutoSize = true;
            this.sizePx.Enabled = false;
            this.sizePx.Location = new System.Drawing.Point(128, 59);
            this.sizePx.Name = "sizePx";
            this.sizePx.Size = new System.Drawing.Size(18, 13);
            this.sizePx.TabIndex = 6;
            this.sizePx.Text = "px";
            // 
            // customSizeRadio
            // 
            this.customSizeRadio.AutoSize = true;
            this.customSizeRadio.Location = new System.Drawing.Point(10, 55);
            this.customSizeRadio.Name = "customSizeRadio";
            this.customSizeRadio.Size = new System.Drawing.Size(60, 17);
            this.customSizeRadio.TabIndex = 4;
            this.customSizeRadio.Text = "Custom";
            this.customSizeRadio.UseVisualStyleBackColor = true;
            this.customSizeRadio.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // extraLargeRadio
            // 
            this.extraLargeRadio.AutoSize = true;
            this.extraLargeRadio.Location = new System.Drawing.Point(192, 19);
            this.extraLargeRadio.Name = "extraLargeRadio";
            this.extraLargeRadio.Size = new System.Drawing.Size(79, 17);
            this.extraLargeRadio.TabIndex = 3;
            this.extraLargeRadio.Text = "Extra Large";
            this.extraLargeRadio.UseVisualStyleBackColor = true;
            this.extraLargeRadio.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // largeRadio
            // 
            this.largeRadio.AutoSize = true;
            this.largeRadio.Location = new System.Drawing.Point(134, 19);
            this.largeRadio.Name = "largeRadio";
            this.largeRadio.Size = new System.Drawing.Size(52, 17);
            this.largeRadio.TabIndex = 2;
            this.largeRadio.Text = "Large";
            this.largeRadio.UseVisualStyleBackColor = true;
            this.largeRadio.CheckedChanged += new System.EventHandler(this.largeRadio_CheckedChanged);
            // 
            // mediumRadio
            // 
            this.mediumRadio.AutoSize = true;
            this.mediumRadio.Checked = true;
            this.mediumRadio.Location = new System.Drawing.Point(66, 19);
            this.mediumRadio.Name = "mediumRadio";
            this.mediumRadio.Size = new System.Drawing.Size(62, 17);
            this.mediumRadio.TabIndex = 1;
            this.mediumRadio.TabStop = true;
            this.mediumRadio.Text = "Medium";
            this.mediumRadio.UseVisualStyleBackColor = true;
            this.mediumRadio.CheckedChanged += new System.EventHandler(this.mediumRadio_CheckedChanged);
            // 
            // smallRadio
            // 
            this.smallRadio.AutoSize = true;
            this.smallRadio.Location = new System.Drawing.Point(10, 20);
            this.smallRadio.Name = "smallRadio";
            this.smallRadio.Size = new System.Drawing.Size(50, 17);
            this.smallRadio.TabIndex = 0;
            this.smallRadio.Text = "Small";
            this.smallRadio.UseVisualStyleBackColor = true;
            this.smallRadio.CheckedChanged += new System.EventHandler(this.smallRadio_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.customCountBox);
            this.groupBox2.Controls.Add(this.customCountRadio);
            this.groupBox2.Controls.Add(this.fiftyRadio);
            this.groupBox2.Controls.Add(this.twentyRadio);
            this.groupBox2.Controls.Add(this.tenRadio);
            this.groupBox2.Controls.Add(this.fiveRadio);
            this.groupBox2.Controls.Add(this.oneRadio);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Count";
            // 
            // customCountBox
            // 
            this.customCountBox.Enabled = false;
            this.customCountBox.Location = new System.Drawing.Point(74, 56);
            this.customCountBox.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.customCountBox.Name = "customCountBox";
            this.customCountBox.Size = new System.Drawing.Size(54, 20);
            this.customCountBox.TabIndex = 7;
            // 
            // customCountRadio
            // 
            this.customCountRadio.AutoSize = true;
            this.customCountRadio.Location = new System.Drawing.Point(7, 56);
            this.customCountRadio.Name = "customCountRadio";
            this.customCountRadio.Size = new System.Drawing.Size(60, 17);
            this.customCountRadio.TabIndex = 6;
            this.customCountRadio.Text = "Custom";
            this.customCountRadio.UseVisualStyleBackColor = true;
            this.customCountRadio.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // fiftyRadio
            // 
            this.fiftyRadio.AutoSize = true;
            this.fiftyRadio.Location = new System.Drawing.Point(167, 20);
            this.fiftyRadio.Name = "fiftyRadio";
            this.fiftyRadio.Size = new System.Drawing.Size(37, 17);
            this.fiftyRadio.TabIndex = 4;
            this.fiftyRadio.Text = "50";
            this.fiftyRadio.UseVisualStyleBackColor = true;
            this.fiftyRadio.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // twentyRadio
            // 
            this.twentyRadio.AutoSize = true;
            this.twentyRadio.Location = new System.Drawing.Point(124, 20);
            this.twentyRadio.Name = "twentyRadio";
            this.twentyRadio.Size = new System.Drawing.Size(37, 17);
            this.twentyRadio.TabIndex = 3;
            this.twentyRadio.Text = "20";
            this.twentyRadio.UseVisualStyleBackColor = true;
            this.twentyRadio.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // tenRadio
            // 
            this.tenRadio.AutoSize = true;
            this.tenRadio.Location = new System.Drawing.Point(81, 19);
            this.tenRadio.Name = "tenRadio";
            this.tenRadio.Size = new System.Drawing.Size(37, 17);
            this.tenRadio.TabIndex = 2;
            this.tenRadio.Text = "10";
            this.tenRadio.UseVisualStyleBackColor = true;
            this.tenRadio.CheckedChanged += new System.EventHandler(this.tenRadio_CheckedChanged);
            // 
            // fiveRadio
            // 
            this.fiveRadio.AutoSize = true;
            this.fiveRadio.Checked = true;
            this.fiveRadio.Location = new System.Drawing.Point(44, 19);
            this.fiveRadio.Name = "fiveRadio";
            this.fiveRadio.Size = new System.Drawing.Size(31, 17);
            this.fiveRadio.TabIndex = 1;
            this.fiveRadio.TabStop = true;
            this.fiveRadio.Text = "5";
            this.fiveRadio.UseVisualStyleBackColor = true;
            this.fiveRadio.CheckedChanged += new System.EventHandler(this.fiveRadio_CheckedChanged);
            // 
            // oneRadio
            // 
            this.oneRadio.AutoSize = true;
            this.oneRadio.Location = new System.Drawing.Point(7, 20);
            this.oneRadio.Name = "oneRadio";
            this.oneRadio.Size = new System.Drawing.Size(31, 17);
            this.oneRadio.TabIndex = 0;
            this.oneRadio.Text = "1";
            this.oneRadio.UseVisualStyleBackColor = true;
            this.oneRadio.CheckedChanged += new System.EventHandler(this.oneRadio_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(3, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(287, 63);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.flowLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 57);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Orientation";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.orientationBothRadio);
            this.flowLayoutPanel1.Controls.Add(this.orientationHRadio);
            this.flowLayoutPanel1.Controls.Add(this.orientationVRadio);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(281, 38);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // orientationBothRadio
            // 
            this.orientationBothRadio.AutoSize = true;
            this.orientationBothRadio.Checked = true;
            this.orientationBothRadio.Location = new System.Drawing.Point(6, 6);
            this.orientationBothRadio.Name = "orientationBothRadio";
            this.orientationBothRadio.Size = new System.Drawing.Size(47, 17);
            this.orientationBothRadio.TabIndex = 0;
            this.orientationBothRadio.TabStop = true;
            this.orientationBothRadio.Text = "Both";
            this.orientationBothRadio.UseVisualStyleBackColor = true;
            this.orientationBothRadio.CheckedChanged += new System.EventHandler(this.orientationBothRadio_CheckedChanged);
            // 
            // orientationHRadio
            // 
            this.orientationHRadio.AutoSize = true;
            this.orientationHRadio.Location = new System.Drawing.Point(59, 6);
            this.orientationHRadio.Name = "orientationHRadio";
            this.orientationHRadio.Size = new System.Drawing.Size(72, 17);
            this.orientationHRadio.TabIndex = 1;
            this.orientationHRadio.Text = "Horizontal";
            this.orientationHRadio.UseVisualStyleBackColor = true;
            this.orientationHRadio.CheckedChanged += new System.EventHandler(this.orientationHRadio_CheckedChanged);
            // 
            // orientationVRadio
            // 
            this.orientationVRadio.AutoSize = true;
            this.orientationVRadio.Location = new System.Drawing.Point(137, 6);
            this.orientationVRadio.Name = "orientationVRadio";
            this.orientationVRadio.Size = new System.Drawing.Size(60, 17);
            this.orientationVRadio.TabIndex = 2;
            this.orientationVRadio.Text = "Vertical";
            this.orientationVRadio.UseVisualStyleBackColor = true;
            this.orientationVRadio.CheckedChanged += new System.EventHandler(this.orientationVRadio_CheckedChanged);
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 389);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.Text = "AutoCollage - Preferences";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customBorderWidthBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customSizeBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customCountBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton customSizeRadio;
        private System.Windows.Forms.RadioButton extraLargeRadio;
        private System.Windows.Forms.RadioButton largeRadio;
        private System.Windows.Forms.RadioButton mediumRadio;
        private System.Windows.Forms.RadioButton smallRadio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton fiftyRadio;
        private System.Windows.Forms.RadioButton twentyRadio;
        private System.Windows.Forms.RadioButton tenRadio;
        private System.Windows.Forms.RadioButton fiveRadio;
        private System.Windows.Forms.RadioButton oneRadio;
        private System.Windows.Forms.RadioButton customCountRadio;
        private System.Windows.Forms.Label borderColorLabel;
        private System.Windows.Forms.Button borderColorButton;
        private System.Windows.Forms.Label borderPx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox borderCheckBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label sizePx;
        private System.Windows.Forms.NumericUpDown customBorderWidthBox;
        private System.Windows.Forms.NumericUpDown customSizeBox;
        private System.Windows.Forms.NumericUpDown customCountBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton orientationBothRadio;
        private System.Windows.Forms.RadioButton orientationHRadio;
        private System.Windows.Forms.RadioButton orientationVRadio;
        private System.Windows.Forms.Label pixelSize;
    }
}