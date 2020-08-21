using Hotel.Business.Models.Reservation;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IReservationService
    {
        List<Reservation> GetAll();
        List<RoomListWithDetail> GetAllRoomListWithDetail();
        Reservation GetById(int id);
        int Add(Reservation record);
        int Update(Reservation record);
    }
}
