using Newtonsoft.Json.Linq;
using SmartHomeSimulator.AdditionalFiles;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.Directors;
using SmartHomeSimulator.Builder.RoomFiles;
using SmartHomeSimulator.HousesFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Executer
{
    public class MenuExecuter
    {
        private List<House> _houses;
        private IJsonWorker _jsonWorker;
        private IIOHandler _handler;
        public MenuExecuter(IJsonWorker jsonWorker, IIOHandler handler)
        {
            _jsonWorker = jsonWorker;
            _handler= handler;
        }

        public async Task RunMenuAsync() // maybe try different console colors
        {
            _houses = await _jsonWorker.ReadAsync<House>();
            while (true)
            {
                Console.Clear();
                int option = RoomLogic.GetOption(_houses, _handler,"Manage houses.");
                if (option < _houses.Count + 1) EnterHouse(_houses[option - 1]);
                else if (option == _houses.Count + 1) ManageHouses();
                else break;
            }
            _jsonWorker.WriteAsync(_houses);
        }
        private void ManageHouses()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                _handler.Write("MANAGING.\nIf you want to remove house, write its number.");
                Console.ResetColor();
                var option = RoomLogic.GetOption(_houses, _handler, "Add new house.");
                if (option < _houses.Count + 1) _houses.Remove(_houses[option - 1]);
                else if (option == _houses.Count + 1)
                {
                    _handler.Write("Enter new house name: ");
                    _houses.Add(new House(_handler.Read()));
                }
                else break;
            }
        }
        private void EnterHouse(House house)
        {
            List<Room> rooms = house.GetRooms();
            while (true)
            {
                Console.Clear();
                int option = RoomLogic.GetOption(house.GetRooms(), _handler, "Manage rooms.");
                if (option < rooms.Count + 1) RoomLogic.EnterRoom(house.GetRooms()[option - 1], _handler);
                else if (option == rooms.Count + 1) RoomLogic.ManageRooms(house.GetRooms(), _handler);
                else break;
            }
        }
    }
}
