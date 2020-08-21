using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult List()
        {
            List<RoomListWithDetail> model = new List<RoomListWithDetail>();
            model = _reservationService.GetAllRoomListWithDetail();
            return View(model);
        }
    }
}
