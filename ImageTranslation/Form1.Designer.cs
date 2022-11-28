namespace ImageTranslation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCapture = new System.Windows.Forms.Button();
            this.pbCapture = new System.Windows.Forms.PictureBox();
            this.txtTranslation = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(12, 12);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(162, 23);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.Text = "화면 캡처";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // pbCapture
            // 
            this.pbCapture.Location = new System.Drawing.Point(12, 41);
            this.pbCapture.Name = "pbCapture";
            this.pbCapture.Size = new System.Drawing.Size(487, 397);
            this.pbCapture.TabIndex = 1;
            this.pbCapture.TabStop = false;
            // 
            // txtTranslation
            // 
            this.txtTranslation.Location = new System.Drawing.Point(516, 41);
            this.txtTranslation.Multiline = true;
            this.txtTranslation.Name = "txtTranslation";
            this.txtTranslation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslation.Size = new System.Drawing.Size(591, 397);
            this.txtTranslation.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 450);
            this.Controls.Add(this.txtTranslation);
            this.Controls.Add(this.pbCapture);
            this.Controls.Add(this.btnCapture);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCapture;
        private PictureBox pbCapture;
        private TextBox txtTranslation;
    }
}