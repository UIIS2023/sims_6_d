using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public class Guest1RemindNotification : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public Guest1RemindNotification() { }

        public Guest1RemindNotification(int reservationId, DateTime date, string message)
        {
            ReservationId = reservationId;
            Date = date;
            Message = message;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Date.ToString(), Message };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Date = Convert.ToDateTime(values[2]);
            Message = values[3];
        }
    }
}
