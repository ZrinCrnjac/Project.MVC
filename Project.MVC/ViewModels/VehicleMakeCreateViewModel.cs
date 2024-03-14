using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Abrv { get; set; }
    }
}
