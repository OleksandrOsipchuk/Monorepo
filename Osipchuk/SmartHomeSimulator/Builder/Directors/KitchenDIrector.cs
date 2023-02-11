﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public class KitchenDIrector : IRoomDirector
    {
        private readonly IRoomBuilder _builder;
        public KitchenDIrector(IRoomBuilder builder)
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
