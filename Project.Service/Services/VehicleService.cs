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

        public async Task UpdateVehicleMakeAsync(VehicleMakeViewModel vehicleMakeViewModel)
        {
            var vehicleMake = this.mapper.Map<VehicleMake>(vehicleMakeViewModel);
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
    }
}
