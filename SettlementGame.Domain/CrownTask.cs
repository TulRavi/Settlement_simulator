using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class CrownTask
    {
        public AnyResource Resource { get; set; }
        public int NumberOfTicks { get; set; }
        public double LoyalityCounter { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        private GameDbContext _dbContext;

        
        //private GameDbContext _dbContext;
        //public GameDbContext DbContext
        //{
        //    get => _dbContext;
        //    set => _dbContext = value;
        //}

        public CrownTask(AnyResource resource,int numberOfTicks,double loyalityCounter)
        {
            Resource = resource;               
            NumberOfTicks = numberOfTicks;     
            LoyalityCounter = loyalityCounter; 
            
        }
        public CrownTask() { }

        public static bool IsCompleted(DataWorld world)
        {
            AnyResource reqAnyResource = world.CurrentCrownTask.Resource;
            if (world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount >= reqAnyResource.Amount) {
                //world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount = world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount - reqAnyResource.Amount;
                return true;}
            else { return false; }
        }
        public static CrownTask CreateNewCrownTack(DataWorld world, GameDbContext dbContext)
        {
            Random random = new Random();
            int numberOfResourses = Enum.GetValues(typeof(ResourceType)).Length;
            int selecteResourceNumber = random.Next(numberOfResourses);
            //int selecteResourceNumber = world.tempTpCheck;
            ResourceType requestedResourceType = (ResourceType)selecteResourceNumber;
            int numberOfWorkers = dbContext.Workers.Count();
            //BuildingType buildingType=world.PossibleBuildingList.Find(x => x.GetType == Id)
            int nubmerOfBuildings = Enum.GetValues(typeof(BuildingType)).Length;
            bool isPossibleTask = false;
            CrownTask tempCrownTask = new CrownTask();

            //исключаем таверну и пр.здания не произв.ресурсы
            //var buildingTypes = Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>().Where(b =>BuildingCatalog.GetProductForCreation(b).Outputs.Count > 0).ToList();

            for(int x = 0; x < nubmerOfBuildings; x++)//перебирем здания
            {
                BuildingType buildingType = (BuildingType)x;//поочередно
                //поочередно
                BuildingCatalog tempBuildingCatalog = BuildingCatalog.GetProductForCreation(buildingType);
                //получаем каталог производимых ресурсов и ищем, есть ли в нем нужный нам
                if (tempBuildingCatalog.Outputs.FirstOrDefault(x => x.ResourceType == requestedResourceType) != null)
                {
                    //если нашли, считаем сколько можно произвести с текущими рабочими
                    int productionOfOneBuilding = tempBuildingCatalog.Outputs.FirstOrDefault(x => x.ResourceType == requestedResourceType).Amount;
                    int productionOfAllPossibleBuildings = productionOfOneBuilding * numberOfWorkers;
                    int amountCounter = random.Next(3, 9);
                    //вводим дабл для деления, ибо при делении инт на инт резтат будет инт.
                    double loyalityCounter = amountCounter / world.denominator;
                    //int reqAmount = random.Next(productionOfAllPossibleBuildings*amountCounter);
                    int reqAmount = productionOfAllPossibleBuildings * amountCounter;
                    //чтобы не было уберпросто, было логино и подталкивало к развию, мы просим произвести больше, чем есть
                    if (world.SettlementResourceList.Find(x => x.ResourceType == requestedResourceType).Amount > reqAmount)
                    {
                        reqAmount = world.SettlementResourceList.Find(x => x.ResourceType == requestedResourceType).Amount + reqAmount;
                        loyalityCounter = loyalityCounter + 0.05;
                        //подумать над логикой, мб это лишает мотивации создавать запас. корректируем лоялитиКаунтером
                    }
                    AnyResource resourse = new AnyResource(requestedResourceType, reqAmount);
                    int numberOfTicks = reqAmount / productionOfAllPossibleBuildings + 5;
                    isPossibleTask = true;
                    tempCrownTask = new CrownTask(resourse, numberOfTicks, loyalityCounter);
                    //world.tempTpCheck++;
                    break;
                }
            }
            if (isPossibleTask == false)
            {
                throw new ArgumentException("Невозможно создать задaние");
            }
            return tempCrownTask;
            //BuildingType buildingType=new BuildingType();

        } } }
    

