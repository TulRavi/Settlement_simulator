using SettlementGame.Domain;
using WinFormsAppUI;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            HttpClient client = new HttpClient();//1 на всё приложение
            client.Timeout = TimeSpan.FromMinutes(5);
            client.BaseAddress = new Uri("http://localhost:5126/");
            AuthService authService = new AuthService(client);
            // создаём сервис авторизации
            AuthForm authForm = new AuthForm(authService);
            // создаём форму логина
            if (authForm.ShowDialog() == DialogResult.OK)
            // показываем модально (пока не залогинился — дальше нельзя)
            {
                string token = authForm.Token;
                // получаем токен из формы

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                //MessageBox.Show(client.DefaultRequestHeaders.Authorization.ToString());
                // добавляем токен ко ВСЕМ последующим запросам
                //MessageBox.Show(token);

                Application.Run(new MainForm(client));
                // запускаем основную форму
            }

            

        }
    }
}