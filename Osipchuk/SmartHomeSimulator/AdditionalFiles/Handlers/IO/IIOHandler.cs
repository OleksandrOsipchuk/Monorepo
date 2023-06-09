﻿using SmartHomeSimulator.AdditionalFiles.Handlers.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers.IO
{
    public interface IIOHandler
    {
        Task WriteAsync(string message);
        Task<string> ReadAsync();
        void Clear();
        void ChangeForegroundColor(IColorParameters color);
        void ResetColor();
    }
}