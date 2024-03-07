using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Project.Model
{
    public class OwnerNotification : Serializer.ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int OwnerId { get; set; }
        public string Message { get; set; }

        public OwnerNotification() { }

        public OwnerNotification(int guestId, int ownerId, string message)
        {
            GuestId = guestId;
            OwnerId = ownerId;
            Message = message;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), OwnerId.ToString(),  Message };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            Message = values[3];
        }
    }
}
