﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public class BedroomDirector : IRoomDirector
    {
        private readonly IRoomBuilder _builder;
        public BedroomDirector(IRoomBuilder builder)
        {
            _builder = builder;
        }
        public void Build()
        {
            _builder
                .ChangeName()
                .ChangeTemperature()
                .ChangeLightState()
                .ChangeHumidity()
                .ChangeTVState();
        }
    }
}
