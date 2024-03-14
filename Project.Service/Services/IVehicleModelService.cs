using Project.Service.Database.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IVehicleModelService
    {
        Task<List<VehicleModel>> GetVehicleModelsAsync();
        Task<VehicleModel> GetVehicleModelByIdAsync(int id);
        Task<int> CreateVehicleModelAsync(VehicleModel vehicleModel);
        Task<bool> DeleteVehicleModelAsync(int id);
        Task<int> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<PageResult<VehicleModel>> GetVehicleModelsPageAsync(FilterSortPageOptions filterSortPageOptions);
    }
}
