using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SettlementGame
{
    internal class Program
    {
        public List<Worker> workers = new List<Worker>();
        public static DateOnly currentDate = new DateOnly();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //для теста

            //этапы: 1) старт поселения - создание рабочих начальных и приказов метополии
            //2) запуск цикла производства и изм.состояния рабочих, их кол-ва, шкал довольства игроком, кол-ва материалов/зданий
            //3)мб случайное событие - вкл.позже.


        }
        public void DateChanges()
        {
            currentDate.AddDays(1);
        }
        public void CreateSettlement(int numberOfWorkers)
        {
            for (int i=0; i<numberOfWorkers;i++)
            {
                Worker worker= new Worker();
            }
        }


    }
}
    

