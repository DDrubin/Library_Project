using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookReservation
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public int BookID { get; set; }

        public string UserID { get; set; }
    }
}
