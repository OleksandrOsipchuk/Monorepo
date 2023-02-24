using SmartHomeSimulator.AdditionalFiles.Handlers.IO;
using SmartHomeSimulator.Builder.RoomFiles;
using SmartHomeSimulator.HousesFiles;
using SmartHomeSimulator.AdditionalFiles.Handlers.Color;

namespace SmartHomeSimulator.Executer
{
    public class HouseMenuHandler : BaseMenuHandler
    {
        private IList<House> _houses;
        private readonly IHomeDataStorage _jsonWorker;
        private readonly IIOHandler _handler;
        public HouseMenuHandler(IHomeDataStorage jsonWorker, IIOHandler handler)
        {
            _jsonWorker = jsonWorker;
            _handler = handler;
        }

        public async Task RunMenuAsync()
        {
            _houses = await _jsonWorker.ReadAsync<House>();
            while (true)
            {
                _handler.Clear();
                int option = await GetOptionAsync(_houses, _handler, "Manage houses.");
                if (option < _houses.Count + 1) await EnterHouse(_houses[option - 1]);
                else if (option == _houses.Count + 1) await ManageHouses();
                else break;
            }
            await _jsonWorker.WriteAsync(_houses);
        }
        private async Task ManageHouses()
        {
            while (true)
            {
                _handler.Clear();
                _handler.ChangeForegroundColor(new ConsoleColorParameters(255, 0, 0));
                await _handler.WriteAsync("MANAGING.\nIf you want to remove house, write its number.");
                _handler.ResetColor();
                var option = await GetOptionAsync(_houses, _handler, "Add new house.");
                if (option < _houses.Count + 1)
                {
                    _houses.Remove(_houses[option - 1]);
                    await _jsonWorker.WriteAsync(_houses);
                }
                else if (option == _houses.Count + 1)
                {
                    await _handler.WriteAsync("Enter new house name: ");
                    _houses.Add(new House(await _handler.ReadAsync()));
                    await _jsonWorker.WriteAsync(_houses);
                }
                else break;
            }
        }
        private async Task EnterHouse(House house)
        {
            List<Room> rooms = house.GetRooms();
            while (true)
            {
                _handler.Clear();
                int option = await GetOptionAsync(house.GetRooms(), _handler, "Manage rooms.");
                if (option < rooms.Count + 1) await RoomMenuHandler.EnterRoom(house.GetRooms()[option - 1], _handler);
                else if (option == rooms.Count + 1) await RoomMenuHandler.ManageRooms(house.GetRooms(), _handler);
                else break;
            }
        }
    }
}
