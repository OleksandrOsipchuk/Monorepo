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
            var exec = new HouseMenuHandler(worker, handler);
            try
            {
                await exec.RunMenuAsync();
            }
            catch (FileNotFoundException)
            {
                await handler.WriteAsync("No json file fould on path you specified." +
                    "Program сreated new file on that path.");
                await worker.WriteAsync(new List<House>());
            }
            catch (InvalidRoomTypeException ex)
            {
                await handler.WriteAsync($"Invalid room type: {ex.Message}");
            }
            catch (InvalidRoomValueException ex)
            {
                await handler.WriteAsync($"Invalid room value: {ex.Message}");
            }
            catch (FormatException)
            {
                await handler.WriteAsync("Wrong input format!");
            }
            catch (OverflowException)
            {
                await handler.WriteAsync("Number is too long. Try again!");
            }
        }
    }
}
