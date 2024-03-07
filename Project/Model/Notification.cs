using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Notification:ISerializable
    {
        public int Id { get; set; }
        public int RecipientId { get; set; }
        public string Message { get; set; }
        public bool IsSended { get; set; }

        public Notification()
        {
            Id = -1;
            RecipientId = -1;
            Message = string.Empty;
            IsSended = false;
        }

        public Notification(int recipientId, string message)
        {
            Id = -1;
            RecipientId=recipientId;
            Message = message;
            IsSended=false;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(),
                RecipientId.ToString(),
                Message,
                IsSended.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            RecipientId =int.Parse(values[1]);
            Message = values[2];
            IsSended = bool.Parse(values[3]);
        }

    }
}
