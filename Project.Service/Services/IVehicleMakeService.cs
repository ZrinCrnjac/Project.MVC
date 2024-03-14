using Project.Service.Database.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<PageResult<VehicleMake>> GetVehiclesPageAsync(FilterSortPageOptions filterSortPageOptions);
        Task<List<VehicleMake>> GetVehiclesAsync();
        Task<VehicleMake> GetVehicleMakeByIdAsync(int id);
        Task<VehicleMake> GetVehicleMakeWithModelsAsync(int id);
        Task CreateVehicleMakeAsync(VehicleMake vehicleCreateMakeViewModel);
        Task UpdateVehicleMakeAsync(VehicleMake vehicleMakeCreateViewModel);
        Task<bool> DeleteVehicleMakeAsync(int id);
    }
}
