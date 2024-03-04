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
        public async Task<ActionResult> Index()
        {
            var models = await vehicleService.GetVehicleModelsAsync();
            return View(models);
        }

        // GET: VehicleModelController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await vehicleService.GetVehicleModelByIdAsync(id);

            if (model == null)
            {
                //napraviti view za notfound u shared
                return NotFound();
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
                return NotFound();
            }

            var makes = await vehicleService.GetVehiclesAsync();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakeList = makeList;

            return View(model);
        }

        // POST: VehicleModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleModelController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var success = await vehicleService.DeleteVehicleModelAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
