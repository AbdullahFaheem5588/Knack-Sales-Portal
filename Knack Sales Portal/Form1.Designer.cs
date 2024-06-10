namespace Knack_Sales_Portal
{
    partial class Form_login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_login));
            this.panel_Login = new System.Windows.Forms.Panel();
            this.lb_Login_WrongInfo = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.lb_Login_password = new System.Windows.Forms.Label();
            this.lb_Login_UserName = new System.Windows.Forms.Label();
            this.tb_Login_password = new System.Windows.Forms.TextBox();
            this.tb_Login_UserName = new System.Windows.Forms.TextBox();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_Login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_Login
            // 
            this.panel_Login.BackColor = System.Drawing.Color.Purple;
            this.panel_Login.Controls.Add(this.lb_Login_WrongInfo);
            this.panel_Login.Controls.Add(this.btn_Login);
            this.panel_Login.Controls.Add(this.lb_Login_password);
            this.panel_Login.Controls.Add(this.lb_Login_UserName);
            this.panel_Login.Controls.Add(this.tb_Login_password);
            this.panel_Login.Controls.Add(this.tb_Login_UserName);
            this.panel_Login.Location = new System.Drawing.Point(13, 134);
            this.panel_Login.Name = "panel_Login";
            this.panel_Login.Size = new System.Drawing.Size(677, 294);
            this.panel_Login.TabIndex = 0;
            // 
            // lb_Login_WrongInfo
            // 
            this.lb_Login_WrongInfo.AutoSize = true;
            this.lb_Login_WrongInfo.BackColor = System.Drawing.Color.Transparent;
            this.lb_Login_WrongInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lb_Login_WrongInfo.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Login_WrongInfo.ForeColor = System.Drawing.Color.Red;
            this.lb_Login_WrongInfo.Location = new System.Drawing.Point(210, 191);
            this.lb_Login_WrongInfo.Name = "lb_Login_WrongInfo";
            this.lb_Login_WrongInfo.Size = new System.Drawing.Size(169, 16);
            this.lb_Login_WrongInfo.TabIndex = 3;
            this.lb_Login_WrongInfo.Text = "Invalid User Name or Password";
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(213, 160);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(259, 28);
            this.btn_Login.TabIndex = 0;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lb_Login_password
            // 
            this.lb_Login_password.AutoSize = true;
            this.lb_Login_password.BackColor = System.Drawing.Color.White;
            this.lb_Login_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lb_Login_password.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Login_password.ForeColor = System.Drawing.Color.Gray;
            this.lb_Login_password.Location = new System.Drawing.Point(317, 118);
            this.lb_Login_password.Name = "lb_Login_password";
            this.lb_Login_password.Size = new System.Drawing.Size(57, 16);
            this.lb_Login_password.TabIndex = 0;
            this.lb_Login_password.Text = "Password";
            this.lb_Login_password.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_Login_password_MouseClick);
            // 
            // lb_Login_UserName
            // 
            this.lb_Login_UserName.AutoSize = true;
            this.lb_Login_UserName.BackColor = System.Drawing.Color.White;
            this.lb_Login_UserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lb_Login_UserName.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Login_UserName.ForeColor = System.Drawing.Color.Gray;
            this.lb_Login_UserName.Location = new System.Drawing.Point(313, 74);
            this.lb_Login_UserName.Name = "lb_Login_UserName";
            this.lb_Login_UserName.Size = new System.Drawing.Size(63, 16);
            this.lb_Login_UserName.TabIndex = 0;
            this.lb_Login_UserName.Text = "User Name";
            this.lb_Login_UserName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lb_Login_UserName_MouseClick);
            // 
            // tb_Login_password
            // 
            this.tb_Login_password.Location = new System.Drawing.Point(213, 115);
            this.tb_Login_password.Name = "tb_Login_password";
            this.tb_Login_password.Size = new System.Drawing.Size(259, 20);
            this.tb_Login_password.TabIndex = 2;
            this.tb_Login_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Login_password.UseSystemPasswordChar = true;
            this.tb_Login_password.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tb_Login_password_MouseClick);
            this.tb_Login_password.Enter += new System.EventHandler(this.tb_Login_password_Enter);
            // 
            // tb_Login_UserName
            // 
            this.tb_Login_UserName.AccessibleDescription = "";
            this.tb_Login_UserName.AccessibleName = "";
            this.tb_Login_UserName.Location = new System.Drawing.Point(213, 71);
            this.tb_Login_UserName.Name = "tb_Login_UserName";
            this.tb_Login_UserName.Size = new System.Drawing.Size(259, 20);
            this.tb_Login_UserName.TabIndex = 1;
            this.tb_Login_UserName.Tag = "";
            this.tb_Login_UserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Login_UserName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tb_Login_UserName_MouseClick);
            this.tb_Login_UserName.Enter += new System.EventHandler(this.tb_Login_UserName_Enter);
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::Knack_Sales_Portal.Properties.Resources.Knack_Logo;
            this.pictureBox_logo.Location = new System.Drawing.Point(186, 12);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(345, 116);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_logo.TabIndex = 1;
            this.pictureBox_logo.TabStop = false;
            // 
            // Form_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(702, 440);
            this.Controls.Add(this.pictureBox_logo);
            this.Controls.Add(this.panel_Login);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form_login";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Knack | Login";
            this.Load += new System.EventHandler(this.Form_login_Load);
            this.panel_Login.ResumeLayout(false);
            this.panel_Login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Login;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Label lb_Login_password;
        private System.Windows.Forms.Label lb_Login_UserName;
        private System.Windows.Forms.TextBox tb_Login_password;
        private System.Windows.Forms.TextBox tb_Login_UserName;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label lb_Login_WrongInfo;
    }
}

