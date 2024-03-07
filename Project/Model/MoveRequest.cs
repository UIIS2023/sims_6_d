using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public enum MoveRequestStatus { PENDING, ACCEPTED, DECLINED}
    public class MoveRequest : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public string OwnerMessage { get; set; }
        public string GuestMessage { get; set; }

        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public MoveRequestStatus Status { get; set;}

        public MoveRequest() 
        {
            OwnerMessage = string.Empty;
            GuestMessage = string.Empty;
            NewStartDate = DateTime.MinValue;
            NewEndDate = DateTime.MinValue;
        }

        public MoveRequest(int reservationId, MoveRequestStatus status, string ownerMessage = "", string guestMessage = "", DateTime newStartDate = default, DateTime newEndDate = default)
        {
            ReservationId = reservationId;
            OwnerMessage = ownerMessage;
            GuestMessage = guestMessage;
            Status = status;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), OwnerMessage, GuestMessage, RequestStatusToString(), NewStartDate.ToString(), NewEndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
            OwnerMessage = values[2];
            GuestMessage = values[3];
            Status = StringToRequestStatus(values[4]);
            NewStartDate = Convert.ToDateTime(values[5]);
            NewEndDate = Convert.ToDateTime(values[6]);
        }

        private string RequestStatusToString()
        {
            if (Status == MoveRequestStatus.PENDING)
            {
                return "PENDING";
            }
            else if (Status == MoveRequestStatus.ACCEPTED)
            {
                return "ACCEPTED";
            }
            else
            {
                return "DECLINED";
            }

        }

        private MoveRequestStatus StringToRequestStatus(string status)
        {
            if (status == "PENDING")
            {
                return MoveRequestStatus.PENDING;
            }
            else if (status == "ACCEPTED")
            {
                return MoveRequestStatus.ACCEPTED;
            }
            else
                return MoveRequestStatus.DECLINED;

        }



    }
}
