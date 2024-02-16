using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database.Models
{
    public class VehicleMake
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public required string Name { get; set; }

        [StringLength(10)]
        public required string Abrv { get; set; }

        public virtual required ICollection<VehicleModel> Models { get; set; }
    }
}
