namespace SheetMetal
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
            this.CreateSheetMetalStyle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateSheetMetalStyle
            // 
            this.CreateSheetMetalStyle.Location = new System.Drawing.Point(83, 45);
            this.CreateSheetMetalStyle.Name = "CreateSheetMetalStyle";
            this.CreateSheetMetalStyle.Size = new System.Drawing.Size(592, 95);
            this.CreateSheetMetalStyle.TabIndex = 0;
            this.CreateSheetMetalStyle.Text = "Create SheetMetal Style\n";
            this.CreateSheetMetalStyle.UseVisualStyleBackColor = true;
            this.CreateSheetMetalStyle.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateSheetMetalStyle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateSheetMetalStyle;
    }
}

