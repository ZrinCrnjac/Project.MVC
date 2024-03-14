using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.ViewModels;
using Project.Service.ViewModels;
using Project.Service.Services;
using Project.Service.Database.Models;
using AutoMapper;


namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeService vehicleMakeService;
        private readonly IMapper mapper;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            this.vehicleMakeService = vehicleMakeService;
            this.mapper = mapper;
        }

        // GET: VehicleMakeController
        public async Task<ActionResult> Index(FilterSortPageOptions filterSortPageOptions)
        {
            ViewData["CurrentSort"] = filterSortPageOptions.SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(filterSortPageOptions.SortOrder) || filterSortPageOptions.SortOrder =="name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParm"] = filterSortPageOptions.SortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewData["CurrentFilter"] = filterSortPageOptions.SearchString;

            var makes = await vehicleMakeService.GetVehiclesPageAsync(filterSortPageOptions);

            var items = mapper.Map<List<VehicleMakeViewModel>>(makes.Items);

            var page = new PageResult<VehicleMakeViewModel>(items, makes.Count, makes.PageIndex, filterSortPageOptions.PageSize);

            return View(page);
        }

        // GET: VehicleMakeController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            var make = await vehicleMakeService.GetVehicleMakeWithModelsAsync(id.Value);

            if(make == null)
            {
                return View("NotFound");
            }

            var makesViewModel = this.mapper.Map<VehicleMakeViewModel>(make);

            return View(makesViewModel);
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

            var make = this.mapper.Map<VehicleMake>(vehicleMakeCreateViewModel);

            await vehicleMakeService.CreateVehicleMakeAsync(make);

            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleMakeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var make = await vehicleMakeService.GetVehicleMakeByIdAsync(id);

            if(make == null)
            {
                return View("NotFound");
            }

            var makeViewModel = this.mapper.Map<VehicleMakeCreateViewModel>(make);

            return View(makeViewModel);
        }

        // POST: VehicleMakeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleMakeCreateViewModel vehicleMakeCreateViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(vehicleMakeCreateViewModel);
            }

            var make = this.mapper.Map<VehicleMake>(vehicleMakeCreateViewModel);

            await vehicleMakeService.UpdateVehicleMakeAsync(make);

            return RedirectToAction(nameof(Details), new { id = vehicleMakeCreateViewModel.Id });
        }

        // GET: VehicleMakeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await vehicleMakeService.DeleteVehicleMakeAsync(id);

            if (!isDeleted)
            {
                return View("NotFound");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
