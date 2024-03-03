using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Abrv { get; set; }
        public string MakeName { get; set; }
    }
}
