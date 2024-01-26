﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOMock2.BO
{
    public class Monitor : IMonitor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public float Diagonal { get; set; }
        public MatrixType Matrix { get; set; }
    }
}
