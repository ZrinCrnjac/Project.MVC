using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Database.Models;
using Project.Service.Services;
using Project.Service.ViewModels;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService vehicleService;

        public VehicleModelController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        // GET: VehicleModelController
        public async Task<ActionResult> Index(string searchString, string sortOrder, int pageNumber = 1)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParm"] = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewData["MakeNameSortParm"] = sortOrder == "MakeName" ? "makeName_desc" : "MakeName";
            ViewData["CurrentFilter"] = searchString;

            var models = await vehicleService.GetVehicleModelsPageAsync(searchString, pageNumber, sortOrder);

            return View(models);
        }

        // GET: VehicleModelController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await vehicleService.GetVehicleModelByIdAsync(id);

            if (model == null)
            {
                return View("NotFound");
            }

            return View(model);
        }

        // GET: VehicleModelController/Create
        public async Task<ActionResult> Create()
        {
            var makes = await vehicleService.GetVehiclesAsync();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakeList = makeList;

            return View();
        }

        // POST: VehicleModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleModelCreateViewModel vehicleModelCreateViewModel)
        {

            if (!ModelState.IsValid)
            {
                var makes = await vehicleService.GetVehiclesAsync();

                var makeList = new SelectList(makes, "Id", "Name");

                ViewBag.MakeList = makeList;

                return View(vehicleModelCreateViewModel);
            }

            var modelId = await vehicleService.CreateVehicleModelAsync(vehicleModelCreateViewModel);

            return RedirectToAction(nameof(Details), new { id = modelId });
        }

        // GET: VehicleModelController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await vehicleService.GetVehicleModelByIdAsync(id);

            if (model == null)
            {
                return View("NotFound");
            }

            var makes = await vehicleService.GetVehiclesAsync();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakeList = makeList;

            return View(model);
        }

        // POST: VehicleModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleModelCreateViewModel vehicleModelCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                var makes = await vehicleService.GetVehiclesAsync();

                var makeList = new SelectList(makes, "Id", "Name");

                ViewBag.MakeList = makeList;

                return View(vehicleModelCreateViewModel);
            }

            var modelId = await vehicleService.UpdateVehicleModelAsync(vehicleModelCreateViewModel);

            return RedirectToAction(nameof(Details), new { id = vehicleModelCreateViewModel.Id });
        }

        // GET: VehicleModelController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var success = await vehicleService.DeleteVehicleModelAsync(id);

            if (!success)
            {
                return View("NotFound");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
