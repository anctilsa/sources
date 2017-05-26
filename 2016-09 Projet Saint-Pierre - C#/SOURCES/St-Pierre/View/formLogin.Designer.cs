namespace StPierre.View
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.adminLoginButton = new System.Windows.Forms.Button();
            this.userLoginLabel = new System.Windows.Forms.Label();
            this.userCode = new System.Windows.Forms.TextBox();
            this.adminLoginLabel = new System.Windows.Forms.Label();
            this.adminCode = new System.Windows.Forms.TextBox();
            this.adminPassword = new System.Windows.Forms.TextBox();
            this.userLoginButton = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.error = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // adminLoginButton
            // 
            this.adminLoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLoginButton.Location = new System.Drawing.Point(334, 153);
            this.adminLoginButton.Name = "adminLoginButton";
            this.adminLoginButton.Size = new System.Drawing.Size(33, 26);
            this.adminLoginButton.TabIndex = 4;
            this.adminLoginButton.Text = ">>";
            this.adminLoginButton.UseVisualStyleBackColor = true;
            this.adminLoginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // userLoginLabel
            // 
            this.userLoginLabel.AutoSize = true;
            this.userLoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLoginLabel.Location = new System.Drawing.Point(184, 30);
            this.userLoginLabel.Name = "userLoginLabel";
            this.userLoginLabel.Size = new System.Drawing.Size(80, 20);
            this.userLoginLabel.TabIndex = 5;
            this.userLoginLabel.Text = "Utilisateur";
            // 
            // userCode
            // 
            this.userCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userCode.ForeColor = System.Drawing.Color.Gray;
            this.userCode.Location = new System.Drawing.Point(188, 54);
            this.userCode.Name = "userCode";
            this.userCode.Size = new System.Drawing.Size(140, 26);
            this.userCode.TabIndex = 0;
            this.userCode.Tag = "inactive";
            this.userCode.Text = "No d\'employé";
            this.userCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loginButton_KeyDown);
            // 
            // adminLoginLabel
            // 
            this.adminLoginLabel.AutoSize = true;
            this.adminLoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminLoginLabel.Location = new System.Drawing.Point(184, 98);
            this.adminLoginLabel.Name = "adminLoginLabel";
            this.adminLoginLabel.Size = new System.Drawing.Size(112, 20);
            this.adminLoginLabel.TabIndex = 6;
            this.adminLoginLabel.Text = "Administrateur";
            // 
            // adminCode
            // 
            this.adminCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminCode.ForeColor = System.Drawing.Color.Gray;
            this.adminCode.Location = new System.Drawing.Point(188, 121);
            this.adminCode.Name = "adminCode";
            this.adminCode.Size = new System.Drawing.Size(140, 26);
            this.adminCode.TabIndex = 1;
            this.adminCode.Tag = "inactive";
            this.adminCode.Text = "No d\'employé";
            this.adminCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loginButton_KeyDown);
            // 
            // adminPassword
            // 
            this.adminPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminPassword.ForeColor = System.Drawing.Color.Gray;
            this.adminPassword.Location = new System.Drawing.Point(188, 153);
            this.adminPassword.Name = "adminPassword";
            this.adminPassword.Size = new System.Drawing.Size(140, 26);
            this.adminPassword.TabIndex = 2;
            this.adminPassword.Tag = "inactive";
            this.adminPassword.Text = "Mot de passe";
            this.adminPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loginButton_KeyDown);
            // 
            // userLoginButton
            // 
            this.userLoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLoginButton.Location = new System.Drawing.Point(334, 54);
            this.userLoginButton.Name = "userLoginButton";
            this.userLoginButton.Size = new System.Drawing.Size(33, 26);
            this.userLoginButton.TabIndex = 3;
            this.userLoginButton.Text = ">>";
            this.userLoginButton.UseVisualStyleBackColor = true;
            this.userLoginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.SystemColors.Control;
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo.InitialImage = null;
            this.logo.Location = new System.Drawing.Point(12, 30);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(155, 149);
            this.logo.TabIndex = 7;
            this.logo.TabStop = false;
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error.ForeColor = System.Drawing.Color.DarkRed;
            this.error.Location = new System.Drawing.Point(13, 183);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(0, 18);
            this.error.TabIndex = 9;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(388, 213);
            this.Controls.Add(this.error);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.userLoginButton);
            this.Controls.Add(this.adminPassword);
            this.Controls.Add(this.adminCode);
            this.Controls.Add(this.adminLoginLabel);
            this.Controls.Add(this.userCode);
            this.Controls.Add(this.userLoginLabel);
            this.Controls.Add(this.adminLoginButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(404, 252);
            this.MinimumSize = new System.Drawing.Size(404, 252);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Se connecter";
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button adminLoginButton;
        private System.Windows.Forms.Label userLoginLabel;
        private System.Windows.Forms.TextBox userCode;
        private System.Windows.Forms.Label adminLoginLabel;
        private System.Windows.Forms.TextBox adminCode;
        private System.Windows.Forms.TextBox adminPassword;
        private System.Windows.Forms.Button userLoginButton;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label error;
    }
}