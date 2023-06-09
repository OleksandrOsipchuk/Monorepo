﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.HousesFiles
{
    public interface IHomeDataStorage
    {
        Task<IList<T>> ReadAsync<T>();
        Task WriteAsync<T>(T obj);
    }
}
