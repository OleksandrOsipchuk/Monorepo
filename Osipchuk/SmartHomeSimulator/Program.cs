using Newtonsoft.Json;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.RoomFiles;
using SmartHomeSimulator.Executer;
using SmartHomeSimulator.HousesFiles;

namespace SmartHomeSimulator // rename (?)
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var jsonpath = @".\houses.json";
            var worker = new FileJsonWorker(jsonpath);
            var handler = new ConsoleHandler();
            var exec = new MenuExecuter(worker, handler);
            try
            {
                await exec.RunMenuAsync();
            }
            catch (FileNotFoundException)
            {
                handler.Write("No json file fould on path you specified." +
                    "Program сreated new file on that path.");
                using (StreamWriter sw = new StreamWriter(jsonpath))
                    JsonConvert.SerializeObject(new List<House>());
            }
            catch (RoomExсeption ex)
            {
                handler.Write($"{ex.Message}");
            }
        }       
    }
}
