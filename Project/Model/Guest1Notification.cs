using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    //public enum NotificationType { INFO, WARNING, ERROR, SUCCESS}
    public class Guest1Notification : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }
        //public NotificationType Type { get; set; }
        public string Message { get; set; }


        public Guest1Notification() { }

        public Guest1Notification(int guestId, int ownerId, /*NotificationType type,*/ string message)
        {
            GuestId = guestId;
            OwnerId = ownerId;
            //Type = type;
            Message = message;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), OwnerId.ToString(), Message };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            Message = values[3];
        }

        /*private string NotificationTypeToString()
        {
            if (Type == NotificationType.ERROR)
            {
                return "ERROR";
            }
            else if (Type == NotificationType.WARNING)
            {
                return "WARNING";
            }
            else if (Type == NotificationType.INFO)
            {
                return "INFO";
            }
            else
                return "SUCCESS";


        }

        private NotificationType StringToNotificationType(string type)
        {
            if (type == "ERROR")
            {
                return NotificationType.ERROR;
            }
            else if (type == "WARNING")
            {
                return NotificationType.WARNING;
            }
            else if (type == "INFO")
            {
                return NotificationType.INFO;
            }
            else
                return NotificationType.SUCCESS;

        }*/


    }
}
