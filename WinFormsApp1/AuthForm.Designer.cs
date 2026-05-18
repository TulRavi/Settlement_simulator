namespace WinFormsAppUI
{
    partial class AuthForm
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
            txtPassword = new TextBox();
            btnLogin = new Button();
            
            txtLogin = new TextBox();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(195, 121);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(225, 43);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(225, 204);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(169, 52);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(198, 68);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(225, 43);
            txtLogin.TabIndex = 3;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtLogin);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Name = "AuthForm";
            Text = "Form2";
            //Load += AuthForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox loginBtn;
        private TextBox txtPassword;
        private Button btnLogin;
        private TextBox txtLogin;
    }
}