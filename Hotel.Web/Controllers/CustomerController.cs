using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Customer;
using Hotel.Web.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            CustomerSearchFilter searchFilter = new CustomerSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;

            model.DataList = _customerService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_LIST)]
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

            CustomerSearchFilter searchFilter = new CustomerSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            searchFilter.Filter_LastName = model.Filter.Filter_LastName;
            model.DataList = _customerService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_ADD)]
        public ActionResult Add()
        {
            Models.Customer.AddViewModel model = new AddViewModel();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Customer.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Data.Entity.Customer customer = new Data.Entity.Customer();
            customer.TC = model.TC;
            customer.Name = model.Name;
            customer.LastName = model.LastName;
            customer.Phone = model.Phone;
            var result = _customerService.Add(customer);
            if (result > 0)
            {
                return RedirectToAction(nameof(CustomerController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                return View(model);
            }
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Customer.AddViewModel model = new AddViewModel();
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = customer.Id;
            model.TC = customer.TC;
            model.Name = customer.Name;
            model.LastName = customer.LastName;
            model.Phone = customer.Phone;
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CUSTOMER_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Customer.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = _customerService.GetById(model.Id);
            if (customer == null)
            {
                return View("_ErrorNotExist");
            }
            customer.TC = model.TC;
            customer.Name = model.Name;
            customer.LastName = model.LastName;
            customer.Phone = model.Phone;

        
                var result = _customerService.Update(customer);
                if (result <= 0)
                {
                    ViewBag.ErrorMessage = "Not Edited";
                    return View(model);
                }
        
          
            return RedirectToAction(nameof(CustomerController.List));
        }

    }
}
