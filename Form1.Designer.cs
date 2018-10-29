namespace _3D_Cube
{
    partial class Form1
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
            this.BoxTest = new System.Windows.Forms.Timer(this.components);
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AutoXTick = new System.Windows.Forms.CheckBox();
            this.XNum = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AutoYTick = new System.Windows.Forms.CheckBox();
            this.YNum = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AutoZTick = new System.Windows.Forms.CheckBox();
            this.ZNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FPSLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CubeButton = new System.Windows.Forms.Button();
            this.PyramidButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BoxTest
            // 
            this.BoxTest.Enabled = true;
            this.BoxTest.Interval = 10;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(71, 0);
            this.hScrollBar1.Maximum = 628;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(815, 54);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(71, 2);
            this.hScrollBar2.Maximum = 628;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(815, 55);
            this.hScrollBar2.TabIndex = 1;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar2_Scroll);
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Location = new System.Drawing.Point(71, 0);
            this.hScrollBar3.Maximum = 628;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(815, 54);
            this.hScrollBar3.TabIndex = 2;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar3_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rotate X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Rotate Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Rotate Z";
            // 
            // AutoXTick
            // 
            this.AutoXTick.AutoSize = true;
            this.AutoXTick.Location = new System.Drawing.Point(6, 19);
            this.AutoXTick.Name = "AutoXTick";
            this.AutoXTick.Size = new System.Drawing.Size(93, 17);
            this.AutoXTick.TabIndex = 9;
            this.AutoXTick.Text = "Auto Rotate X";
            this.AutoXTick.UseVisualStyleBackColor = true;
            // 
            // XNum
            // 
            this.XNum.Location = new System.Drawing.Point(105, 16);
            this.XNum.Name = "XNum";
            this.XNum.Size = new System.Drawing.Size(52, 20);
            this.XNum.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AutoXTick);
            this.groupBox1.Controls.Add(this.XNum);
            this.groupBox1.Location = new System.Drawing.Point(15, 632);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 46);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AutoYTick);
            this.groupBox2.Controls.Add(this.YNum);
            this.groupBox2.Location = new System.Drawing.Point(230, 632);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 46);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // AutoYTick
            // 
            this.AutoYTick.AutoSize = true;
            this.AutoYTick.Location = new System.Drawing.Point(6, 19);
            this.AutoYTick.Name = "AutoYTick";
            this.AutoYTick.Size = new System.Drawing.Size(93, 17);
            this.AutoYTick.TabIndex = 9;
            this.AutoYTick.Text = "Auto Rotate Y";
            this.AutoYTick.UseVisualStyleBackColor = true;
            // 
            // YNum
            // 
            this.YNum.Location = new System.Drawing.Point(105, 16);
            this.YNum.Name = "YNum";
            this.YNum.Size = new System.Drawing.Size(52, 20);
            this.YNum.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AutoZTick);
            this.groupBox3.Controls.Add(this.ZNum);
            this.groupBox3.Location = new System.Drawing.Point(445, 632);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(173, 46);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // AutoZTick
            // 
            this.AutoZTick.AutoSize = true;
            this.AutoZTick.Location = new System.Drawing.Point(6, 19);
            this.AutoZTick.Name = "AutoZTick";
            this.AutoZTick.Size = new System.Drawing.Size(93, 17);
            this.AutoZTick.TabIndex = 9;
            this.AutoZTick.Text = "Auto Rotate Z";
            this.AutoZTick.UseVisualStyleBackColor = true;
            // 
            // ZNum
            // 
            this.ZNum.Location = new System.Drawing.Point(105, 16);
            this.ZNum.Name = "ZNum";
            this.ZNum.Size = new System.Drawing.Size(52, 20);
            this.ZNum.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 604);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Input a number between 1 and 100";
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPSLabel.Location = new System.Drawing.Point(11, 9);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(57, 20);
            this.FPSLabel.TabIndex = 14;
            this.FPSLabel.Text = "FPS: 0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.hScrollBar3);
            this.panel1.Location = new System.Drawing.Point(12, 700);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 15);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.hScrollBar2);
            this.panel2.Location = new System.Drawing.Point(12, 716);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(893, 15);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.hScrollBar1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 739);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(893, 15);
            this.panel3.TabIndex = 17;
            // 
            // CubeButton
            // 
            this.CubeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CubeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CubeButton.Location = new System.Drawing.Point(818, 12);
            this.CubeButton.Name = "CubeButton";
            this.CubeButton.Size = new System.Drawing.Size(80, 28);
            this.CubeButton.TabIndex = 18;
            this.CubeButton.Text = "Cube";
            this.CubeButton.UseVisualStyleBackColor = true;
            this.CubeButton.Click += new System.EventHandler(this.CubeButton_Click);
            // 
            // PyramidButton
            // 
            this.PyramidButton.Enabled = false;
            this.PyramidButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PyramidButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PyramidButton.Location = new System.Drawing.Point(818, 46);
            this.PyramidButton.Name = "PyramidButton";
            this.PyramidButton.Size = new System.Drawing.Size(80, 28);
            this.PyramidButton.TabIndex = 19;
            this.PyramidButton.Text = "Pyramid";
            this.PyramidButton.UseVisualStyleBackColor = true;
            this.PyramidButton.Click += new System.EventHandler(this.PyramidButton_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(818, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 43);
            this.button2.TabIndex = 20;
            this.button2.Text = "Square Pyramid";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(911, 761);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PyramidButton);
            this.Controls.Add(this.CubeButton);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.FPSLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer BoxTest;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox AutoXTick;
        private System.Windows.Forms.TextBox XNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox AutoYTick;
        private System.Windows.Forms.TextBox YNum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox AutoZTick;
        private System.Windows.Forms.TextBox ZNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label FPSLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button CubeButton;
        private System.Windows.Forms.Button PyramidButton;
        private System.Windows.Forms.Button button2;
    }
}

