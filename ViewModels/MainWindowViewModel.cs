﻿using Modul_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_12.ViewModels
{
    public class MainWindowViewModel
    {
        public Clients Clients { get; set; }

        public Consultant Consultant { get; set; } 
        
        public Meneger Meneger { get; set; } 

        public MainWindowViewModel()
        {
            this.Clients = new Clients("data.csv"); //path

            Consultant = new Consultant();

            Meneger = new Meneger();
        }
    }
}
