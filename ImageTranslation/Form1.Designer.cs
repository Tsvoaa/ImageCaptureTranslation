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
            this.txtTranslationTo = new System.Windows.Forms.TextBox();
            this.cbBefore = new System.Windows.Forms.ComboBox();
            this.cbAfter = new System.Windows.Forms.ComboBox();
            this.btnGameMode = new System.Windows.Forms.Button();
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
            this.txtTranslation.Location = new System.Drawing.Point(12, 41);
            this.txtTranslation.Multiline = true;
            this.txtTranslation.Name = "txtTranslation";
            this.txtTranslation.ReadOnly = true;
            this.txtTranslation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslation.Size = new System.Drawing.Size(564, 397);
            this.txtTranslation.TabIndex = 2;
            // 
            // txtTranslationTo
            // 
            this.txtTranslationTo.Location = new System.Drawing.Point(582, 41);
            this.txtTranslationTo.Multiline = true;
            this.txtTranslationTo.Name = "txtTranslationTo";
            this.txtTranslationTo.ReadOnly = true;
            this.txtTranslationTo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslationTo.Size = new System.Drawing.Size(564, 397);
            this.txtTranslationTo.TabIndex = 3;
            // 
            // cbBefore
            // 
            this.cbBefore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBefore.FormattingEnabled = true;
            this.cbBefore.Items.AddRange(new object[] {
            "한국어",
            "영어",
            "일본어",
            "중국어"});
            this.cbBefore.Location = new System.Drawing.Point(455, 12);
            this.cbBefore.Name = "cbBefore";
            this.cbBefore.Size = new System.Drawing.Size(121, 23);
            this.cbBefore.TabIndex = 4;
            // 
            // cbAfter
            // 
            this.cbAfter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAfter.FormattingEnabled = true;
            this.cbAfter.Items.AddRange(new object[] {
            "한국어",
            "영어",
            "일본어",
            "중국어"});
            this.cbAfter.Location = new System.Drawing.Point(1025, 12);
            this.cbAfter.Name = "cbAfter";
            this.cbAfter.Size = new System.Drawing.Size(121, 23);
            this.cbAfter.TabIndex = 5;
            // 
            // btnGameMode
            // 
            this.btnGameMode.Location = new System.Drawing.Point(1007, 444);
            this.btnGameMode.Name = "btnGameMode";
            this.btnGameMode.Size = new System.Drawing.Size(139, 23);
            this.btnGameMode.TabIndex = 6;
            this.btnGameMode.Text = "게임모드";
            this.btnGameMode.UseVisualStyleBackColor = true;
            this.btnGameMode.Click += new System.EventHandler(this.btnGameMode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 477);
            this.Controls.Add(this.btnGameMode);
            this.Controls.Add(this.cbAfter);
            this.Controls.Add(this.cbBefore);
            this.Controls.Add(this.txtTranslationTo);
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
        private TextBox txtTranslationTo;
        private ComboBox cbBefore;
        private ComboBox cbAfter;
        private Button btnGameMode;
    }
}