namespace InventorBasics
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.tbx1 = new System.Windows.Forms.TextBox();
            this.tbx2 = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx3 = new System.Windows.Forms.TextBox();
            this.btn2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(57, 27);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(87, 32);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Width";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(63, 84);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(97, 32);
            this.lbl2.TabIndex = 1;
            this.lbl2.Text = "Height";
            // 
            // tbx1
            // 
            this.tbx1.Location = new System.Drawing.Point(251, 27);
            this.tbx1.Name = "tbx1";
            this.tbx1.Size = new System.Drawing.Size(332, 38);
            this.tbx1.TabIndex = 2;
            // 
            // tbx2
            // 
            this.tbx2.Location = new System.Drawing.Point(260, 84);
            this.tbx2.Name = "tbx2";
            this.tbx2.Size = new System.Drawing.Size(323, 38);
            this.tbx2.TabIndex = 3;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(158, 151);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(501, 86);
            this.btn1.TabIndex = 4;
            this.btn1.Text = "Update View";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Caption";
            // 
            // tbx3
            // 
            this.tbx3.Location = new System.Drawing.Point(164, 382);
            this.tbx3.Name = "tbx3";
            this.tbx3.Size = new System.Drawing.Size(495, 38);
            this.tbx3.TabIndex = 6;
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(158, 478);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(501, 96);
            this.btn2.TabIndex = 7;
            this.btn2.Text = "Update Caption";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 625);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.tbx3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.tbx2);
            this.Controls.Add(this.tbx1);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox tbx1;
        private System.Windows.Forms.TextBox tbx2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx3;
        private System.Windows.Forms.Button btn2;
    }
}

