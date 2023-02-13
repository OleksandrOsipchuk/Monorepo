using Newtonsoft.Json;
using SmartHomeSimulator.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class House
    {
        [JsonProperty]
        private List<Room> rooms= new List<Room>();
        public void AddRoom (Room room) => this.rooms.Add(room);
        public void RemoveRoom (Room room) => this.rooms.Remove(room);
        public IEnumerable<Room> GetRooms () => this.rooms;
    }
}