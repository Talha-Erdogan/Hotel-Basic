using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Business.Models.Reservation
{

    public class RoomListWithDetail 
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }


        public bool IsEmpty { get; set; }

    }
}
