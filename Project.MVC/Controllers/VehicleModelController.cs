using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.ViewModels;
using Project.Service.Database.Models;
using Project.Service.Services;
using Project.Service.ViewModels;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelService vehicleModelService;
        private readonly IVehicleMakeService vehicleMakeService;
        private readonly IMapper mapper;

        public VehicleModelController(IVehicleModelService vehicleService, IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            this.vehicleModelService = vehicleService;
            this.vehicleMakeService = vehicleMakeService;
            this.mapper = mapper;
        }

        // GET: VehicleModelController
        public async Task<ActionResult> Index(FilterSortPageOptions filterSortPageOptions)
        {
            ViewData["CurrentSort"] = filterSortPageOptions.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(filterSortPageOptions.SortOrder) || filterSortPageOptions.SortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParm"] = filterSortPageOptions.SortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewData["MakeNameSortParm"] = filterSortPageOptions.SortOrder == "MakeName" ? "makeName_desc" : "MakeName";
            ViewData["CurrentFilter"] = filterSortPageOptions.SearchString;

            var models = await vehicleModelService.GetVehicleModelsPageAsync(filterSortPageOptions);

            var items = this.mapper.Map<List<VehicleModelViewModel>>(models.Items);

            var page = new PageResult<VehicleModelViewModel>(items, models.Count, models.PageIndex, filterSortPageOptions.PageSize);

            return View(page);
        }

        // GET: VehicleModelController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var model = await vehicleModelService.GetVehicleModelByIdAsync(id);

            if(model == null)
            {
                return View("NotFound");
            }

            var modelViewModel = this.mapper.Map<VehicleModelViewModel>(model);

            return View(modelViewModel);
        }

        // GET: VehicleModelController/Create
        public async Task<ActionResult> Create()
        {
            var makes = await vehicleMakeService.GetVehiclesAsync();

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
                var makes = await vehicleMakeService.GetVehiclesAsync();

                var makeList = new SelectList(makes, "Id", "Name");

                ViewBag.MakeList = makeList;

                return View(vehicleModelCreateViewModel);
            }

            var model = this.mapper.Map<VehicleModel>(vehicleModelCreateViewModel);

            var modelId = await vehicleModelService.CreateVehicleModelAsync(model);

            return RedirectToAction(nameof(Details), new { id = modelId });
        }

        // GET: VehicleModelController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await vehicleModelService.GetVehicleModelByIdAsync(id);

            if (model == null)
            {
                return View("NotFound");
            }

            var modelViewModel = this.mapper.Map<VehicleModelCreateViewModel>(model);

            var makes = await vehicleMakeService.GetVehiclesAsync();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakeList = makeList;

            return View(modelViewModel);
        }

        // POST: VehicleModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleModelCreateViewModel vehicleModelCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                var makes = await vehicleMakeService.GetVehiclesAsync();

                var makeList = new SelectList(makes, "Id", "Name");

                ViewBag.MakeList = makeList;

                return View(vehicleModelCreateViewModel);
            }

            var model = this.mapper.Map<VehicleModel>(vehicleModelCreateViewModel);

            var modelId = await vehicleModelService.UpdateVehicleModelAsync(model);

            return RedirectToAction(nameof(Details), new { id = vehicleModelCreateViewModel.Id });
        }

        // GET: VehicleModelController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var success = await vehicleModelService.DeleteVehicleModelAsync(id);

            if (!success)
            {
                return View("NotFound");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
