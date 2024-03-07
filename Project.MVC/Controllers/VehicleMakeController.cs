using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Service.ViewModels;
using Project.Service.Services;
using Project.Service.Database.Models;


namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService vehicleService;

        public VehicleMakeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }
        // GET: VehicleMakeController
        public async Task<ActionResult> Index(string searchString, string sortOrder, int pageNumber = 1)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder=="name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParm"] = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewData["CurrentFilter"] = searchString;

            var makes = await vehicleService.GetVehiclesPageAsync(searchString, pageNumber, sortOrder);

            return View(makes);
        }

        // GET: VehicleMakeController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var make = await vehicleService.GetVehicleMakeWithModelsAsync(id.Value);

            if(make == null)
            {
                return View("NotFound");
            }

            return View(make);
        }

        // GET: VehicleMakeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleMakeCreateViewModel vehicleMakeCreateViewModel)
        {
            if(ModelState.IsValid)
            {
                return View(vehicleMakeCreateViewModel);
            }

            var make = new VehicleMakeCreateViewModel
            {
                Name = vehicleMakeCreateViewModel.Name,
                Abrv = vehicleMakeCreateViewModel.Abrv
            };

            await vehicleService.CreateVehicleMakeAsync(make);

            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleMakeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var make = await vehicleService.GetVehicleMakeByIdAsync(id);

            if(make == null)
            {
                return View("NotFound");
            }

            var makeViewModel = new VehicleMakeViewModel
            {
                Id = make.Id,
                Name = make.Name,
                Abrv = make.Abrv
            };

            return View(makeViewModel);
        }

        // POST: VehicleMakeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleMakeViewModel vehicleMakeViewModel)
        {
            if(ModelState.IsValid)
            {
                return View(vehicleMakeViewModel);
            }

            var make = new VehicleMakeViewModel
            {
                Id = vehicleMakeViewModel.Id,
                Name = vehicleMakeViewModel.Name,
                Abrv = vehicleMakeViewModel.Abrv
            };

            await vehicleService.UpdateVehicleMakeAsync(make);

            return RedirectToAction(nameof(Details), new { id = vehicleMakeViewModel.Id });
        }

        // GET: VehicleMakeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var make = await vehicleService.GetVehicleMakeByIdAsync(id);

            if(make == null)
            {
                return View("NotFound");
            }

            await vehicleService.DeleteVehicleMakeAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
