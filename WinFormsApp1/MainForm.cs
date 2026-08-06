using Microsoft.Identity.Client;
using SettlementGame.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SettlementGame.Domain.DataWorld;
using static SettlementGame.Domain.WorldService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        //private WorldService _worldService;
        //private WorldCreator _worldCreator;
        private HttpClient _httpClient;
        private bool isBuildingMode = false;
        private bool isDestroyMode = false;
        //private bool isFirstDestroySelection;
        private bool userActionDestroy = false;
        private bool userActionChooseWorker = false;
        private bool userActionChooseBuilding = false;
        private bool isHireMode = false;
        private bool isFireMode = false;
        private bool isAksForNewWorker = false;
        private int chosenWorkerId;
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


        private async Task RefreshDestroyBuildingsComboBox()
        {
            // отправляем GET запрос
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/buildings/GetBuildings");

            // если запрос успешен
            if (response.IsSuccessStatusCode)
            {
                // получаем JSON строку
                //string json =
                //    await response.Content.ReadAsStringAsync();

                //// превращаем JSON в список объектов
                //List<WorldService.BuildingDto>? buildings =
                //    JsonSerializer.Deserialize<List<WorldService.BuildingDto>>(
                //        json,
                //        new JsonSerializerOptions
                //        {
                //            PropertyNameCaseInsensitive = true
                //        });
                List<BuildingDto>? buildings = await response.Content.ReadFromJsonAsync<List<BuildingDto>>();
                // защита от null
                if (buildings == null)
                {
                    return;
                }
                comboBoxDestroyType.SelectedIndexChanged -= comboBoxDestroyType_SelectedIndexChanged;
                // источник данных комбобокса
                comboBoxDestroyType.DataSource = buildings;

                
                // что показывать пользователю
                comboBoxDestroyType.DisplayMember = "info".ToString();



                // что считать значением
                comboBoxDestroyType.ValueMember = "Id";
                comboBoxDestroyType.SelectedIndex = -1;
                comboBoxDestroyType.SelectedIndexChanged += comboBoxDestroyType_SelectedIndexChanged;
            }
        }

        private async Task RefreshPeopleLoayalityBrogressBar()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/world/getPeopleLoayliyDTO"); // запрос к API
            //[HttpGet("getSettlementresourcesListDTO")]
            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                //временно полный json вид
                //json = json.Replace(".", ",");
                JsonNode node = JsonNode.Parse(json);

                // Извлекаем значение и приводим к нужному типу (например, int)
                double amount = (double)node["peopleLoyality"];
                progressBarPeopleLoyality.Value = (int)(amount * 100);

            }
            else // если ошибка запроса
            {
                MessageBox.Show(response.StatusCode.ToString()); // показываем код ошибки
            }
        }

        private async Task RefreshCrownLoayalityBrogressBar()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/world/getCrownLoaylityDTO"); // запрос к API
            //[HttpGet("getSettlementresourcesListDTO")]
            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                //временно полный json вид
                //json = json.Replace(".", ",");
                //progressBarCrownLoyality.Value = (int)(double.Parse(json) * 100);

                //пробуем делать JsonNode по аналогии с progressBarPeopleLoyality
                JsonNode node = JsonNode.Parse(json);
                // Извлекаем значение и приводим к нужному типу (например, int)
                double amount = (double)node["crownLoyality"];
                progressBarCrownLoyality.Value = (int)(amount * 100);
            }
            else // если ошибка запроса
            {
                MessageBox.Show(response.StatusCode.ToString()); // показываем код ошибки
            }
        }

        private async Task RefreshWorkersInfo() // метод обновления информации о рабочих
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/workers/GetWorkersDTO"); // запрос к API

            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку

                JsonDocument workersTreeContainer = JsonDocument.Parse(json); // парсим JSON в дерево, то есть не строку, но структурированный блок как в БД
                //JsonDocument — это контейнер дерева
                JsonElement workersTree = workersTreeContainer.RootElement; // корневой элемент (он же массив)

                textWorkersState.Clear();
                //textWorkersState.Text = json;
                //временно уберем имязависимое красивое отображение
                foreach (JsonElement worker in workersTree.EnumerateArray()) // идем по всем элементам массива
                {
                    int id = worker.GetProperty("id").GetInt32(); // внутри JSON найди поле id и т.д.
                    bool isAlive = worker.GetProperty("isAlive").GetBoolean(); // читаем жив ли
                    int WorkPlaceId = worker.GetProperty("workPlaceId").GetInt32(); //очень тупо искать по имени, которое 10 раз поменяется, но оставим пока так
                    //обращаю, что имя свойства с маленькой буквы
                    double PersonalLoyality = worker.GetProperty("personalLoyality").GetDouble();
                    int PersonalMoney = worker.GetProperty("personalMoney").GetInt32();

                    //int x = worker.GetProperty("x").GetInt32(); 
                    //int y = worker.GetProperty("y").GetInt32();

                    //textWorkersState.AppendText($"Id:{id} X:{x} Y:{y} Alive:{isAlive}\r\n"); // выводим строку
                    textWorkersState.AppendText($"Id:{id} Alive:{isAlive} WorkPlaceId {WorkPlaceId} PersonalLoyality {PersonalLoyality} PersonalMoney {PersonalMoney}\r\n");
                }
            }
            else // если ошибка запроса
            {
                textWorkersState.Text = response.StatusCode.ToString(); // показываем код ошибки
            }
        }

        //лучеше обновлять данные о рабочих как ниже, но из-за вложенности класса Дто все падает, а выносить все Дто в отд.классы ради 1 метода сомнительное решение, так что придется идти тупо по именам
        //private async Task RefreshWorkersInfo()
        //{
        //    HttpResponseMessage response =
        //        await _httpClient.GetAsync("api/workers/GetWorkers");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string json =
        //            await response.Content.ReadAsStringAsync();
        //        //MessageBox.Show(json);
        //        List<WorkerDto>? workers =
        //            JsonSerializer.Deserialize<List<WorldService.WorkerDto>>(json);
        //        MessageBox.Show(workers[0].GetType().FullName);
        //        textWorkersState.Clear();

        //        foreach (WorkerDto worker in workers)
        //        {
        //            textWorkersState.AppendText(
        //                $"Id:{worker.Id} " +
        //                $"Alive:{worker.IsAlive} " +
        //                $"WorkPlaceId:{worker.WorkPlaceId} " +
        //                $"Loyality:{worker.PersonalLoyality} " +
        //                $"Money:{worker.PersonalMoney}" +
        //                Environment.NewLine);
        //        }
        //    }
        //    else
        //    {
        //        textWorkersState.Text =
        //            response.StatusCode.ToString();
        //    }
        //}

        private async Task RefreshBuildingsInfo() // метод обновления информации о рабочих
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/buildings/GetBuildings"); // запрос к API

            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                List<BuildingDto>? buildings = await response.Content.ReadFromJsonAsync<List<BuildingDto>>();

                if (buildings == null)
                {
                    textBuildingsState.Text = "No buildings";
                    return;
                }

                //string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                ////временно полный json вид
                //textBuildingsState.Text = json;

                //JsonDocument buildingsTreeContainer = JsonDocument.Parse(json); // парсим JSON в дерево, то есть не строку, но структурированный блок как в БД
                ////JsonDocument — это контейнер дерева
                //JsonElement buildingsTree = buildingsTreeContainer.RootElement; // корневой элемент (он же массив)

                textBuildingsState.Clear();

                //foreach (JsonElement building in buildingsTree.EnumerateArray()) // идем по всем элементам массива
                foreach (BuildingDto building in buildings)
                {
                    int?id = building.Id;
                    //int id = building.GetProperty("id").GetInt32(); // внутри JSON найди поле id и т.д.
                    //int buildingTypeValue = building.GetProperty("buildingType").GetInt32();
                    //int buildingTypeValue= (int)building.BuildingType;
                    //BuildingType buildingType = (BuildingType)buildingTypeValue;
                    BuildingType buildingType= building.BuildingType;
                    //чтобы , когда нет рабочего, не шло исключение, ставим рабочего-пустышку
                    int?assignedWorkerId = -1;
                    //и проверяем есть ли рил
                    if (building.AssignedWorkerId != -1) { assignedWorkerId = building.AssignedWorkerId; }
                    //if (building.TryGetProperty("assignedWorkerId", out JsonElement workerIdElement))
                    //{
                    //    if (workerIdElement.ValueKind == JsonValueKind.Number)
                    //    {
                    //        assignedWorkerId = workerIdElement.GetInt32();
                    //    }
                    //}
                    //int x = building.GetProperty("x").GetInt32();
                    //int y = building.GetProperty("y").GetInt32();


                    textBuildingsState.AppendText($"Id:{id} BuildingType:{buildingType} AssignedWorkerId {assignedWorkerId}\r\n"); // выводим строку
                }
            }
            else // если ошибка запроса
            {
                textBuildingsState.Text = response.StatusCode.ToString(); // показываем код ошибки
            }
        }

        private async Task RefreshPossibleBuildings()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/buildings/GetPossibleBuildings");

            string json =
                await response.Content.ReadAsStringAsync();

            List<BuildingType>? buildings =
                JsonSerializer.Deserialize<List<BuildingType>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (buildings == null)
                return;

            comboBoxBuildingType.DataSource = buildings;
        }

        private async Task RefreshSettlementResourcesInfo() // метод обновления информации о рабочих
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/world/getSettlementresourcesListDTO"); // запрос к API
            //[HttpGet("getSettlementresourcesListDTO")]
            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                //временно полный json вид
                //textSettlementResourcesState.Clear();
                //textSettlementResourcesState.Text = json;

                JsonDocument SettlementResourcesTreeContainer = JsonDocument.Parse(json); // парсим JSON в дерево, то есть не строку, но структурированный блок как в БД
                //JsonDocument — это контейнер дерева
                JsonElement SettlementResourcesTree = SettlementResourcesTreeContainer.RootElement; // корневой элемент (он же массив)



                foreach (JsonElement SettlementResources in SettlementResourcesTree.EnumerateArray()) // идем по всем элементам массива
                {
                    int resourceTypeValue = SettlementResources.GetProperty("resourceType").GetInt32(); // внутри JSON найди поле id и т.д.
                    int amount = SettlementResources.GetProperty("amount").GetInt32();


                    ResourceType resourceType = (ResourceType)resourceTypeValue;

                    textSettlementResourcesState.AppendText($"{resourceType} {amount}\r\n"); // выводим строку
                    //MessageBox.Show(textSettlementResourcesState.ToString());
                }
            }
            else // если ошибка запроса
            {
                textSettlementResourcesState.Text = response.StatusCode.ToString(); // показываем код ошибки
            }
        }

        private async Task RefreshGameState() // метод обновления информации о рабочих
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/world/getCurrentGameState");
                if (response.IsSuccessStatusCode) // проверяем успешность ответа
                {
                    string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                                                                              //временно полный json вид
                                                                              //textCurrentCrownTask.Clear();
                    int result = int.Parse(json);
                    if (result == -1)
                    {
                        ShowEnd("Simulation failed");
                    }

                    if (result == 1)
                    {
                        ShowEnd("Simulation is successful");
                    }


                }
                else // если ошибка запроса
                {
                    MessageBox.Show(response.StatusCode.ToString()); // показываем код ошибки
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowEnd(string message)
        {
            MessageBox.Show(message, "Info");

            // важно: через UI thread безопасно
            BeginInvoke(new Action(() =>
            {
                Application.Exit();
                //мы не останавливаем весь сервер из-за 1 человека, закрываем онли его винформс 
            }

            ));

        }

        private async Task RefreshCrownTaskInfo() // метод обновления информации о рабочих
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/world/getCurrentCrownTaskDTO");
            if (response.IsSuccessStatusCode) // проверяем успешность ответа
            {
                string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку
                
                textCurrentCrownTask.Clear();

                //временно полный json вид
                //textCurrentCrownTask.Text = json;

                JsonDocument SettlementTaskTreeContainer = JsonDocument.Parse(json); // парсим JSON в дерево, то есть не строку, но структурированный блок как в БД

                JsonElement SettlementTaskTree = SettlementTaskTreeContainer.RootElement; // корневой элемент (он же массив)

                JsonElement resourceType = SettlementTaskTree.GetProperty("resourceType");


                int amount = SettlementTaskTree.GetProperty("amount").GetInt32();

                int ticks = SettlementTaskTree.GetProperty("numberOfTicks").GetInt32();


                //textCurrentCrownTask.AppendText($"{resourceType} {amount} ticks:{ticks}\r\n");
                textCurrentCrownTask.Text = $"{resourceType} {amount} ticks:{ticks}\r\n";

                //трайнем получить из ДТО данные - не срабатывает дессериалзация, возвр.к прошлому варинату.
                //CrownTaskDto? task = JsonSerializer.Deserialize<CrownTaskDto>(json);
                //textCurrentCrownTask.Text = $"{task.ResourceType} {task.Amount} ticks:{task.NumberOfTicks}";

            }
            else // если ошибка запроса
            {
                textCurrentCrownTask.Text = response.StatusCode.ToString(); // показываем код ошибки
            }
        }




        private async void btnCreateWorld_Click(object sender, EventArgs e)
        {
            //int workers = 10;
            //StringContent content = new StringContent(
            //workers.ToString(),
            //Encoding.UTF8,
            //"application/json");
            //HttpResponseMessage response = await _httpClient.PostAsync("api/world/create", content);
            // POST
            //MessageBox.Show(_httpClient.DefaultRequestHeaders.Authorization?.ToString());
            //MessageBox.Show(content.ToString());
            //HttpResponseMessage response =
            //await _httpClient.PostAsync(
            //    "api/world/create",
            //    content);

            HttpResponseMessage response =
            await _httpClient.PostAsync(
                "api/world/create", null);

            //MessageBox.Show(response.StatusCode.ToString());

            string text =
                await response.Content.ReadAsStringAsync();

            //MessageBox.Show(text);


            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();
                await RefreshSettlementResourcesInfo();
                await RefreshPossibleBuildings();
                await RefreshCrownTaskInfo();

                //MessageBox.Show("Мир создан");

            }
            else
            {
                MessageBox.Show("Ошибка запроса");
            }
        }

        private async void btnTick_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = await _httpClient.PutAsync("api/world/tick", null);
            //MessageBox.Show(response.StatusCode.ToString());
            //todo: System.Threading.Tasks.TaskCanceledException: "The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing."
            //передал на подольше
            //пошла авторизация
            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                //MessageBox.Show("Workers OK");
                await RefreshBuildingsInfo();
                //MessageBox.Show("Buildings OK");

                await RefreshSettlementResourcesInfo();
                //MessageBox.Show("Resources OK");
                await RefreshPeopleLoayalityBrogressBar();
                //MessageBox.Show("PeopleLoyality OK");
                RefreshCrownLoayalityBrogressBar();
                //MessageBox.Show("CrownLoyality OK");
                await RefreshCrownTaskInfo();
                //MessageBox.Show("Task OK");
                await RefreshGameState();
                //MessageBox.Show("GameState OK");
                RefreshTicksTillNewWorkers();
                //MessageBox.Show("TicksTillNewWorkersComeInfo OK");
                //MessageBox.Show("Tick выполнен");
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();

                MessageBox.Show(error);
            }
        }

        private async void btnCreateBuilding_Click(object sender, EventArgs e)
        {
            isBuildingMode = true;
            comboBoxBuildingType.Visible = true;
            // далее берём выбранный тип здания из ComboBox
        }

        private async void btnDestroyBuilding_Click(object sender, EventArgs e)
        {
            isDestroyMode = true;

            comboBoxDestroyType.Visible = true;
            userActionDestroy = false;
            RefreshDestroyBuildingsComboBox();
            // берём выбранный тип здания из ComboBox
            userActionDestroy = true;
        }

        private async void btnHireWorker_Click(object sender, EventArgs e)
        {
            isFireMode = false;
            isHireMode = true;
            userActionChooseWorker = false;
            comboBoxChooseWorker.Visible = true;
            await ChooseWorkersComboBox();//присваиваем переменной chosenWorkerId номер выбранного рабочего
            userActionChooseWorker = true;
            comboBoxChooseWorker.Enabled = true;
        }

        private async void btnFireWorker_Click(object sender, EventArgs e)
        {
            isHireMode = false;
            isFireMode = true;
            userActionChooseWorker = false;
            comboBoxChooseWorker.Visible = true;
            await ChooseWorkersComboBox();
            userActionChooseWorker = true;
            comboBoxChooseWorker.Enabled = true;
        }

        private async Task ChooseWorkersComboBox()
        {
            // отправляем GET запрос
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/workers/GetWorkersDTO");

            // если запрос успешен
            if (response.IsSuccessStatusCode)
            {

                // получаем JSON строку
                string json =
                        await response.Content.ReadAsStringAsync();

                // превращаем JSON в список объектов
                List<WorldService.WorkerDTO>? workers =
                    JsonSerializer.Deserialize<List<WorldService.WorkerDTO>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                // защита от null
                if (workers == null)
                {
                    return;
                }
                comboBoxChooseWorker.SelectedIndexChanged -= comboBoxChooseWorker_SelectedIndexChanged;
                // источник данных комбобокса
                comboBoxChooseWorker.DataSource = workers;

                // что показывать пользователю
                comboBoxChooseWorker.DisplayMember = "info";

                // что считать значением
                comboBoxChooseWorker.ValueMember = "Id";
                comboBoxChooseWorker.SelectedIndex = -1;
                comboBoxChooseWorker.SelectedIndexChanged += comboBoxChooseWorker_SelectedIndexChanged;
            }
        }

        private async void comboBoxChooseWorker_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (!isHireMode)
            //    return;

            // получаем выбранный тип
            //BuildingDto selectedBuilding =
            //    (BuildingDto)comboBoxDestroyType.SelectedItem;

            //if (selectedBuilding == null)
            //{
            //    return;
            //}
            // берём id здания
            if (!userActionChooseWorker)
                return;
            if (comboBoxChooseWorker.SelectedIndex < 0)
                return;
            //BuildingDto temp = (BuildingDto)comboBoxDestroyType.SelectedValue;
            //int id = (int)temp.Id;

            chosenWorkerId = (int)comboBoxChooseWorker.SelectedValue;
            if (chosenWorkerId == null)
            {
                MessageBox.Show("У worker нет Id");

                return;
            }

            // выключаем режим 
            //isHireMode = false;
            comboBoxChooseWorker.Enabled = false;

            if (isHireMode == true)
            {
                comboBoxChooseBuildingForWorker.Visible = true;
                comboBoxChooseBuildingForWorker.Enabled = true;
                await ChooseBuildingForWorkersComboBox();
                return;
            }
            if (isFireMode == true)
            {
                isFireMode = false;
                comboBoxChooseWorker.Visible = false;

                //формируем JSON
                string json =
                    $"{{chosenWorkerId}}";

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");


                // отправляем запрос
                HttpResponseMessage response =
                    await _httpClient.PutAsync($"api/workers/{chosenWorkerId}/fire", content);

                if (response.IsSuccessStatusCode)
                {
                    await RefreshWorkersInfo();
                    await RefreshBuildingsInfo();
                    MessageBox.Show("Рабочий уволен");
                }
                else
                    MessageBox.Show(await response.Content.ReadAsStringAsync());
            }
            // скрываем список
            //comboBoxChooseWorker.Visible = false;


            // формируем JSON
            //string json =
            //    $"{{\"buildingType\":{(int)selectedType}}}";

            //StringContent content =
            //    new StringContent(json, Encoding.UTF8, "application/json");


            //// отправляем запрос
            //HttpResponseMessage response =
            //    await _httpClient.DeleteAsync($"api/buildings/{id}");

            //if (response.IsSuccessStatusCode)
            //    MessageBox.Show("Здание уничтожено");
            //else
            //    MessageBox.Show(await response.Content.ReadAsStringAsync());

        }

        private async Task ChooseBuildingForWorkersComboBox()
        {
            // отправляем GET запрос
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/buildings/GetBuildings");

            // если запрос успешен
            if (response.IsSuccessStatusCode)
            {

                // получаем JSON строку
                string json = await response.Content.ReadAsStringAsync();

                // превращаем JSON в список объектов
                List<WorldService.BuildingDto>? buildings =
                    JsonSerializer.Deserialize<List<WorldService.BuildingDto>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                // защита от null
                if (buildings == null)
                {
                    return;
                }
                comboBoxChooseBuildingForWorker.SelectedIndexChanged -= comboBoxChooseBuildingForWorker_SelectedIndexChanged;
                // источник данных комбобокса
                comboBoxChooseBuildingForWorker.DataSource = buildings;

                // что показывать пользователю
                comboBoxChooseBuildingForWorker.DisplayMember = "info".ToString();
                
                // что считать значением
                comboBoxChooseBuildingForWorker.ValueMember = "Id";
                comboBoxChooseBuildingForWorker.SelectedIndex = -1;
                comboBoxChooseBuildingForWorker.SelectedIndexChanged += comboBoxChooseBuildingForWorker_SelectedIndexChanged;
                userActionChooseBuilding = true;

            }
        }

        private async void comboBoxChooseBuildingForWorker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isHireMode)
                return;

            // получаем выбранный тип
            //BuildingDto selectedBuilding =
            //    (BuildingDto)comboBoxDestroyType.SelectedItem;

            //if (selectedBuilding == null)
            //{
            //    return;
            //}
            // берём id здания
            if (!userActionChooseBuilding)
                return;
            if (comboBoxChooseBuildingForWorker.SelectedIndex < 0)
                return;
            //BuildingDto temp = (BuildingDto)comboBoxDestroyType.SelectedValue;
            //int id = (int)temp.Id;

            int chosenBuildingId = (int)comboBoxChooseBuildingForWorker.SelectedValue;
            if (chosenBuildingId == null)
            {
                MessageBox.Show("У building нет Id");

                return;
            }

            // выключаем режим 
            isHireMode = false;

            comboBoxChooseBuildingForWorker.Enabled = false;
            comboBoxChooseWorker.Visible = false;
            comboBoxChooseBuildingForWorker.Visible = false;


            //формируем JSON
            string json =
                $"{{\"workerId\":{chosenWorkerId},\"id\":{chosenBuildingId}}}";

            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");


            // отправляем запрос
            HttpResponseMessage response =
                await _httpClient.PutAsync($"api/workers/hire", content);

            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();
                MessageBox.Show("Рабочий нанят");
            }
            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }


        private async void comboBoxBuildingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // если мы не в режиме строительства — ничего не делаем. можно и изящнее сделать, ну да ладно
            if (!isBuildingMode)
                return;

            // получаем выбранный тип
            BuildingType selectedType =
                (BuildingType)comboBoxBuildingType.SelectedItem;

            // выключаем режим 
            isBuildingMode = false;

            // скрываем список
            comboBoxBuildingType.Visible = false;

            // формируем JSON
            string json =
                $"{{\"buildingType\":{(int)selectedType}}}";

            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");

            // отправляем запрос
            HttpResponseMessage response =
                await _httpClient.PostAsync("api/buildings/CreateBuilding", content);


            if (response.IsSuccessStatusCode)
            {
                await RefreshBuildingsInfo();
                await RefreshSettlementResourcesInfo();
                MessageBox.Show("Здание построено");
            }

            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }

        private async void comboBoxDestroyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // если мы не в режиме удаления — ничего не делаем
            if (!isDestroyMode)
                return;

            // получаем выбранный тип
            //BuildingDto selectedBuilding =
            //    (BuildingDto)comboBoxDestroyType.SelectedItem;

            //if (selectedBuilding == null)
            //{
            //    return;
            //}
            // берём id здания
            if (!userActionDestroy)
                return;
            if (comboBoxDestroyType.SelectedIndex < 0)
                return;
            //BuildingDto temp = (BuildingDto)comboBoxDestroyType.SelectedValue;
            //int id = (int)temp.Id;

            int id = (int)comboBoxDestroyType.SelectedValue;
            if (id == null)
            {
                MessageBox.Show("У здания нет Id");

                return;
            }

            // выключаем режим 
            isDestroyMode = false;

            // скрываем список
            comboBoxBuildingType.Visible = false;


            // формируем JSON
            //string json =
            //    $"{{\"buildingType\":{(int)selectedType}}}";

            //StringContent content =
            //    new StringContent(json, Encoding.UTF8, "application/json");


            // отправляем запрос
            HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/buildings/{id}");
            comboBoxDestroyType.Visible = false;
            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();
                MessageBox.Show("Здание уничтожено");
            }
            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());

        }

        private async void btnAskForNewWorkers_Click(object sender, EventArgs e)
        {
            //isFireMode = false;
            isAksForNewWorker = true;
            comboBoxAmountOfWorkers.Visible = true;
            await ChooseAmountOfNewWorkers();
            
        }

        private async Task ChooseAmountOfNewWorkers()
        {
            int[] numbers = new int[9]; // создаем массив на 9 элементов

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1; // записываем числа от 1 до 9
            }
            //comboBoxAmountOfWorkers.Items.AddRange(numbers); плохо - не работает с инт, нуждно в объект преобразовывать?

            comboBoxAmountOfWorkers.Items.Clear();

            for (int i = 1; i <= 9; i++)
            {
                comboBoxAmountOfWorkers.Items.Add(i);
            }

            comboBoxAmountOfWorkers.SelectedIndexChanged += comboBoxAmountOfWorkers_SelectedIndexChanged;
         }

        private async void comboBoxAmountOfWorkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // если мы не в режиме строительства — ничего не делаем. можно и изящнее сделать, ну да ладно
            if (!isAksForNewWorker)
                return;

            // получаем выбранный тип
            int number = (int)comboBoxAmountOfWorkers.SelectedItem;

            // выключаем режим 
            isAksForNewWorker = false;

                 
            // формируем JSON
            string json =
                $"{{\"Number\":{number}}}";

            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");

            // отправляем запрос
            HttpResponseMessage response =
                await _httpClient.PostAsync("api/workers/CreateOrderForNewWorkers", content);


            if (response.IsSuccessStatusCode)
            {
                //await RefreshBuildingsInfo();
                //await RefreshSettlementResourcesInfo();
                MessageBox.Show("Работники заказаны");
                RefreshTicksTillNewWorkers();


                comboBoxAmountOfWorkers.Visible = false;
            }

            else
                MessageBox.Show(await response.Content.ReadAsStringAsync());
        }

        private async Task RefreshTicksTillNewWorkers() // метод обновления информации о рабочих
        {   

            HttpResponseMessage response = await _httpClient.GetAsync("api/workers/GetTicksTillNewWorkers"); // запрос к API
            if (!response.IsSuccessStatusCode)
            {
                textWorkersState.Text = response.StatusCode.ToString();
                return;
            }

            List<WorkerOrderDTO>? orders = await response.Content.ReadFromJsonAsync<List<WorkerOrderDTO>>();
            textTicksTillNewWorkers.Clear();

            if (orders == null)
                return;

            foreach (WorkerOrderDTO order in orders)
            {
                textTicksTillNewWorkers.AppendText(
                    $"{order.Amount} in:{order.TicksLeft}\r\n");
            }
            //if (response.IsSuccessStatusCode) // проверяем успешность ответа
            //{
            //    string json = await response.Content.ReadAsStringAsync(); // получаем JSON строку

            //    JsonDocument WorkerOrderContainer = JsonDocument.Parse(json); // парсим JSON в дерево, то есть не строку, но структурированный блок как в БД
            //    JsonElement WorkerOrderTree = WorkerOrderContainer.RootElement; // корневой элемент (он же массив)

            //    textTicksTillNewWorkers.Clear();
            //    //textTicksTillNewWorkers.Text = json;

            //    foreach (JsonElement workerOrder in WorkerOrderTree.EnumerateArray()) // идем по всем элементам массива
            //    {
            //        int amount = workerOrder.GetProperty("amount").GetInt32(); // внутри JSON найди поле id и т.д.
            //        int ticks = workerOrder.GetProperty("ticksLeft").GetInt32(); //очень тупо искать по имени, которое 10 раз поменяется, но оставим пока так

            //        //textWorkersState.AppendText($"Id:{id} X:{x} Y:{y} Alive:{isAlive}\r\n"); // выводим строку
            //        textTicksTillNewWorkers.AppendText($"{amount} in:{ticks}\r\n");

            //        //}
            //    }
            //}
            //else // если ошибка запроса
            //{
            //    textWorkersState.Text = response.StatusCode.ToString(); // показываем код ошибки
            //}
        }





    }
}
