using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public required string Name { get; set; }

        [StringLength(10)]
        public required string Abrv { get; set; }

        public int MakeId { get; set; }
        [ForeignKey("MakeId")]
        public required VehicleMake VehicleMake { get; set; }
    }
}
