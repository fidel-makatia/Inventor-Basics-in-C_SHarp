namespace ASM
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
            this.Assembly = new System.Windows.Forms.Button();
            this.Occurence = new System.Windows.Forms.Button();
            this.Lab = new System.Windows.Forms.Button();
            this.Transform = new System.Windows.Forms.Button();
            this.AngleLabel = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Assembly
            // 
            this.Assembly.Location = new System.Drawing.Point(34, 12);
            this.Assembly.Name = "Assembly";
            this.Assembly.Size = new System.Drawing.Size(356, 70);
            this.Assembly.TabIndex = 0;
            this.Assembly.Text = "Create Assembly";
            this.Assembly.UseVisualStyleBackColor = true;
            this.Assembly.Click += new System.EventHandler(this.Assembly_Click);
            // 
            // Occurence
            // 
            this.Occurence.Location = new System.Drawing.Point(34, 124);
            this.Occurence.Name = "Occurence";
            this.Occurence.Size = new System.Drawing.Size(356, 65);
            this.Occurence.TabIndex = 1;
            this.Occurence.Text = "Add Occurence";
            this.Occurence.UseVisualStyleBackColor = true;
            this.Occurence.Click += new System.EventHandler(this.Occurence_Click);
            // 
            // Lab
            // 
            this.Lab.Location = new System.Drawing.Point(34, 734);
            this.Lab.Name = "Lab";
            this.Lab.Size = new System.Drawing.Size(339, 72);
            this.Lab.TabIndex = 2;
            this.Lab.Text = "Lab Demo Constraints";
            this.Lab.UseVisualStyleBackColor = true;
            this.Lab.Click += new System.EventHandler(this.Lab_Click);
            // 
            // Transform
            // 
            this.Transform.Location = new System.Drawing.Point(34, 641);
            this.Transform.Name = "Transform";
            this.Transform.Size = new System.Drawing.Size(339, 50);
            this.Transform.TabIndex = 3;
            this.Transform.Text = "Transform Occurence";
            this.Transform.UseVisualStyleBackColor = true;
            this.Transform.Click += new System.EventHandler(this.Transform_Click);
            // 
            // AngleLabel
            // 
            this.AngleLabel.AutoSize = true;
            this.AngleLabel.Location = new System.Drawing.Point(28, 509);
            this.AngleLabel.Name = "AngleLabel";
            this.AngleLabel.Size = new System.Drawing.Size(174, 32);
            this.AngleLabel.TabIndex = 4;
            this.AngleLabel.Text = "Angle(Deg.):";
            // 
            // Angle
            // 
            this.Angle.Location = new System.Drawing.Point(79, 575);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(248, 38);
            this.Angle.TabIndex = 5;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(405, 857);
            this.Controls.Add(this.Angle);
            this.Controls.Add(this.AngleLabel);
            this.Controls.Add(this.Transform);
            this.Controls.Add(this.Lab);
            this.Controls.Add(this.Occurence);
            this.Controls.Add(this.Assembly);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button CreateAssembly;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button AssemblyCreation;
        private System.Windows.Forms.Button Assembly;
        private System.Windows.Forms.Button Occurence;
        private System.Windows.Forms.Button Lab;
        private System.Windows.Forms.Button Transform;
        private System.Windows.Forms.Label AngleLabel;
        private VectorControl VectorControl2;
        private VectorControl VectorControl1;
        private System.Windows.Forms.TextBox Angle;
    }
}

