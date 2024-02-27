using Microsoft.EntityFrameworkCore;
using Project.Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database
{
    public class VehicleDatabase : DbContext
    {
        private readonly DbContextOptions<VehicleDatabase> _options;
        public VehicleDatabase(DbContextOptions<VehicleDatabase> options) : base(options)
        {
            _options = options;
        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
