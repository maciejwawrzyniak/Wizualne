using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DAOLibraryName"];
            BLC.BLC blc = new BLC.BLC(libraryName);

            foreach(IProducer producer in blc.GetProducers()) 
            {
                Console.WriteLine($"{producer.ID}: {producer.Name}");
            }
            Console.WriteLine("______________");

            foreach (IMonitor monitor in blc.GetMonitors())
            {
                Console.WriteLine($"{monitor.ID}: {monitor.Producer.Name} {monitor.Name} {monitor.Diagonal} {monitor.Matrix}");
            }
            Console.WriteLine("______________");
        }
    }
}
