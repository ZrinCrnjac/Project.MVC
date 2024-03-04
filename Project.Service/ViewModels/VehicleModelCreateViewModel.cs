using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.ViewModels
{
    public class VehicleModelCreateViewModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(20)]
        public required string Name { get; set; }
        [Required]
        [StringLength(10)]
        public required string Abrv { get; set; }
        [Required]
        //error message in Required
        public int MakeId { get; set; }
    }
}
