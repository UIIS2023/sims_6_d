using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public class Guest1Review : ISerializable
    {
        public int Id { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerBehaviour { get; set; }
        public string Comment { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public int RenovationEmergencyLevel { get; set; }
        public List<Guest1ReviewImage> Images { get; set; }

        public Guest1Review()
        {
            Reservation = new AccommodationReservation();
            Images = new List<Guest1ReviewImage>();
        }

        public Guest1Review(int cleanliness, int ownerBehaviour, string comment, int reservationId, AccommodationReservation reservation = null, int renovationEmergencyLevel = 0)
        {
            Cleanliness = cleanliness;
            OwnerBehaviour = ownerBehaviour;
            Comment = comment;
            ReservationId = reservationId;
            Reservation = reservation;
            Images = new List<Guest1ReviewImage>();
            RenovationEmergencyLevel = renovationEmergencyLevel;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Cleanliness.ToString(), OwnerBehaviour.ToString(), Comment, ReservationId.ToString(), RenovationEmergencyLevel.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Cleanliness = Convert.ToInt32(values[1]);
            OwnerBehaviour = Convert.ToInt32(values[2]);
            Comment = values[3];
            ReservationId = Convert.ToInt32(values[4]);
            RenovationEmergencyLevel = Convert.ToInt32(values[5]);
        }
    }
}
