using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Service.ViewModels;
using Project.Service.Services;


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
        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";

            var makes = from m in await vehicleService.GetVehiclesAsync()
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(m => m.Name.Contains(searchString)
                                    || m.Abrv.Contains(searchString));
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
                case "abrv_asc":
                    makes = makes.OrderBy(m => m.Abrv);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }
            return View(makes);
        }

        // GET: VehicleMakeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var make = await vehicleService.GetVehicleMakeByIdAsync(id);

            if(make == null)
            {
                return NotFound();
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
                return NotFound();
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
            if(!ModelState.IsValid)
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

            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleMakeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var make = await vehicleService.GetVehicleMakeByIdAsync(id);

            if(make == null)
            {
                return NotFound();
            }

            await vehicleService.DeleteVehicleMakeAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
