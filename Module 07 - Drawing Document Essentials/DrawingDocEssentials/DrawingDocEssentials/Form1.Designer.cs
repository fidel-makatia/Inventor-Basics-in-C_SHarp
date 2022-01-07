namespace DrawingDocEssentials
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
            this.AddTitleBlock = new System.Windows.Forms.Button();
            this.CreateBorderDefination = new System.Windows.Forms.Button();
            this.CreateViews = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddTitleBlock
            // 
            this.AddTitleBlock.Location = new System.Drawing.Point(122, 88);
            this.AddTitleBlock.Name = "AddTitleBlock";
            this.AddTitleBlock.Size = new System.Drawing.Size(631, 87);
            this.AddTitleBlock.TabIndex = 0;
            this.AddTitleBlock.Text = "Add Title Block";
            this.AddTitleBlock.UseVisualStyleBackColor = true;
            this.AddTitleBlock.Click += new System.EventHandler(this.AddTitleBlock_Click);
            // 
            // CreateBorderDefination
            // 
            this.CreateBorderDefination.Location = new System.Drawing.Point(122, 241);
            this.CreateBorderDefination.Name = "CreateBorderDefination";
            this.CreateBorderDefination.Size = new System.Drawing.Size(631, 90);
            this.CreateBorderDefination.TabIndex = 1;
            this.CreateBorderDefination.Text = "Border Defination\r\n";
            this.CreateBorderDefination.UseVisualStyleBackColor = true;
            this.CreateBorderDefination.Click += new System.EventHandler(this.CreateBorderDefination_Click);
            // 
            // CreateViews
            // 
            this.CreateViews.Location = new System.Drawing.Point(123, 383);
            this.CreateViews.Name = "CreateViews";
            this.CreateViews.Size = new System.Drawing.Size(630, 82);
            this.CreateViews.TabIndex = 2;
            this.CreateViews.Text = "Create Views\r\n";
            this.CreateViews.UseVisualStyleBackColor = true;
            this.CreateViews.Click += new System.EventHandler(this.CreateViews_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(630, 100);
            this.button1.TabIndex = 3;
            this.button1.Text = "Retrieve Dimensions From Model";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 853);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CreateViews);
            this.Controls.Add(this.CreateBorderDefination);
            this.Controls.Add(this.AddTitleBlock);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddTitleBlock;
        private System.Windows.Forms.Button CreateBorderDefination;
        private System.Windows.Forms.Button CreateViews;
        private System.Windows.Forms.Button button1;
    }
}

