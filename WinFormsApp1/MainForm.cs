using SettlementGame.Domain;
using System.Net.Http.Json;
using System.Text;
using static SettlementGame.Domain.WorldService;
using static SettlementGame.Web.Controllers.WorkersController;

namespace WinFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly HttpClient _httpClient;

        private bool isBuildingMode = false;
        private bool isDestroyMode = false;
        private bool userActionDestroy = false;
        private bool userActionChooseWorker = false;
        private bool userActionChooseBuilding = false;
        private bool isHireMode = false;
        private bool isFireMode = false;
        private bool isAksForNewWorker = false;

        private int chosenWorkerId;


        public MainForm(HttpClient client1)
        {
            InitializeComponent();

            _httpClient = client1;

            // The world has not been Created yet.
            // Only the Create World button is available.
            SetGameControlsVisible(false);
        }

        private void SetGameControlsVisible(bool visible)
        {
            btnTick.Visible = visible;
            btnCreateBuilding.Visible = visible;
            btnDestroyBuilding.Visible = visible;
            btnHireWorker.Visible = visible;
            btnFireWorker.Visible = visible;
            btnAskForNewWorkers.Visible = visible;

            comboBoxBuildingType.Visible = false;
            comboBoxDestroyType.Visible = false;
            comboBoxChooseWorker.Visible = false;
            comboBoxChooseBuildingForWorker.Visible = false;
            comboBoxAmountOfWorkers.Visible = false;
        }


        private async Task RefreshDestroyBuildingsComboBox()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/buildings/GetBuildings");

            if (response.IsSuccessStatusCode)
            {
                List<BuildingDto>? buildings =
                    await response.Content.ReadFromJsonAsync<List<BuildingDto>>();

                if (buildings == null)
                {
                    return;
                }

                comboBoxDestroyType.SelectedIndexChanged -=
                    comboBoxDestroyType_SelectedIndexChanged;

                comboBoxDestroyType.DataSource = buildings;

                // Property displayed to the user.
                comboBoxDestroyType.DisplayMember = "info";

                // Property used as the selected value.
                comboBoxDestroyType.ValueMember = "Id";

                comboBoxDestroyType.SelectedIndex = -1;

                comboBoxDestroyType.SelectedIndexChanged +=
                    comboBoxDestroyType_SelectedIndexChanged;
            }
        }


        private async Task RefreshPeopleLoayalityBrogressBar()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/world/GetPeopleLoayliyDto");

            if (response.IsSuccessStatusCode)
            {
                PeopleLoayalityDto? peopleLoyaltyDto =
                    await response.Content.ReadFromJsonAsync<PeopleLoayalityDto>();

                if (peopleLoyaltyDto == null)
                {
                    return;
                }

                double amount = peopleLoyaltyDto.PeopleLoyalty;

                progressBarPeopleLoyalty.Value = (int)(amount * 100);
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }


        private async Task RefreshCrownLoayalityBrogressBar()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/world/GetCrownLoyalityDto");

            if (response.IsSuccessStatusCode)
            {
                CrownLoayalityDto? crownLoyaltyDto =
                    await response.Content.ReadFromJsonAsync<CrownLoayalityDto>();

                if (crownLoyaltyDto == null)
                {
                    return;
                }

                double amount = crownLoyaltyDto.CrownLoyalty;

                progressBarCrownLoyalty.Value = (int)(amount * 100);
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
        }


        private async Task RefreshWorkersInfo()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/workers/GetWorkersDto");

            if (response.IsSuccessStatusCode)
            {
                textWorkersState.Clear();

                List<WorkerDto>? workersDto =
                    await response.Content.ReadFromJsonAsync<List<WorkerDto>>();

                if (workersDto == null)
                {
                    return;
                }

                foreach (WorkerDto workerDto in workersDto)
                {
                    int id = workerDto.Id;
                    bool isAlive = workerDto.IsAlive;
                    int workPlaceId = workerDto.WorkPlaceId;
                    double personalLoyalty = workerDto.PersonalLoyalty;
                    int personalMoney = workerDto.PersonalMoney;

                    textWorkersState.AppendText(
                        $"Id:{id} " +
                        $"Alive:{isAlive} " +
                        $"WorkPlaceId:{workPlaceId} " +
                        $"PersonalLoyalty:{personalLoyalty} " +
                        $"PersonalMoney:{personalMoney}\r\n");
                }
            }
            else
            {
                textWorkersState.Text =
                    response.StatusCode.ToString();
            }
        }


        private async Task RefreshBuildingsInfo()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync("api/buildings/GetBuildings");

            if (response.IsSuccessStatusCode)
            {
                List<BuildingDto>? buildings =
                    await response.Content.ReadFromJsonAsync<List<BuildingDto>>();

                if (buildings == null)
                {
                    textBuildingsState.Text = "No buildings";
                    return;
                }

                textBuildingsState.Clear();

                foreach (BuildingDto building in buildings)
                {
                    int? id = building.Id;

                    BuildingType buildingType =
                        building.BuildingType;

                    // If there is no assigned worker,
                    // display -1 instead of a nullable value.
                    int? assignedWorkerId = -1;

                    if (building.AssignedWorkerId != -1)
                    {
                        assignedWorkerId = building.AssignedWorkerId;
                    }

                    textBuildingsState.AppendText(
                        $"Id:{id} " +
                        $"BuildingType:{buildingType} " +
                        $"AssignedWorkerId:{assignedWorkerId}\r\n");
                }
            }
            else
            {
                textBuildingsState.Text =
                    response.StatusCode.ToString();
            }
        }


        private async Task RefreshPossibleBuildings()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/buildings/GetPossibleBuildings");

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
                return;
            }

            List<BuildingType>? buildingTypeList =
                await response.Content.ReadFromJsonAsync<List<BuildingType>>();

            if (buildingTypeList == null)
            {
                return;
            }

            comboBoxBuildingType.DataSource =
                buildingTypeList;
        }


        private async Task RefreshSettlementResourcesInfo()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/world/GetSettlementresourcesListDto");

            if (response.IsSuccessStatusCode)
            {
                textSettlementResourcesState.Clear();

                List<Resource>? resourceList =
                    await response.Content.ReadFromJsonAsync<List<Resource>>();

                if (resourceList == null)
                {
                    return;
                }

                foreach (Resource resource in resourceList)
                {
                    textSettlementResourcesState.AppendText(
                        $"{resource.ResourceType} {resource.Amount}\r\n");
                }
            }
            else
            {
                textSettlementResourcesState.Text =
                    response.StatusCode.ToString();
            }
        }


        private async Task RefreshGameState()
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync(
                        "api/world/GetCurrentGameState");

                if (response.IsSuccessStatusCode)
                {
                    int result =
                        await response.Content.ReadFromJsonAsync<int>();

                    if (result == -1)
                    {
                        ShowEnd("Simulation failed");
                    }

                    if (result == 1)
                    {
                        ShowEnd("Simulation is successful");
                    }
                }
                else
                {
                    MessageBox.Show(response.StatusCode.ToString());
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

            // Close only the current WinForms application.
            // The ASP.NET server continues running.
            BeginInvoke(new Action(() =>
            {
                Application.Exit();
            }));
        }


        private async Task RefreshCrownTaskInfo()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/world/GetCurrentCrownTaskDto");

            if (response.IsSuccessStatusCode)
            {
                CrownTaskDto? crownTaskDto =
                    await response.Content.ReadFromJsonAsync<CrownTaskDto>();

                if (crownTaskDto == null)
                {
                    return;
                }

                textCurrentCrownTask.Text =
                    $"{crownTaskDto.ResourceType} " +
                    $"{crownTaskDto.Amount} " +
                    $"Ticks:{crownTaskDto.NumberOfTicks}\r\n";
            }
            else
            {
                textCurrentCrownTask.Text =
                    response.StatusCode.ToString();
            }
        }


        private async void btnCreateWorld_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response =
                await _httpClient.PostAsync(
                    "api/world/Create",
                    null);

            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();
                await RefreshSettlementResourcesInfo();
                await RefreshPossibleBuildings();
                await RefreshCrownTaskInfo();

                SetGameControlsVisible(true);
            }
            else
            {
                MessageBox.Show(
                    await response.Content.ReadAsStringAsync());
            }
        }


        private async void btnTick_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response =
                await _httpClient.PutAsync(
                    "api/world/Tick",
                    null);

            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();
                await RefreshSettlementResourcesInfo();
                await RefreshPeopleLoayalityBrogressBar();
                await RefreshCrownLoayalityBrogressBar();
                await RefreshCrownTaskInfo();
                await RefreshGameState();
                await RefreshTicksTillNewWorkers();
            }
            else
            {
                string error =
                    await response.Content.ReadAsStringAsync();

                MessageBox.Show(error);
            }
        }


        private async void btnCreateBuilding_Click(object sender, EventArgs e)
        {
            isBuildingMode = true;

            comboBoxBuildingType.Visible = true;
        }


        private async void btnDestroyBuilding_Click(object sender, EventArgs e)
        {
            isDestroyMode = true;

            comboBoxDestroyType.Visible = true;

            userActionDestroy = false;

            await RefreshDestroyBuildingsComboBox();

            userActionDestroy = true;
        }


        private async void btnHireWorker_Click(object sender, EventArgs e)
        {
            isFireMode = false;
            isHireMode = true;

            userActionChooseWorker = false;

            comboBoxChooseWorker.Visible = true;

            await ChooseWorkersComboBox();

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
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/workers/GetWorkersDto");

            if (response.IsSuccessStatusCode)
            {
                List<WorkerDto>? workerDtoList =
                    await response.Content.ReadFromJsonAsync<List<WorkerDto>>();

                if (workerDtoList == null)
                {
                    return;
                }

                comboBoxChooseWorker.SelectedIndexChanged -=
                    comboBoxChooseWorker_SelectedIndexChanged;

                comboBoxChooseWorker.DataSource =
                    workerDtoList;

                comboBoxChooseWorker.DisplayMember =
                    "info";

                comboBoxChooseWorker.ValueMember =
                    "Id";

                comboBoxChooseWorker.SelectedIndex = -1;

                comboBoxChooseWorker.SelectedIndexChanged +=
                    comboBoxChooseWorker_SelectedIndexChanged;
            }
        }


        private async void comboBoxChooseWorker_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            if (!userActionChooseWorker)
            {
                return;
            }

            if (comboBoxChooseWorker.SelectedIndex < 0)
            {
                return;
            }

            // Save the selected worker ID.
            // It will be used later when the user selects a building.
            chosenWorkerId = (int)comboBoxChooseWorker.SelectedValue;

            // Disable the worker selector after the worker has been chosen.
            comboBoxChooseWorker.Enabled = false;

            if (isHireMode == true)
            {
                // The selected worker is stored in chosenWorkerId.
                // Now the user has to select a building for this worker.
                comboBoxChooseBuildingForWorker.Visible = true;
                comboBoxChooseBuildingForWorker.Enabled = true;

                await ChooseBuildingForWorkersComboBox();

                return;
            }

            if (isFireMode == true)
            {
                isFireMode = false;
                comboBoxChooseWorker.Visible = false;

                // Worker ID is already stored in chosenWorkerId.
                // The API receives it from the URL: /api/workers/{id}/fire.
                HttpResponseMessage response =
                    await _httpClient.PutAsync(
                        $"api/workers/{chosenWorkerId}/fire",
                        null);

                if (response.IsSuccessStatusCode)
                {
                    await RefreshWorkersInfo();
                    await RefreshBuildingsInfo();

                    MessageBox.Show("Рабочий уволен");
                }
                else
                {
                    MessageBox.Show(
                        await response.Content.ReadAsStringAsync());
                }
            }
        }


        private async Task ChooseBuildingForWorkersComboBox()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/buildings/GetBuildings");

            if (response.IsSuccessStatusCode)
            {
                List<BuildingDto>? buildingDtoList =
                    await response.Content.ReadFromJsonAsync<List<BuildingDto>>();

                if (buildingDtoList == null)
                {
                    return;
                }

                comboBoxChooseBuildingForWorker.SelectedIndexChanged -=
                    comboBoxChooseBuildingForWorker_SelectedIndexChanged;

                comboBoxChooseBuildingForWorker.DataSource =
                    buildingDtoList;

                comboBoxChooseBuildingForWorker.DisplayMember =
                    "info";

                comboBoxChooseBuildingForWorker.ValueMember =
                    "Id";

                comboBoxChooseBuildingForWorker.SelectedIndex = -1;

                comboBoxChooseBuildingForWorker.SelectedIndexChanged +=
                    comboBoxChooseBuildingForWorker_SelectedIndexChanged;

                userActionChooseBuilding = true;
            }
        }


        private async void comboBoxChooseBuildingForWorker_SelectedIndexChanged(
    object sender,
    EventArgs e)
        {
            if (!isHireMode)
            {
                return;
            }

            if (!userActionChooseBuilding)
            {
                return;
            }

            if (comboBoxChooseBuildingForWorker.SelectedIndex < 0)
            {
                return;
            }

            int chosenBuildingId =
                (int)comboBoxChooseBuildingForWorker.SelectedValue;

            // The worker was selected in the previous step
            // and is stored in chosenWorkerId.

            isHireMode = false;

            comboBoxChooseBuildingForWorker.Enabled = false;

            comboBoxChooseWorker.Visible = false;
            comboBoxChooseBuildingForWorker.Visible = false;

            HireWorkerRequest request = new HireWorkerRequest
            {
                WorkerId = chosenWorkerId,
                BuildingId = chosenBuildingId
            };

            JsonContent content =
                JsonContent.Create(request);

            HttpResponseMessage response =
                await _httpClient.PutAsync(
                    "api/workers/hire",
                    content);

            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();

                MessageBox.Show("Рабочий нанят");
            }
            else
            {
                MessageBox.Show(
                    await response.Content.ReadAsStringAsync());
            }
        }


        private async void comboBoxBuildingType_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (!isBuildingMode)
            {
                return;
            }

            if (comboBoxBuildingType.SelectedIndex < 0)
            {
                return;
            }

            BuildingType selectedType =
                (BuildingType)comboBoxBuildingType.SelectedItem;

            isBuildingMode = false;

            comboBoxBuildingType.Visible = false;

            // Create the request object instead of manually constructing JSON.
            var request = new
            {
                BuildingType = selectedType
            };

            JsonContent content =
                JsonContent.Create(request);

            HttpResponseMessage response =
                await _httpClient.PostAsync(
                    "api/buildings/CreateBuilding",
                    content);

            if (response.IsSuccessStatusCode)
            {
                await RefreshBuildingsInfo();
                await RefreshSettlementResourcesInfo();

                MessageBox.Show("Здание построено");
            }
            else
            {
                MessageBox.Show(
                    await response.Content.ReadAsStringAsync());
            }
        }


        private async void comboBoxDestroyType_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (!isDestroyMode)
            {
                return;
            }

            if (!userActionDestroy)
            {
                return;
            }

            if (comboBoxDestroyType.SelectedIndex < 0)
            {
                return;
            }

            int id =
                (int)comboBoxDestroyType.SelectedValue;

            isDestroyMode = false;

            comboBoxDestroyType.Visible = false;

            HttpResponseMessage response =
                await _httpClient.DeleteAsync(
                    $"api/buildings/{id}");

            if (response.IsSuccessStatusCode)
            {
                await RefreshWorkersInfo();
                await RefreshBuildingsInfo();

                MessageBox.Show("Здание уничтожено");
            }
            else
            {
                MessageBox.Show(
                    await response.Content.ReadAsStringAsync());
            }
        }


        private async void btnAskForNewWorkers_Click(
            object sender,
            EventArgs e)
        {
            isAksForNewWorker = true;

            comboBoxAmountOfWorkers.Visible = true;

            await ChooseAmountOfNewWorkers();
        }


        private async Task ChooseAmountOfNewWorkers()
        {
            // Important:
            // remove the handler first because this method can be called
            // more than once during the lifetime of the form.
            comboBoxAmountOfWorkers.SelectedIndexChanged -=
                comboBoxAmountOfWorkers_SelectedIndexChanged;

            comboBoxAmountOfWorkers.Items.Clear();

            for (int i = 1; i <= 9; i++)
            {
                comboBoxAmountOfWorkers.Items.Add(i);
            }

            comboBoxAmountOfWorkers.SelectedIndex = -1;

            comboBoxAmountOfWorkers.SelectedIndexChanged +=
                comboBoxAmountOfWorkers_SelectedIndexChanged;
        }


        private async void comboBoxAmountOfWorkers_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (!isAksForNewWorker)
            {
                return;
            }

            if (comboBoxAmountOfWorkers.SelectedIndex < 0)
            {
                return;
            }

            int number =
                (int)comboBoxAmountOfWorkers.SelectedItem;

            // Disable the mode before sending the request.
            // This prevents accidental repeated orders.
            isAksForNewWorker = false;

            comboBoxAmountOfWorkers.Visible = false;

            // Create JSON automatically.
            // System.Net.Http.Json serializes the anonymous object into:
            // {"Number":3}
            var request = new
            {
                Number = number
            };

            JsonContent content =
                JsonContent.Create(request);

            HttpResponseMessage response =
                await _httpClient.PostAsync(
                    "api/workers/CreateOrderForNewWorkers",
                    content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Работники заказаны");

                await RefreshTicksTillNewWorkers();
            }
            else
            {
                MessageBox.Show(
                    await response.Content.ReadAsStringAsync());
            }
        }


        private async Task RefreshTicksTillNewWorkers()
        {
            HttpResponseMessage response =
                await _httpClient.GetAsync(
                    "api/workers/GetTicksTillNewWorkers");

            if (!response.IsSuccessStatusCode)
            {
                textWorkersState.Text =
                    response.StatusCode.ToString();

                return;
            }

            List<WorkerOrderDto>? orders =
                await response.Content.ReadFromJsonAsync<List<WorkerOrderDto>>();

            textTicksTillNewWorkers.Clear();

            if (orders == null)
            {
                return;
            }

            foreach (WorkerOrderDto order in orders)
            {
                textTicksTillNewWorkers.AppendText(
                    $"{order.Amount} in:{order.TicksLeft}\r\n");
            }
        }
    }
}