using Hotel.Business.İnterfaces;
using Hotel.Business.Models.Reservation;
using Hotel.Data;
using Hotel.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hotel.Business
{
    public class ReservationService : IReservationService
    {
        private IConfiguration _config;

        public ReservationService(IConfiguration config)
        {
            _config = config;
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> resultList = new List<Reservation>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Reservation.AsNoTracking().ToList());
            }
            return resultList;
        }
        public List<RoomListWithDetail> GetAllRoomListWithDetail()
        {
            List<RoomListWithDetail> resultList = new List<RoomListWithDetail>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var roomList = dbContext.Room.AsNoTracking().ToList();
                var reservationRoomList = dbContext.Reservation.Where(x => x.ReleaseDate == null && x.EntryDate != null).ToList();
                foreach (var room in roomList)
                {
                    if (reservationRoomList.Where(x=>x.RoomId == room.Id).Any())
                    {
                        resultList.Add(new RoomListWithDetail()
                        {
                             RoomId= room.Id,
                             RoomName = room.Name,
                             IsEmpty = false
                        });
                    }
                    else
                    {
                        resultList.Add(new RoomListWithDetail()
                        {
                            RoomId = room.Id,
                            RoomName = room.Name,
                            IsEmpty = true
                        });
                    }
                }
            }
            return resultList;
        }

        public RoomListWithDetail GetRoomByRoomIdWithDetail(int roomId)
        {
            RoomListWithDetail result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var room = dbContext.Room.Where(x=>x.Id ==roomId).AsNoTracking().FirstOrDefault();
                if (room==null)
                {
                    return result;
                }
                var reservationRoom = dbContext.Reservation.Where(x =>x.RoomId == roomId&& x.ReleaseDate == null && x.EntryDate != null ).FirstOrDefault();
                if (reservationRoom ==null)
                {
                    result = new RoomListWithDetail()
                    { RoomId = room.Id,
                    IsEmpty = true,
                    RoomName =room.Name,
                    RoomPrice = room.Price
                    };
                }
                else
                {
                    result = new RoomListWithDetail()
                    {
                        RoomId = room.Id,
                        IsEmpty = false,
                        RoomName = room.Name,
                        RoomPrice = room.Price
                    };
                }
            }
            return result;
        }

        public List<Customer> GetAllCustomerWhichIsNotReservation()
        {
            List<Customer> resultList = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var reservationCustomeIdList = dbContext.Reservation.Where(x =>  x.ReleaseDate == null && x.EntryDate != null).Select(x=>x.CustomerId );
                resultList = dbContext.Customer.Where(x => !reservationCustomeIdList.Contains(x.Id)).AsNoTracking().ToList();
            }
            return resultList;
        }

        public Reservation GetById(int id)
        {
            Reservation result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Reservation.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public Reservation GetByRoomIdWhichIsEmptyFalse(int roomId)
        {
            Reservation result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Reservation.Where(x => x.ReleaseDate == null && x.EntryDate != null && x.RoomId ==roomId).AsNoTracking().FirstOrDefault();
            }

            return result;
        }

        public int Add(Reservation record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Reservation record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }

    }
}
