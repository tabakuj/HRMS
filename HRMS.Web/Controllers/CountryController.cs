﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }
        public IActionResult Index()
        {
            var list = countryService.GetAll();
            return View(list.Result.Select(a => new CountryViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Code=a.Code
            }));
        }

        public IActionResult Create()
        {
            var model = new CountryViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryViewModel model)
        {
            var toCreateModel = new Country
            {
                Name = model.Name,
                Code=model.Code
            };
            countryService.Create(toCreateModel);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var model = countryService.GetById(id).Result;
            return View(new CountryViewModel {
                Id=id,
                Name=model.Name,
                Code=model.Code
            });
        }

        public IActionResult Details(int id)
        {
            var model = countryService.GetById(id).Result;
            return View(new CountryViewModel
            {
                Id = id,
                Name = model.Name,
                Code=model.Code
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CountryViewModel model)
        {
            var toUpdateModel = new Country
            {
                Name = model.Name,
                Id = model.Id,
                Code=model.Code
            };
            countryService.Edit(toUpdateModel);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            countryService.Delete(id);
            return Json(new GenericViewModel
            {
                IsSuccessful = true
            });
        }

    }

}