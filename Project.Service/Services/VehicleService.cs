using Microsoft.EntityFrameworkCore;
using Project.Service.Database;
using Project.Service.Database.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDatabase context;
        private readonly IMapper mapper;

        public VehicleService(VehicleDatabase context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<VehicleMakeViewModel>> GetVehiclesAsync()
        {
            var vehicleMakes = await this.context.VehicleMakes.ToListAsync();
            var vehicleMakeViewModels = this.mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);
            return vehicleMakeViewModels;
        }

        public async Task<PageResult<VehicleMakeViewModel>> GetVehiclesPageAsync(string searchString, int pageNumber, string sortOrder)
        {

            var makes = this.context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                makes = makes.Where(m => m.Name.ToLower().Contains(searchString) || m.Abrv.ToLower().Contains(searchString));
                pageNumber = 1;
            }

            var pageSize = 10;

            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                case "name_asc":
                    makes = makes.OrderBy(m => m.Name);
                    break;
                case "abrv_desc":
                    makes = makes.OrderByDescending(m => m.Abrv);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

            var totalCount = await makes.CountAsync();

            var items = await makes.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var vehicleMakeViewModels = this.mapper.Map<List<VehicleMakeViewModel>>(items);

            var page = new PageResult<VehicleMakeViewModel>(vehicleMakeViewModels, totalCount, pageNumber, pageSize);

            return page;
        }

        public async Task CreateVehicleMakeAsync(VehicleMakeCreateViewModel vehicleMakeCreateViewModel)
        {
            var vehicleMake = this.mapper.Map<VehicleMake>(vehicleMakeCreateViewModel);
            this.context.VehicleMakes.Add(vehicleMake);
            await this.context.SaveChangesAsync();
        }

        public async Task<VehicleMakeViewModel> GetVehicleMakeByIdAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.FindAsync(id);
            var vehicleMakeViewModel = this.mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return vehicleMakeViewModel;
        }

        public async Task<VehicleMakeViewModel> GetVehicleMakeWithModelsAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.Include(v => v.Models).FirstOrDefaultAsync(v => v.Id == id);

            if (vehicleMake == null)
            {
                return null;
            }

            var vehicleMakeViewModel = this.mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return vehicleMakeViewModel;
        }

        public async Task UpdateVehicleMakeAsync(VehicleMakeCreateViewModel vehicleMakeCreateViewModel)
        {
            var vehicleMake = this.mapper.Map<VehicleMake>(vehicleMakeCreateViewModel);
            this.context.VehicleMakes.Update(vehicleMake);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteVehicleMakeAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.FindAsync(id);
            this.context.VehicleMakes.Remove(vehicleMake);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<VehicleModelViewModel>> GetVehicleModelsAsync()
        {
            var vehicleModels = await this.context.VehicleModels.Include(v => v.VehicleMake).ToListAsync();
            var vehicleModelViewModels = this.mapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            return vehicleModelViewModels;
        }

        public async Task<VehicleModelViewModel> GetVehicleModelByIdAsync(int id)
        {
            var vehicleModel = await this.context.VehicleModels.Include(v => v.VehicleMake).FirstOrDefaultAsync(v => v.Id == id);
            var vehicleModelViewModel = this.mapper.Map<VehicleModelViewModel>(vehicleModel);
            return vehicleModelViewModel;
        }

        public async Task<int> CreateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel)
        {
            var vehicleModel = this.mapper.Map<VehicleModel>(vehicleModelCreateViewModel);
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

        public async Task<int> UpdateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel)
        {
            var vehicleModel = this.mapper.Map<VehicleModel>(vehicleModelCreateViewModel);
            this.context.VehicleModels.Update(vehicleModel);
            await this.context.SaveChangesAsync();
            return vehicleModel.Id;
        }

        public async Task<PageResult<VehicleModelViewModel>> GetVehicleModelsPageAsync(string searchString, int pageNumber, string sortOrder)
        {
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

            var items = await models.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var vehicleModelViewModels = this.mapper.Map<List<VehicleModelViewModel>>(items);

            var page = new PageResult<VehicleModelViewModel>(vehicleModelViewModels, totalCount, pageNumber, pageSize);

            return page;
        }
    }
}
