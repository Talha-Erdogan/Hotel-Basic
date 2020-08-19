using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Common;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Auth;
using Hotel.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            model.DataList = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_LIST)]
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

            AuthSearchFilter searchFilter = new AuthSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.DataList = _authService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        public ActionResult Add()
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Data.Entity.Auth auth = new Data.Entity.Auth();
            auth.Code = model.Code;
            auth.Name = model.Name;
            var result = _authService.Add(auth);
            if (result>0)
            {
                return RedirectToAction(nameof(AuthController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                return View(model);
            }
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Auth.AddViewModel model = new AddViewModel();
            var auth = _authService.GetById(id);
            if (auth == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = auth.Id;
            model.Code = auth.Code;
            model.Name = auth.Name;
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_AUTH_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Auth.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var auth = _authService.GetById(model.Id);
            if (auth == null)
            {
                return View("_ErrorNotExist");
            }
            auth.Code = model.Code;
            auth.Name = model.Name;

            if (model.SubmitType == "Edit")
            {
                var result = _authService.Update(auth);
                if (result<=0)
                {
                    ViewBag.ErrorMessage ="Not Edited";
                    return View(model);
                }
            }
            if (model.SubmitType == "Delete")
            {
                var result = _authService.Delete(model.Id, SessionHelper.CurrentUser.Id);
                if (result<=0)
                {
                    ViewBag.ErrorMessage ="Not Deleted";
                    return View(model);
                }
            }
            return RedirectToAction(nameof(AuthController.List));
        }
    }
}
