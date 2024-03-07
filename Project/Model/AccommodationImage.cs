using System;
using Project.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

    public class AccommodationImage : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int AccommodationId { get; set; }


        public AccommodationImage() { }

        public AccommodationImage(int id, string url, int entityId)
        {
            Id = id;
            Url = url;
            AccommodationId = entityId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Url, AccommodationId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
            AccommodationId = Convert.ToInt32(values[2]);
        }


    }
}
