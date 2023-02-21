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
                worker.WriteAsync(new List<House>());
            }
            catch (RoomExсeption ex)
            {
                handler.Write($"Room exception occured: {ex.Message}");
            }
            catch (FormatException)
            {
                handler.Write("Wrong input format!");
            }
            catch (OverflowException)
            {
                handler.Write("Number is too long. Try again!");
            }
        }
    }
}
