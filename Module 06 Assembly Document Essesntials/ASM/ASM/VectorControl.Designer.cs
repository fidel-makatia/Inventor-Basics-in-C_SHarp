namespace ASM
{
    partial class VectorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxx = new System.Windows.Forms.TextBox();
            this.textBoxy = new System.Windows.Forms.TextBox();
            this.textBoxz = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // textBoxx
            // 
            this.textBoxx.Location = new System.Drawing.Point(30, 97);
            this.textBoxx.Name = "textBoxx";
            this.textBoxx.Size = new System.Drawing.Size(249, 38);
            this.textBoxx.TabIndex = 1;
            // 
            // textBoxy
            // 
            this.textBoxy.Location = new System.Drawing.Point(30, 160);
            this.textBoxy.Name = "textBoxy";
            this.textBoxy.Size = new System.Drawing.Size(249, 38);
            this.textBoxy.TabIndex = 2;
            // 
            // textBoxz
            // 
            this.textBoxz.Location = new System.Drawing.Point(30, 213);
            this.textBoxz.Name = "textBoxz";
            this.textBoxz.Size = new System.Drawing.Size(257, 38);
            this.textBoxz.TabIndex = 3;
            // 
            // VectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxz);
            this.Controls.Add(this.textBoxy);
            this.Controls.Add(this.textBoxx);
            this.Controls.Add(this.label1);
            this.Name = "VectorControl";
            this.Size = new System.Drawing.Size(309, 295);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxx;
        private System.Windows.Forms.TextBox textBoxy;
        private System.Windows.Forms.TextBox textBoxz;
    }
}
