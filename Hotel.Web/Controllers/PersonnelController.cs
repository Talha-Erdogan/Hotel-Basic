using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Personnel;
using Hotel.Web.Filters;
using Hotel.Web.Models.Personnel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly IPersonnelService _personnelService;
        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            PersonnelSearchFilter searchFilter = new PersonnelSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;

            model.DataList = _personnelService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_LIST)]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            PersonnelSearchFilter searchFilter = new PersonnelSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            model.DataList = _personnelService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        public ActionResult Add()
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Personnel.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Data.Entity.Personnel personnel = new Data.Entity.Personnel();
            personnel.TC = model.TC;
            personnel.Name = model.Name;
            personnel.LastName = model.LastName;
            personnel.Phone = model.Phone;
            personnel.Address = model.Address;
            personnel.UserName = model.UserName;
            personnel.Password = model.Password;
            var result = _personnelService.Add(personnel);
            if (result > 0)
            {
                return RedirectToAction(nameof(PersonnelController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Personnel.AddViewModel model = new AddViewModel();
            var personnel = _personnelService.GetById(id);
            if (personnel == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = personnel.Id;
            model.TC = personnel.TC;
            model.Name = personnel.Name;
            model.LastName = personnel.LastName;
            model.Phone = personnel.Phone;
            model.Address = personnel.Address;
            model.UserName = personnel.UserName;
            model.Password = personnel.Password;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_PERSONNEL_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Personnel.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var personnel = _personnelService.GetById(model.Id);
            if (personnel == null)
            {
                return View("_ErrorNotExist");
            }
            personnel.TC = model.TC;
            personnel.Name = model.Name;
            personnel.LastName = model.LastName;
            personnel.Phone = model.Phone;
            personnel.Address = model.Address;
            personnel.UserName = model.UserName;
            personnel.Password = model.Password;

            var result = _personnelService.Update(personnel);
            if (result <= 0)
            {
                ViewBag.ErrorMessage = "Not Edited";
                return View(model);
            }
            return RedirectToAction(nameof(PersonnelController.List));
        }


    }
}
