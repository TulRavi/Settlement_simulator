using Azure;
using SettlementGame.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsAppUI
{
    public partial class AuthForm : Form
    {
        private readonly AuthService _authService;
        // сервис авторизации

        public string Token { get; private set; }
        // токен доступен снаружи, но записывается только внутри

        public AuthForm(AuthService authService)
        {
            InitializeComponent();
            // создаёт UI

            _authService = authService;
            // сохраняем сервис
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("нажата кнопка login");
            System.Diagnostics.Debug.WriteLine("нажата кнопка");

            string email = txtLogin.Text;
            // читаем логин

            string password = txtPassword.Text;
            // читаем пароль

            string token = await _authService.loginTask(email, password);
            // вызываем сервис (UI не знает про HTTP)
            //MessageBox.Show(token);
            //Console.WriteLine(token);

            if (token == null)
            {

                //MessageBox.Show("Ошибка авторизации");
                DialogResult result = MessageBox.Show(
                "Введите логин и пароль заново",
                "Ошибка авторизации",
                MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Activate();
                    txtLogin.Focus();
                }
                
            }
            if (token != null) { 
            Token = token;
            // сохраняем токен

            DialogResult = DialogResult.OK;
            // сигнал Program.cs

            Close();
            // закрываем форму
        }
        }

    }
}
