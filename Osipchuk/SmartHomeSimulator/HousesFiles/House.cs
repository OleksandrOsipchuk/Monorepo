using Newtonsoft.Json;
using SmartHomeSimulator.AdditionalFiles;
using SmartHomeSimulator.Builder.Directors;
using SmartHomeSimulator.Builder.RoomFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.HousesFiles
{
    public class House : NameBase
    {
        public string Name { get; set; }
        public House(string name) { Name = name; }
        [JsonProperty]
        private List<Room> _rooms = new();
        public List<Room> GetRooms() => _rooms;
    }
}