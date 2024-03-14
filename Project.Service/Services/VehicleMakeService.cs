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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleDatabase context;

        public VehicleMakeService(VehicleDatabase context)
        {
            this.context = context;
        }
        public async Task<List<VehicleMake>> GetVehiclesAsync()
        {
            var vehicleMakes = await this.context.VehicleMakes.ToListAsync();
            return vehicleMakes;
        }

        public async Task<PageResult<VehicleMake>> GetVehiclesPageAsync(FilterSortPageOptions filterSortPageOptions)
        {
            string searchString = filterSortPageOptions.SearchString;
            string sortOrder = filterSortPageOptions.SortOrder;
            int pageNumber = filterSortPageOptions.PageNumber;
            int pageSize = filterSortPageOptions.PageSize;

            var makes = this.context.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                makes = makes.Where(m => m.Name.ToLower().Contains(searchString) || m.Abrv.ToLower().Contains(searchString));
                pageNumber = 1;
            }

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

            var vehicleMakes = await makes.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            var page = new PageResult<VehicleMake>(vehicleMakes, totalCount, pageNumber, pageSize);

            return page;
        }

        public async Task CreateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            this.context.VehicleMakes.Add(vehicleMake);
            await this.context.SaveChangesAsync();
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.FindAsync(id);
            return vehicleMake;
        }

        public async Task<VehicleMake> GetVehicleMakeWithModelsAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.Include(v => v.Models).FirstOrDefaultAsync(v => v.Id == id);
            return vehicleMake;
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            this.context.VehicleMakes.Update(vehicleMake);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVehicleMakeAsync(int id)
        {
            var vehicleMake = await this.context.VehicleMakes.FindAsync(id);
            if(vehicleMake == null)
            {
                return false;
            }
            this.context.VehicleMakes.Remove(vehicleMake);
            await this.context.SaveChangesAsync();
            return true;
        }
    }
}
