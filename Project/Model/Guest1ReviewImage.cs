using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public class Guest1ReviewImage : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ReviewId { get; set; }


        public Guest1ReviewImage() { }

        public Guest1ReviewImage(string url, int entityId)
        {
            Url = url;
            ReviewId = entityId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Url, ReviewId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
            ReviewId = Convert.ToInt32(values[2]);
        }
    }
}
