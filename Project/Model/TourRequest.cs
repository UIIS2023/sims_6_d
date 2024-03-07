using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourRequest:ISerializable
    {
        public enum STATUS { ONHOLD,EXPIRED,ACCEPTED}
        public enum TYPE { REGULAR, COMPLEX}
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int GuestNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AcceptedAppointment { get; set; }
        public int GuideId { get; set; }
        public int GuestId { get; set; }
        public STATUS Status { get; set; }
        public TYPE RequestType { get; set; }
        public DateTime CreatingDate { get; set; }

        public TourRequest()
        {
            Id = -1;
            LocationId = -1;
            Location = new Location();
            Description = string.Empty;
            Language = string.Empty;
            GuestNumber = 0;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            AcceptedAppointment = DateTime.MinValue;
            GuideId = -1;
            GuestId = -1;
            Status = STATUS.ONHOLD;
            CreatingDate = DateTime.Today;
            RequestType = TYPE.REGULAR;
        }

        public TourRequest(int locationId, string description, string language, int guestNum,
                            DateTime startDate, DateTime endDate, DateTime acceptedAppointment, STATUS status, int guestId, TYPE requestType)
        {
            Id = -1;
            LocationId = locationId;
            Location = new Location();
            Description = description;
            Language = language;
            GuestNumber = guestNum;
            StartDate = startDate;
            EndDate = endDate;
            AcceptedAppointment = acceptedAppointment;
            GuideId = -1;
            GuestId = guestId;
            Status = status;
            CreatingDate = DateTime.Today;
            RequestType = requestType;
        }

        public TourRequest(int locationId, string description, string language, int guestNum, DateTime startDate, DateTime endDate, int guestId, TYPE requestType)
        {
            Id = -1;
            LocationId = locationId;
            Location = new Location();
            Description = description;
            Language = language;
            GuestNumber = guestNum;
            StartDate = startDate;
            EndDate = endDate;
            AcceptedAppointment = new DateTime();
            GuideId = -1;
            GuestId = guestId;
            Status = STATUS.ONHOLD;
            CreatingDate = DateTime.Today;
            RequestType = requestType;

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                LocationId.ToString(),
                Description,
                Language,
                GuestNumber.ToString(),
                StartDate.ToString("G"),
                EndDate.ToString("G"),
                AcceptedAppointment.ToString("G"),
                GuideId.ToString(),
                GuestId.ToString(),
                Status.ToString(),
                CreatingDate.ToString(),
                RequestType.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
            Description = values[2];
            Language = values[3];
            GuestNumber = int.Parse(values[4]);
            StartDate = DateTime.Parse(values[5]);
            EndDate = DateTime.Parse(values[6]);
            AcceptedAppointment = DateTime.Parse(values[7]);
            GuideId = int.Parse(values[8]);
            GuestId = int.Parse(values[9]);
            string status = values[10];
            CreatingDate = DateTime.Parse(values[11]);
            string type = values[12];
            switch (status)
            {
                case "ACCEPTED":
                    Status = STATUS.ACCEPTED;
                    break;
                case "EXPIRED":
                    Status = STATUS.EXPIRED;
                    break;
                default:
                    Status = STATUS.ONHOLD;
                    break;
            }

            switch(type)
            {
                case "COMPLEX":
                    RequestType = TYPE.COMPLEX;
                    break;
                default : 
                    RequestType = TYPE.REGULAR;
                    break;
            }
        }

    }
}
