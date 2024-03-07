using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class OwnerReview : ISerializable // ovo je review koji vlasnik ostavlja za goste
    {

        public int Id { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int Cleanliness {get; set; }
        public int HousePolicies { get; set; }
        public string Comment { get; set; }

        public OwnerReview() { }

        public OwnerReview(int reservationId, int cleanliness, int housePolicies, string comment)
        {
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            HousePolicies = housePolicies;
            Comment = comment;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToInt32(values[2]);
            HousePolicies = Convert.ToInt32(values[3]);
            Comment = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Cleanliness.ToString(), HousePolicies.ToString(), Comment };
            return csvValues;
        }
    }
}
