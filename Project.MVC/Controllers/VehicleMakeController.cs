﻿using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> Index()
        {
            var makes = await vehicleService.GetVehiclesAsync();
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
        public async Task<ActionResult> Create(VehicleMakeViewModel vehicleMakeViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(vehicleMakeViewModel);
            }

            var make = new VehicleMakeViewModel
            {
                Name = vehicleMakeViewModel.Name,
                Abrv = vehicleMakeViewModel.Abrv
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

        [HttpPost]
        [ValidateAntiForgeryToken]
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