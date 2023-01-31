namespace cosetTest
{
    partial class Login
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblFind = new System.Windows.Forms.LinkLabel();
            this.imgMini = new System.Windows.Forms.PictureBox();
            this.imgExit = new System.Windows.Forms.PictureBox();
            this.panelDrag = new System.Windows.Forms.Panel();
            this.checkBoxID = new System.Windows.Forms.CheckBox();
            this.imglistCheck = new System.Windows.Forms.ImageList(this.components);
            this.checkAutologin = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMini)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).BeginInit();
            this.panelDrag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.ImageLocation = "\\\\E_Doc\\98 ETC\\E03 소모품(데이터베이스)\\test\\사진\\logo2.png";
            this.imgLogo.Location = new System.Drawing.Point(85, 66);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(150, 60);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgLogo.TabIndex = 0;
            this.imgLogo.TabStop = false;
            // 
            // txtId
            // 
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtId.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtId.Location = new System.Drawing.Point(71, 176);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(177, 18);
            this.txtId.TabIndex = 2;
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtId_KeyDown);
            // 
            // txtPw
            // 
            this.txtPw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPw.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPw.Location = new System.Drawing.Point(71, 217);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(177, 18);
            this.txtPw.TabIndex = 3;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.Window;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnLogin.Location = new System.Drawing.Point(56, 296);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(205, 32);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "로 그 인";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(52, 314);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 12);
            this.lblResult.TabIndex = 6;
            // 
            // lblFind
            // 
            this.lblFind.ActiveLinkColor = System.Drawing.Color.DarkGray;
            this.lblFind.AutoSize = true;
            this.lblFind.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFind.LinkColor = System.Drawing.Color.SteelBlue;
            this.lblFind.Location = new System.Drawing.Point(53, 344);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(211, 13);
            this.lblFind.TabIndex = 6;
            this.lblFind.TabStop = true;
            this.lblFind.Text = "아이디나 비밀번호가 기억나지 않으세요?";
            this.lblFind.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFind_LinkClicked);
            this.lblFind.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblFind_MouseMove);
            // 
            // imgMini
            // 
            this.imgMini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgMini.ImageLocation = "\\\\E_Doc\\98 ETC\\E03 소모품(데이터베이스)\\test\\사진\\minus2.png";
            this.imgMini.Location = new System.Drawing.Point(274, 5);
            this.imgMini.Name = "imgMini";
            this.imgMini.Size = new System.Drawing.Size(16, 16);
            this.imgMini.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgMini.TabIndex = 26;
            this.imgMini.TabStop = false;
            this.imgMini.Click += new System.EventHandler(this.imgMini_Click);
            // 
            // imgExit
            // 
            this.imgExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgExit.ImageLocation = "\\\\E_Doc\\98 ETC\\E03 소모품(데이터베이스)\\test\\사진\\close2.png";
            this.imgExit.Location = new System.Drawing.Point(296, 5);
            this.imgExit.Name = "imgExit";
            this.imgExit.Size = new System.Drawing.Size(16, 16);
            this.imgExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgExit.TabIndex = 25;
            this.imgExit.TabStop = false;
            this.imgExit.Click += new System.EventHandler(this.imgExit_Click);
            // 
            // panelDrag
            // 
            this.panelDrag.Controls.Add(this.lblCopy);
            this.panelDrag.Controls.Add(this.imgMini);
            this.panelDrag.Controls.Add(this.btnLogin);
            this.panelDrag.Controls.Add(this.checkBoxID);
            this.panelDrag.Controls.Add(this.imgExit);
            this.panelDrag.Controls.Add(this.checkAutologin);
            this.panelDrag.Controls.Add(this.lblFind);
            this.panelDrag.Controls.Add(this.imgLogo);
            this.panelDrag.Controls.Add(this.txtId);
            this.panelDrag.Controls.Add(this.txtPw);
            this.panelDrag.Controls.Add(this.pictureBox1);
            this.panelDrag.Controls.Add(this.pictureBox2);
            this.panelDrag.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelDrag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelDrag.Location = new System.Drawing.Point(-4, -4);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(320, 439);
            this.panelDrag.TabIndex = 27;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            this.panelDrag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseMove);
            // 
            // checkBoxID
            // 
            this.checkBoxID.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxID.AutoSize = true;
            this.checkBoxID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBoxID.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.checkBoxID.FlatAppearance.CheckedBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBoxID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBoxID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkBoxID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxID.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBoxID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.checkBoxID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkBoxID.ImageIndex = 0;
            this.checkBoxID.ImageList = this.imglistCheck;
            this.checkBoxID.Location = new System.Drawing.Point(56, 262);
            this.checkBoxID.Name = "checkBoxID";
            this.checkBoxID.Size = new System.Drawing.Size(94, 23);
            this.checkBoxID.TabIndex = 4;
            this.checkBoxID.Text = " 아이디 기억";
            this.checkBoxID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkBoxID.UseVisualStyleBackColor = true;
            this.checkBoxID.CheckedChanged += new System.EventHandler(this.checkBoxID_CheckedChanged);
            // 
            // imglistCheck
            // 
            this.imglistCheck.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistCheck.ImageStream")));
            this.imglistCheck.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistCheck.Images.SetKeyName(0, "check_off.png");
            this.imglistCheck.Images.SetKeyName(1, "check_on.png");
            // 
            // checkAutologin
            // 
            this.checkAutologin.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkAutologin.AutoSize = true;
            this.checkAutologin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkAutologin.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.checkAutologin.FlatAppearance.CheckedBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkAutologin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkAutologin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.checkAutologin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkAutologin.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkAutologin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.checkAutologin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkAutologin.ImageIndex = 0;
            this.checkAutologin.ImageList = this.imglistCheck;
            this.checkAutologin.Location = new System.Drawing.Point(167, 262);
            this.checkAutologin.Name = "checkAutologin";
            this.checkAutologin.Size = new System.Drawing.Size(94, 23);
            this.checkAutologin.TabIndex = 5;
            this.checkAutologin.Text = " 자동 로그인";
            this.checkAutologin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.checkAutologin.UseVisualStyleBackColor = true;
            this.checkAutologin.CheckedChanged += new System.EventHandler(this.checkAutologin_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "\\\\E_Doc\\98 ETC\\E03 소모품(데이터베이스)\\test\\사진\\textbox.png";
            this.pictureBox1.Location = new System.Drawing.Point(59, 172);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ImageLocation = "\\\\E_Doc\\98 ETC\\E03 소모품(데이터베이스)\\test\\사진\\textbox.png";
            this.pictureBox2.Location = new System.Drawing.Point(59, 213);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // lblCopy
            // 
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCopy.ForeColor = System.Drawing.Color.Silver;
            this.lblCopy.Location = new System.Drawing.Point(63, 396);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(185, 13);
            this.lblCopy.TabIndex = 29;
            this.lblCopy.Text = "ⓒ 2022. COSET All rights reserved.";
            // 
            // Login
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(314, 414);
            this.ControlBox = false;
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.panelDrag);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMini)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgExit)).EndInit();
            this.panelDrag.ResumeLayout(false);
            this.panelDrag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.LinkLabel lblFind;
        private System.Windows.Forms.PictureBox imgMini;
        private System.Windows.Forms.PictureBox imgExit;
        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.CheckBox checkAutologin;
        private System.Windows.Forms.ImageList imglistCheck;
        private System.Windows.Forms.CheckBox checkBoxID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCopy;
    }
}

