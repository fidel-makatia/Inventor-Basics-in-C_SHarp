namespace Transaction_C_sharp
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
            this.UsingTrans = new System.Windows.Forms.Button();
            this.CreateMyProperty = new System.Windows.Forms.Button();
            this.start_end_abort = new System.Windows.Forms.Button();
            this.checkPoints = new System.Windows.Forms.Button();
            this.Parent_Child_Trasns = new System.Windows.Forms.Button();
            this.startEvents = new System.Windows.Forms.Button();
            this.stopEvents = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UsingTrans
            // 
            this.UsingTrans.Location = new System.Drawing.Point(79, 51);
            this.UsingTrans.Name = "UsingTrans";
            this.UsingTrans.Size = new System.Drawing.Size(368, 60);
            this.UsingTrans.TabIndex = 0;
            this.UsingTrans.Text = "Use Transaction";
            this.UsingTrans.UseVisualStyleBackColor = true;
            this.UsingTrans.Click += new System.EventHandler(this.UsingTrans_Click);
            // 
            // CreateMyProperty
            // 
            this.CreateMyProperty.Location = new System.Drawing.Point(79, 151);
            this.CreateMyProperty.Name = "CreateMyProperty";
            this.CreateMyProperty.Size = new System.Drawing.Size(368, 72);
            this.CreateMyProperty.TabIndex = 1;
            this.CreateMyProperty.Text = "Create My Property";
            this.CreateMyProperty.UseVisualStyleBackColor = true;
            this.CreateMyProperty.Click += new System.EventHandler(this.CreateMyProperty_Click);
            // 
            // start_end_abort
            // 
            this.start_end_abort.Location = new System.Drawing.Point(79, 256);
            this.start_end_abort.Name = "start_end_abort";
            this.start_end_abort.Size = new System.Drawing.Size(368, 92);
            this.start_end_abort.TabIndex = 2;
            this.start_end_abort.Text = "start_end_abort";
            this.start_end_abort.UseVisualStyleBackColor = true;
            this.start_end_abort.Click += new System.EventHandler(this.start_end_abort_Click);
            // 
            // checkPoints
            // 
            this.checkPoints.Location = new System.Drawing.Point(79, 389);
            this.checkPoints.Name = "checkPoints";
            this.checkPoints.Size = new System.Drawing.Size(368, 68);
            this.checkPoints.TabIndex = 3;
            this.checkPoints.Text = "add checkPoints";
            this.checkPoints.UseVisualStyleBackColor = true;
            this.checkPoints.Click += new System.EventHandler(this.checkPoints_Click);
            // 
            // Parent_Child_Trasns
            // 
            this.Parent_Child_Trasns.Location = new System.Drawing.Point(79, 488);
            this.Parent_Child_Trasns.Name = "Parent_Child_Trasns";
            this.Parent_Child_Trasns.Size = new System.Drawing.Size(368, 74);
            this.Parent_Child_Trasns.TabIndex = 4;
            this.Parent_Child_Trasns.Text = "Parent Child Trasnsaction";
            this.Parent_Child_Trasns.UseVisualStyleBackColor = true;
            this.Parent_Child_Trasns.Click += new System.EventHandler(this.Parent_Child_Trasns_Click);
            // 
            // startEvents
            // 
            this.startEvents.Location = new System.Drawing.Point(68, 598);
            this.startEvents.Name = "startEvents";
            this.startEvents.Size = new System.Drawing.Size(387, 60);
            this.startEvents.TabIndex = 5;
            this.startEvents.Text = "Start Events";
            this.startEvents.UseVisualStyleBackColor = true;
            this.startEvents.Click += new System.EventHandler(this.startEvents_Click);
            // 
            // stopEvents
            // 
            this.stopEvents.Location = new System.Drawing.Point(68, 684);
            this.stopEvents.Name = "stopEvents";
            this.stopEvents.Size = new System.Drawing.Size(379, 71);
            this.stopEvents.TabIndex = 10;
            this.stopEvents.Text = "Stop Events";
            this.stopEvents.UseVisualStyleBackColor = true;
            this.stopEvents.Click += new System.EventHandler(this.stopEvents_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 807);
            this.Controls.Add(this.stopEvents);
            this.Controls.Add(this.startEvents);
            this.Controls.Add(this.Parent_Child_Trasns);
            this.Controls.Add(this.checkPoints);
            this.Controls.Add(this.start_end_abort);
            this.Controls.Add(this.CreateMyProperty);
            this.Controls.Add(this.UsingTrans);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UsingTrans;
        private System.Windows.Forms.Button CreateMyProperty;
        private System.Windows.Forms.Button start_end_abort;
        private System.Windows.Forms.Button checkPoints;
        private System.Windows.Forms.Button Parent_Child_Trasns;
        private System.Windows.Forms.Button startEvents;
        private System.Windows.Forms.Button stopEvents;
    }
}

