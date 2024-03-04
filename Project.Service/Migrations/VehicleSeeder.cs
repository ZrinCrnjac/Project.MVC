using Project.Service.Database;
using Project.Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Migrations
{
    public class VehicleSeeder
    {
        private readonly VehicleDatabase vehicleDatabase;

        public VehicleSeeder(VehicleDatabase vehicleDatabase)
        {
            this.vehicleDatabase = vehicleDatabase;
        }

        public void Seed()
        {
            var makes = new List<VehicleMake>();

            for (int i = 0; i < 50; i++)
            {
                string name = "Make " + i;
                string abrv = "M" + i;

                var make = new VehicleMake { Name = name, Abrv = abrv, Models = new List<VehicleModel>() };

                makes.Add(make);
            }

            var models = new List<VehicleModel>();

            for (int i = 0; i < 50; i++)
            {
                string name = "Model " + i;
                string abrv = "M" + i;

                var randomMake = makes[i];

                var model = new VehicleModel { Name = name, Abrv = abrv, MakeId = i, VehicleMake = randomMake };
                models.Add(model);
            }

            vehicleDatabase.AddRange(makes);
            vehicleDatabase.AddRange(models);
            vehicleDatabase.SaveChanges();
        }
    }
}
