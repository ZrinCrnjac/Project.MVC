using Microsoft.EntityFrameworkCore;
using Project.Service.Database;
using Project.Service.Database.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleDatabase context;

        public VehicleModelService(VehicleDatabase context)
        {
            this.context = context;
        }

        public async Task<List<VehicleModel>> GetVehicleModelsAsync()
        {
            var vehicleModels = await this.context.VehicleModels.Include(v => v.VehicleMake).ToListAsync();
            return vehicleModels;
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(int id)
        {
            var vehicleModel = await this.context.VehicleModels.Include(v => v.VehicleMake).FirstOrDefaultAsync(v => v.Id == id);
            return vehicleModel;
        }

        public async Task<int> CreateVehicleModelAsync(VehicleModel vehicleModel)
        {
            this.context.VehicleModels.Add(vehicleModel);
            await this.context.SaveChangesAsync();
            return vehicleModel.Id;
        }

        public async Task<bool> DeleteVehicleModelAsync(int id)
        {
            var vehicleModel = await this.context.VehicleModels.FindAsync(id);

            if (vehicleModel == null)
            {
                return false;
            }

            this.context.VehicleModels.Remove(vehicleModel);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<int> UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            this.context.VehicleModels.Update(vehicleModel);
            await this.context.SaveChangesAsync();
            return vehicleModel.Id;
        }

        public async Task<PageResult<VehicleModel>> GetVehicleModelsPageAsync(FilterSortPageOptions filterSortPageOptions)
        {
            string searchString = filterSortPageOptions.SearchString;
            string sortOrder = filterSortPageOptions.SortOrder;
            int pageNumber = filterSortPageOptions.PageNumber;

            var models = this.context.VehicleModels.Include(v => v.VehicleMake).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                models = models.Where(m => m.Name.ToLower().Contains(searchString) || m.VehicleMake.Name.ToLower().Contains(searchString));
                pageNumber = 1;
            }

            var pageSize = 10;

            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                case "name_asc":
                    models = models.OrderBy(m => m.Name);
                    break;
                case "abrv_desc":
                    models = models.OrderByDescending(m => m.VehicleMake.Name);
                    break;
                case "makeName_desc":
                    models = models.OrderByDescending(m => m.VehicleMake.Name);
                    break;
                default:
                    models = models.OrderBy(m => m.Name);
                    break;
            }

            var totalCount = await models.CountAsync();

            var vehicleModels = await models.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var page = new PageResult<VehicleModel>(vehicleModels, totalCount, pageNumber, pageSize);

            return page;
        }
    }
}
