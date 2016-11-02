namespace KTL
{
    partial class Form2
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
            this.colorsPanel = new System.Windows.Forms.Panel();
            this.panelNumbers = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.actualColorPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_n = new System.Windows.Forms.Label();
            this.label_k = new System.Windows.Forms.Label();
            this.label_c = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorsPanel
            // 
            this.colorsPanel.BackColor = System.Drawing.SystemColors.Window;
            this.colorsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorsPanel.Location = new System.Drawing.Point(0, 80);
            this.colorsPanel.Name = "colorsPanel";
            this.colorsPanel.Size = new System.Drawing.Size(634, 129);
            this.colorsPanel.TabIndex = 0;
            // 
            // panelNumbers
            // 
            this.panelNumbers.BackColor = System.Drawing.SystemColors.Window;
            this.panelNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumbers.Location = new System.Drawing.Point(0, 0);
            this.panelNumbers.Name = "panelNumbers";
            this.panelNumbers.Size = new System.Drawing.Size(634, 197);
            this.panelNumbers.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 80);
            this.panel1.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.label_c);
            this.panel5.Controls.Add(this.label_k);
            this.panel5.Controls.Add(this.label_n);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(193, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(441, 80);
            this.panel5.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ilość kolorów do wykorzystania";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Wygrywająca długość tęczy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ilość dostępnych liczb";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.actualColorPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 80);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wybrany kolor";
            // 
            // actualColorPanel
            // 
            this.actualColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.actualColorPanel.Location = new System.Drawing.Point(139, 18);
            this.actualColorPanel.Name = "actualColorPanel";
            this.actualColorPanel.Size = new System.Drawing.Size(40, 40);
            this.actualColorPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.colorsPanel);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(634, 209);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panelNumbers);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 209);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(634, 197);
            this.panel4.TabIndex = 4;
            // 
            // label_n
            // 
            this.label_n.AutoSize = true;
            this.label_n.Location = new System.Drawing.Point(206, 4);
            this.label_n.Name = "label_n";
            this.label_n.Size = new System.Drawing.Size(35, 13);
            this.label_n.TabIndex = 3;
            this.label_n.Text = "label6";
            // 
            // label_k
            // 
            this.label_k.AutoSize = true;
            this.label_k.Location = new System.Drawing.Point(206, 28);
            this.label_k.Name = "label_k";
            this.label_k.Size = new System.Drawing.Size(35, 13);
            this.label_k.TabIndex = 4;
            this.label_k.Text = "label7";
            // 
            // label_c
            // 
            this.label_c.AutoSize = true;
            this.label_c.Location = new System.Drawing.Point(206, 54);
            this.label_c.Name = "label_c";
            this.label_c.Size = new System.Drawing.Size(35, 13);
            this.label_c.TabIndex = 5;
            this.label_c.Text = "label8";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F);
            this.button1.Location = new System.Drawing.Point(366, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 80);
            this.button1.TabIndex = 6;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 406);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Name = "Form2";
            this.Text = "Anty-Van der Waerden ";
            this.ResizeEnd += new System.EventHandler(this.Form2_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colorsPanel;
        private System.Windows.Forms.Panel panelNumbers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel actualColorPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_c;
        private System.Windows.Forms.Label label_k;
        private System.Windows.Forms.Label label_n;
        private System.Windows.Forms.Button button1;
    }
}