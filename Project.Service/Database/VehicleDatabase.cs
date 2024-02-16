using Project.Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database
{
    public class VehicleDatabase : DbContext
    {
        public VehicleDatabase() : base("VehicleDatabase")
        {
        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
