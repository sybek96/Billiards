namespace lab7_starter1
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
            this.lblScoreWhite = new System.Windows.Forms.Label();
            this.lblScoreYellow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblScoreWhite
            // 
            this.lblScoreWhite.AutoSize = true;
            this.lblScoreWhite.BackColor = System.Drawing.Color.ForestGreen;
            this.lblScoreWhite.Location = new System.Drawing.Point(106, 441);
            this.lblScoreWhite.Name = "lblScoreWhite";
            this.lblScoreWhite.Size = new System.Drawing.Size(84, 13);
            this.lblScoreWhite.TabIndex = 0;
            this.lblScoreWhite.Text = "White Score = 0";
            // 
            // lblScoreYellow
            // 
            this.lblScoreYellow.AutoSize = true;
            this.lblScoreYellow.BackColor = System.Drawing.Color.ForestGreen;
            this.lblScoreYellow.Location = new System.Drawing.Point(608, 441);
            this.lblScoreYellow.Name = "lblScoreYellow";
            this.lblScoreYellow.Size = new System.Drawing.Size(87, 13);
            this.lblScoreYellow.TabIndex = 1;
            this.lblScoreYellow.Text = "Yellow Score = 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 486);
            this.Controls.Add(this.lblScoreYellow);
            this.Controls.Add(this.lblScoreWhite);
            this.Name = "Form1";
            this.Text = "Ball";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScoreWhite;
        private System.Windows.Forms.Label lblScoreYellow;
    }
}

