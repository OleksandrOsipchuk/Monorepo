using SmartHomeSimulator.AdditionalFiles;
using SmartHomeSimulator.Builder.Directors;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.RoomFiles;

namespace SmartHomeSimulator.Executer
{
    public class RoomMenuHandler : BaseMenuHandler
    {
        public static async Task ManageRooms(List<Room> rooms, IIOHandler handler)
        {
            while (true)
            {
                handler.Clear();
                handler.ChangeForegroundColor(ConsoleColor.Red);
                await handler.WriteAsync("MANAGING.\nIf you want to remove room, write its number.");
                handler.ResetColor();
                var option = await GetOption<Room>(rooms, handler, "Add new room.");
                if (option < rooms.Count + 1) rooms.Remove(rooms[option - 1]);
                else if (option == rooms.Count + 1)
                {
                    var builder = new RoomBuilder();
                    RoomDirectorFactory factory = new RoomDirectorFactory();
                    IRoomDirector director = factory.GetRoomDirector(await GetRoomType(), builder);
                    await director.Build(handler);
                    Room room = builder.GetRoom();
                    rooms.Add(room);
                }
                else break;
            }
            async Task<RoomType> GetRoomType()
            {
                await handler.WriteAsync("Choose room type: ");
                foreach (var type in Enum.GetNames(typeof(RoomType)))
                    await handler.WriteAsync(type);
                if (Enum.TryParse<RoomType>(await handler.ReadAsync(), true, out var result))
                    return result;
                else throw new InvalidRoomTypeException("Room types didn't match with your input");
            }
        }
        public static async Task EnterRoom(Room room, IIOHandler handler)
        {
            while (true)
            {
                handler.Clear();
                handler.ChangeForegroundColor(ConsoleColor.DarkGreen);
                await handler.WriteAsync("1. Change room states.");
                handler.ResetColor();
                await handler.WriteAsync("2. Return.");
                await handler.WriteAsync(room.ToString());
                if (int.Parse(await handler.ReadAsync()) == 1) await ChangeRoom(room, handler);
                else break;
            }
        }
        private static async Task ChangeRoom(Room room, IIOHandler handler)
        {
            await handler.WriteAsync("Write prop name to change: ");
            var propertyName = await handler.ReadAsync();
            var property = typeof(Room).GetProperty(propertyName);
            if (property != null && property.GetValue(room) != null && property.CanWrite)
            {
                if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(bool))
                {
                    bool currentValue = (bool)property.GetValue(room);
                    property.SetValue(room, !currentValue);
                }
                else if (property.PropertyType == typeof(string))
                {
                    var newValue = await handler.ReadAsync();
                    property.SetValue(room, newValue);
                }
                else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(float))
                {
                    await handler.WriteAsync($"Enter new value for {propertyName}: ");
                    var newValue = float.Parse(await handler.ReadAsync());
                    float minValue = 30;
                    float maxValue = 70;
                    if (propertyName == nameof(room.Temperature)) { minValue = 0; maxValue = 40; }
                    if (newValue >= minValue && newValue <= maxValue)
                    {
                        property.SetValue(room, newValue);
                    }
                    else await handler.WriteAsync($"Invalid value. {propertyName} must be between {minValue} and {maxValue}.");
                }
            }
            else await handler.WriteAsync($"Couldn't change {propertyName} value.");
        }
    }
}
