namespace ImageTranslation
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.btnSetLocation = new System.Windows.Forms.Button();
            this.lblLang = new System.Windows.Forms.Label();
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.textTrans = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtTransTo = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tCheck = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnSetLocation
            // 
            this.btnSetLocation.Location = new System.Drawing.Point(1, 155);
            this.btnSetLocation.Name = "btnSetLocation";
            this.btnSetLocation.Size = new System.Drawing.Size(154, 23);
            this.btnSetLocation.TabIndex = 0;
            this.btnSetLocation.Text = "영역 지정";
            this.btnSetLocation.UseVisualStyleBackColor = true;
            // 
            // lblLang
            // 
            this.lblLang.AutoSize = true;
            this.lblLang.Location = new System.Drawing.Point(607, 159);
            this.lblLang.Name = "lblLang";
            this.lblLang.Size = new System.Drawing.Size(78, 15);
            this.lblLang.TabIndex = 1;
            this.lblLang.Text = "번역할 언어 :";
            // 
            // cbLang
            // 
            this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Items.AddRange(new object[] {
            "영어",
            "일본어",
            "중국어"});
            this.cbLang.Location = new System.Drawing.Point(691, 155);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(86, 23);
            this.cbLang.TabIndex = 2;
            // 
            // textTrans
            // 
            this.textTrans.Location = new System.Drawing.Point(1, 3);
            this.textTrans.Multiline = true;
            this.textTrans.Name = "textTrans";
            this.textTrans.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textTrans.Size = new System.Drawing.Size(776, 146);
            this.textTrans.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1, 184);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(154, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtTransTo
            // 
            this.txtTransTo.Location = new System.Drawing.Point(1, 3);
            this.txtTransTo.Multiline = true;
            this.txtTransTo.Name = "txtTransTo";
            this.txtTransTo.Size = new System.Drawing.Size(776, 146);
            this.txtTransTo.TabIndex = 5;
            this.txtTransTo.Visible = false;
            this.txtTransTo.TextChanged += new System.EventHandler(this.txtTransTo_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(353, 151);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(353, 180);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "중지";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tCheck
            // 
            this.tCheck.Interval = 1000;
            this.tCheck.Tick += new System.EventHandler(this.tCheck_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 208);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textTrans);
            this.Controls.Add(this.cbLang);
            this.Controls.Add(this.lblLang);
            this.Controls.Add(this.btnSetLocation);
            this.Controls.Add(this.txtTransTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MdiChildrenMinimizedAnchorBottom = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.ShowIcon = false;
            this.Text = "GameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSetLocation;
        private Label lblLang;
        private ComboBox cbLang;
        private TextBox textTrans;
        private Button btnClose;
        private TextBox txtTransTo;
        private Button btnStart;
        private Button btnStop;
        private System.Windows.Forms.Timer tCheck;
    }
}