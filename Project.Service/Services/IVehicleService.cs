using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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
        Task<PageResult<VehicleMakeViewModel>> GetVehiclesPageAsync(string searchString, int pageNumber, string sortOrder);
        Task<List<VehicleMakeViewModel>> GetVehiclesAsync();
        Task<VehicleMakeViewModel> GetVehicleMakeByIdAsync(int id);
        Task<VehicleMakeViewModel> GetVehicleMakeWithModelsAsync(int id);
        Task CreateVehicleMakeAsync(VehicleMakeCreateViewModel vehicleCreateMakeViewModel);
        Task UpdateVehicleMakeAsync(VehicleMakeCreateViewModel vehicleMakeCreateViewModel);
        Task DeleteVehicleMakeAsync(int id);
        Task<List<VehicleModelViewModel>> GetVehicleModelsAsync();
        Task<VehicleModelViewModel> GetVehicleModelByIdAsync(int id);
        Task<int> CreateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel);
        Task<bool> DeleteVehicleModelAsync(int id);
        Task<int> UpdateVehicleModelAsync(VehicleModelCreateViewModel vehicleModelCreateViewModel);
        Task<PageResult<VehicleModelViewModel>> GetVehicleModelsPageAsync(string searchString, int pageNumber, string sortOrder);
    }
}
