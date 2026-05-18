using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementGame.Domain;
using System.Windows.Forms;


namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        //private WorldService _worldService;
        //private WorldCreator _worldCreator;
        private HttpClient _httpClient;
        // используем тот же HttpClient (с токеном)
        //private System.Windows.Forms.Button btnTick;

        public MainForm(HttpClient client1)
        {
            InitializeComponent();// создаёт кнопки,поля эт цэтра
                                  //_httpClient = new HttpClient();
            _httpClient = client1;

            //DataWorld world=_worldCreator.CreateWorld();
            //btnTick = new Button();
            //btnTick.Text = "Tick";
            //btnTick.Left = 150;
            //btnTick.Top = 50;


            //this.Controls.Add(this.btnTick);
        }

        //private async void btnTick_Click(object sender, EventArgs e)
        //{ // async — чтобы UI не зависал
        //    try
        //    {
        //        var response = await _httpClient.PostAsync("tick", null); // POST запрос как в Postman // null — тело не передаём
        //        if (response.IsSuccessStatusCode) { MessageBox.Show("Tick выполнен"); }
        //        else { MessageBox.Show("Ошибка: " + response.StatusCode); }
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message); }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnCreateWorld_Click(object sender, EventArgs e)
        {
            int workers = 10;
            StringContent content = new StringContent(
            workers.ToString(),
            Encoding.UTF8,
            "application/json");
            //HttpResponseMessage response = await _httpClient.PostAsync("api/world/create", content);
            // POST
            //MessageBox.Show(_httpClient.DefaultRequestHeaders.Authorization?.ToString());
            //MessageBox.Show(content.ToString());
            HttpResponseMessage response =
            await _httpClient.PostAsync(
                "api/world/create",
                content);
            
            MessageBox.Show(response.StatusCode.ToString());

            string text =
                await response.Content.ReadAsStringAsync();

            MessageBox.Show(text);


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Мир создан");
            }
            else
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        private async void btnTick_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("api/world/tick", null);
            //todo: System.Threading.Tasks.TaskCanceledException: "The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing."
            //пошла авторизация
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Tick выполнен");
            }
            else
            {
                MessageBox.Show("Ошибка запроса");
            }
        }
    }
}
