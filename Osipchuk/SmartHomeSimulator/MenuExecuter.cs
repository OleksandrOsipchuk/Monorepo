using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using SmartHomeSimulator.HousesFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class MenuExecuter
    {
        private List<House> _houses;
        private IJsonWorker _jsonWorker;
        //private House _currentHouse;
        //private Room _currentRoom;
        public MenuExecuter(IJsonWorker jsonWorker)
        {
            _jsonWorker = jsonWorker;
        }

        public async Task RunAsync()
        {
            _houses = await _jsonWorker.ReadAsync<House>();
            while (true)
            {
                int option = GetOption<House>(_houses);
                if (option < _houses.Count+ 1) EnterHouse(_houses[option-1]);
                else if (option == _houses.Count + 1) ManageHouses();
                else break;
            }
        }
        private void EnterHouse(House house) {
            List<Room> rooms = house.GetRooms();
            while(true)
            {
                int option = GetOption<Room>(rooms);
                if (option < rooms.Count + 1) break; // EnterRoom(rooms[option - 1]);
                else break;
            }

        }
        private void ManageHouses() { // idk yet
            Console.WriteLine("ManageHouses working");
        }
        private int GetOption<T>(List<T> values) where T : INameable
        {
            Console.WriteLine("Select an option: ");
            int i = 1;
            while(i <= values.Count)
            {
                Console.WriteLine($"{i}. {values[i-1].Name}.");
                i++;
            }
            Console.WriteLine($"{i++}. Manage.");
            Console.WriteLine($"{i++}. Exit.");
            return int.Parse(Console.ReadLine());
        }
    }
}
