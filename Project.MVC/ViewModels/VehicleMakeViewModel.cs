using Project.Service.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Abrv { get; set; }
        public List<VehicleModelViewModel> Models { get; set; }
    }
}
