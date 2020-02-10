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
    public class JsonController : Controller
    {
        private readonly IJsonService service;

        public JsonController(IJsonService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetCountries(string search, int page)
        {

            var serviceResponse = await service.GetAllCountriesAsync(search);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions(string search, int page,int? countryId)
        {

            var serviceResponse = await service.GetAllRegionsAsync(search,countryId);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(string search, int page, int? countryId,int? regionId)
        {

            var serviceResponse = await service.GetAllCitiesAsync(search, countryId,regionId);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSites(string search, int page,Guid companyId)
        {

            var serviceResponse = await service.GetAllSitesAsync(search,companyId);

            var result = new JsonGenericModel();
            if (serviceResponse.IsSuccessful)
            {
                result.IsSuccessful = true;
                result.Items = serviceResponse.Result.Select(a => Parse(a));
            }
            else
            {
                result.ErrorMessage = serviceResponse.Message;
            }

            return Json(result);
        }

        private SelectDataDTO Parse(Site a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }

        private SelectDataDTO Parse(City a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }
        private SelectDataDTO Parse(Region a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }

        private static SelectDataDTO Parse(Country a)
        {
            return new SelectDataDTO
            {
                Id = a.Id.ToString(),
                Text = a.Name
            };
        }
    }
}