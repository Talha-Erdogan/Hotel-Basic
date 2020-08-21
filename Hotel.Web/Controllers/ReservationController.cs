using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.Enums;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Reservation;
using Hotel.Data.Entity;
using Hotel.Web.Filters;
using Hotel.Web.Models.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;

        public ReservationController(IReservationService reservationService,ICustomerService customerService)
        {
            _reservationService = reservationService;
            _customerService = customerService;
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOMRESERVATION_OPERATION)]
        public IActionResult List()
        {
            List<RoomListWithDetail> model = new List<RoomListWithDetail>();
            model = _reservationService.GetAllRoomListWithDetail();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOMRESERVATION_OPERATION)]
        public ActionResult Add(int roomId)
        {
            if (roomId <= 0)
            {
                return View("_ErrorNotExist");
            }
            Models.Reservation.AddViewModel model = new AddViewModel();
            var roomWithDetail = _reservationService.GetRoomByRoomIdWithDetail(roomId);
            if (roomWithDetail.IsEmpty == false)
            {
                return View("_ErrorNotExist");
            }
            model.RoomId = roomWithDetail.RoomId;
            model.RoomPrice = roomWithDetail.RoomPrice;

            //select list
            model.CustomerSelectList = GetCustomerSelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOMRESERVATION_OPERATION)]
        [HttpPost]
        public ActionResult Add(Models.Reservation.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CustomerSelectList = GetCustomerSelectList();
                return View(model);
            }
            var reservation = _reservationService.GetRoomByRoomIdWithDetail(model.RoomId);
            if (reservation.IsEmpty == false)
            {
                return View("_ErrorNotExist");
            }

            Reservation record = new Reservation();
            record.CustomerId = model.CustomerId;
            record.RoomId = reservation.RoomId;
            record.CreatedDateTime = DateTime.Now;
            record.EntryDate = DateTime.Now;
            var result = _reservationService.Add(record);
            if (result > 0)
            {
                return RedirectToAction(nameof(ReservationController.List));
            }
            else
            {
                ViewBag.ErrorMessage = "Not Saved";
                model.CustomerSelectList = GetCustomerSelectList();
                return View(model);
            }
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOMRESERVATION_OPERATION)]
        public ActionResult Edit(int roomId)
        {
            if (roomId <= 0)
            {
                return View("_ErrorNotExist");
            }
            Models.Reservation.AddViewModel model = new AddViewModel();
            var roomWithDetail = _reservationService.GetRoomByRoomIdWithDetail(roomId);
            if (roomWithDetail.IsEmpty == true)
            {
                return View("_ErrorNotExist");
            }

            var reservationResponce = _reservationService.GetByRoomIdWhichIsEmptyFalse(roomId);
            if (reservationResponce == null)
            {
                return View("_ErrorNotExist");
            }
            var customer = _customerService.GetById(reservationResponce.CustomerId);
            model.Customer_Name = customer.Name + " " + customer.LastName;

            model.Id = reservationResponce.Id;
            model.RoomId = reservationResponce.RoomId;
            model.CustomerId = reservationResponce.CustomerId;
            model.RoomPrice = roomWithDetail.RoomPrice;

            model.TotalDebt = (Convert.ToInt32((DateTime.Now - reservationResponce.EntryDate.Value).TotalDays));
            if (model.TotalDebt==0)
            {
                model.TotalDebt = roomWithDetail.RoomPrice;
            }

            model.CustomerSelectList = GetCustomerSelectList();
            return View(model);
        }

        [AppAuthorizeFilter(AuthCodeStatic.PAGE_ROOMRESERVATION_OPERATION)]
        [HttpPost]
        public ActionResult Edit(Models.Reservation.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CustomerSelectList = GetCustomerSelectList();
                return View(model);
            }

            var reservation = _reservationService.GetById(model.Id);
            if (reservation.ReleaseDate != null)
            {
                return View("_ErrorNotExist");
            }

            reservation.ReleaseDate = DateTime.Now;
            reservation.TotalDebt = (Convert.ToInt32((DateTime.Now - reservation.EntryDate.Value).TotalDays));
            if (reservation.TotalDebt == 0)
            {
                reservation.TotalDebt = model.TotalDebt;
            }

            var result = _reservationService.Update(reservation);
            if (result <= 0)
            {
                ViewBag.ErrorMessage = "Not Edited";
                model.CustomerSelectList = GetCustomerSelectList();

                return View(model);
            }
            return RedirectToAction(nameof(ReservationController.List));
        }




        [NonAction]
        private List<SelectListItem> GetCustomerSelectList()
        {
            //müşteri kayıtları listelenir
            List<SelectListItem> resultList = new List<SelectListItem>();
            resultList = _reservationService.GetAllCustomerWhichIsNotReservation().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return resultList;
        }
    }
}
