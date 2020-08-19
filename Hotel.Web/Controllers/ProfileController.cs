using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Common;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Profile;
using Hotel.Web.Models.Profile;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        // [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            model.DataList = _profileService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_LIST)]
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

            ProfileSearchFilter searchFilter = new ProfileSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Code = model.Filter.Filter_Code;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.DataList = _profileService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        public ActionResult Add()
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            return View(model);
        }

       // [AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Data.Entity.Profile profile = new Data.Entity.Profile();
            profile.Code = model.Code;
            profile.Name = model.Name;
            var result = _profileService.Add(profile);
            if (result>0)
            {
                return RedirectToAction(nameof(ProfileController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Profile.AddViewModel model = new AddViewModel();
            var profile = _profileService.GetById(id);
            if (profile == null)
            {
                return View("_ErrorNotExist");
            }
            model.Id = profile.Id;
            model.Code = profile.Code;
            model.Name = profile.Name;
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_PROFILE_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Profile.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var profile = _profileService.GetById(model.Id);
            if (profile == null)
            {
                return View("_ErrorNotExist");
            }
            profile.Code = model.Code;
            profile.Name = model.Name;
            if (model.SubmitType == "Edit")
            {
                var result = _profileService.Update(profile);
                if (result<=0)
                {
                    ViewBag.ErrorMessage = "Not Edited";
                    return View(model);
                }
            }
            if (model.SubmitType == "Delete")
            {
                var result = _profileService.Delete(model.Id, SessionHelper.CurrentUser.Id);
                if (result <= 0)
                {
                    ViewBag.ErrorMessage = "Not Deleted";
                    return View(model);
                }
            }
            return RedirectToAction(nameof(ProfileController.List));
        }
    }
}
