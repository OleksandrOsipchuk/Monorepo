using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public class BathroomDirector : IRoomDirector
    {
        private readonly IRoomBuilder _builder;
        public BathroomDirector(IRoomBuilder builder)
        {
            _builder = builder;
        }
        public void Build()
        {
            _builder
                .ChangeTemperature()
                .ChangeLightState()
                .ChangeHumidity();
        }
    }
}
