namespace Lab_Solution__CommonDocFunc_CSHarp
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
            this.tbx1 = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.tbx2 = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(78, 41);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(180, 32);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Author Name";
            // 
            // tbx1
            // 
            this.tbx1.Location = new System.Drawing.Point(42, 105);
            this.tbx1.Name = "tbx1";
            this.tbx1.Size = new System.Drawing.Size(650, 38);
            this.tbx1.TabIndex = 1;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(48, 230);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(210, 32);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "Set Parameters";
            // 
            // tbx2
            // 
            this.tbx2.Location = new System.Drawing.Point(42, 293);
            this.tbx2.Name = "tbx2";
            this.tbx2.Size = new System.Drawing.Size(650, 38);
            this.tbx2.TabIndex = 3;
            this.tbx2.TextChanged += new System.EventHandler(this.tbx2_TextChanged);
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(484, 174);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(208, 57);
            this.btn1.TabIndex = 4;
            this.btn1.Text = "Create Part";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(484, 359);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(208, 79);
            this.btn2.TabIndex = 5;
            this.btn2.Text = "Update";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.tbx2);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.tbx1);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox tbx1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox tbx2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
    }
}

