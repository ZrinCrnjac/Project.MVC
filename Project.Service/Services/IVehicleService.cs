using Project.Service.Database.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleMakeViewModel>> GetVehiclesAsync();
        Task<VehicleMakeViewModel> GetVehicleMakeByIdAsync(int id);
        Task CreateVehicleMakeAsync(VehicleMakeCreateViewModel vehicleCreateMakeViewModel);
        Task UpdateVehicleMakeAsync(VehicleMakeViewModel vehicleMakeViewModel);
        Task DeleteVehicleMakeAsync(int id);
        Task<List<VehicleModelViewModel>> GetVehicleModelsAsync();
        Task<VehicleModelViewModel> GetVehicleModelByIdAsync(int id);
        Task<int> CreateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel);
        Task<bool> DeleteVehicleModelAsync(int id);
        Task<int> UpdateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel);
    }
}
