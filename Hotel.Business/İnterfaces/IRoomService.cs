using Hotel.Business.Models;
using Hotel.Business.Models.Room;
using Hotel.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.İnterfaces
{
    public interface IRoomService
    {
        PaginatedList<Room> GetAllPaginatedWithDetailBySearchFilter(RoomSearchFilter searchFilter);
        List<Room> GetAll();
        Room GetById(int id);
        int Add(Room record);
        int Update(Room record);
    }
}
