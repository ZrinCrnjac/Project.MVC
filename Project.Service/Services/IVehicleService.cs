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
        Task CreateVehicleMakeAsync(VehicleMakeViewModel vehicleMakeViewModel);
        Task UpdateVehicleMakeAsync(VehicleMakeViewModel vehicleMakeViewModel);
        Task DeleteVehicleMakeAsync(int id);
    }
}
