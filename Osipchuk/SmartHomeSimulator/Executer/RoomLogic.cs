using SmartHomeSimulator.AdditionalFiles;
using SmartHomeSimulator.Builder.Directors;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.RoomFiles;

namespace SmartHomeSimulator.Executer
{
    public static class RoomLogic
    {
        public static void ManageRooms(List<Room> rooms, IIOHandler handler)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                handler.Write("MANAGING.\nIf you want to remove room, write its number.");
                Console.ResetColor();
                var option = GetOption<Room>(rooms, handler, "Add new room.");
                if (option < rooms.Count + 1) rooms.Remove(rooms[option - 1]);
                else if (option == rooms.Count + 1)
                {
                    var builder = new RoomBuilder();
                    RoomDirectorFactory factory = new RoomDirectorFactory();
                    IRoomDirector director = factory.GetRoomDirector(GetRoomType(), builder);
                    director.Build();
                    Room room = builder.GetRoom();
                    rooms.Add(room);
                }
                else break;
            }
            RoomType GetRoomType()
            {
                handler.Write("Choose room type: ");
                foreach (var type in Enum.GetNames(typeof(RoomType)))
                    handler.Write(type);
                if (Enum.TryParse<RoomType>(Console.ReadLine(), true, out var result))
                    return result;
                throw new RoomExсeption("Room types didn't match with your input");
            }
        }
        public static void EnterRoom(Room room, IIOHandler handler)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                handler.Write("1. Change room states.");
                Console.ResetColor();
                handler.Write("2. Return.");
                handler.Write(room);
                if (int.Parse(handler.Read()) == 1) ChangeRoom(room, handler);
                else break;
            }
        }
        private static void ChangeRoom(Room room, IIOHandler handler)
        {
            handler.Write("Write prop name to change: ");
            var propertyName = handler.Read();
            var property = typeof(Room).GetProperty(propertyName);
            if (property != null && property.GetValue(room) != null && property.CanWrite)
            {
                if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(bool))
                {
                    bool currentValue = (bool)property.GetValue(room);
                    property.SetValue(room, !currentValue);
                    return;
                }
                else if (property.PropertyType == typeof(string))
                {
                    var newValue = handler.Read();
                    property.SetValue(room, newValue);
                    return;
                }
                else if (Nullable.GetUnderlyingType(property.PropertyType) == typeof(float))
                {
                    handler.Write($"Enter new value for {propertyName}: ");
                    var newValue = float.Parse(handler.Read());
                    float minValue = 30;
                    float maxValue = 70;
                    if(propertyName == nameof(room.Temperature)) { minValue = 0; maxValue = 40; }
                    if (newValue >= minValue && newValue <= maxValue)
                    {
                        property.SetValue(room, newValue);
                    }
                    else handler.Write($"Invalid value. {propertyName} must be between {minValue} and {maxValue}.");
                    return;
                }
            }
            else throw new RoomExсeption($"Cannot change {propertyName} property");
        }
        public static int GetOption<T>(List<T> values, IIOHandler handler, params string[] args) where T : INameable //maybe do other than list - new params 
        {
            handler.Write("Select an option: ");
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            while (i <= values.Count)
            {
                handler.Write($"{i}. {values[i - 1].Name}.");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            foreach (var arg in args)
                handler.Write($"{i++}. {arg}");
            Console.ResetColor();
            handler.Write($"{i++}. Return.");
            return int.Parse(handler.Read());
        }
    }
}
