using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Room;
using Hotel.Web.Filters;
using Hotel.Web.Models.Room;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            RoomSearchFilter searchFilter = new RoomSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            model.DataList = _roomService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_LIST)]
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

            RoomSearchFilter searchFilter = new RoomSearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;
            model.DataList = _roomService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_ADD)]
        public ActionResult Add()
        {
            Models.Room.AddViewModel model = new AddViewModel();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_ADD)]
        [HttpPost]
        public ActionResult Add(Models.Room.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Data.Entity.Room room = new Data.Entity.Room();
            room.Name = model.Name;
            room.Price = model.Price;
            room.Floor = model.Floor;
            var result = _roomService.Add(room);
            if (result > 0)
            {
                return RedirectToAction(nameof(RoomController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.Room.AddViewModel model = new AddViewModel();
            var room = _roomService.GetById(id);
            if (room == null)
            {
                return View("_ErrorNotExist");
            }

            model.Id = room.Id;
            model.Name = room.Name;
            model.Price = room.Price;
            model.Floor = room.Floor;
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOM_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.Room.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var room = _roomService.GetById(model.Id);
            if (room == null)
            {
                return View("_ErrorNotExist");
            }
            room.Name = model.Name;
            room.Price = model.Price;
            room.Floor = model.Floor;
            var result = _roomService.Update(room);
            if (result <= 0)
            {
                ViewBag.ErrorMessage = "Not Edited";
                return View(model);
            }
            return RedirectToAction(nameof(RoomController.List));
        }
    }
}
