using Modul_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul_12.ViewModels
{
    public class MainWindowViewModel
    {
        public Clients Clients { get; set; } = new Clients();

        public MainWindowViewModel()
        {

        }
    }
}
