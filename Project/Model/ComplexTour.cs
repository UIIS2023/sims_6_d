using Project.Serializer;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
   
    public class ComplexTour: ISerializable
    {
        public enum STATUS { ONHOLD, ACCEPTED, INVALID }

        public int Id { get; set; }
        public string Name { get; set; }

        public int GuestId { get; set; }

        public List<int> complexTourPartsIds { get; set; }
        public List<TourRequest> complexTourParts { get; set; }
        public STATUS Status { get; set; }

        public ComplexTour()
        {
            Id = -1;
            Name = string.Empty;
            GuestId = -1;
            complexTourPartsIds = new List<int>();
            Status = STATUS.ONHOLD;
            complexTourParts =  new List<TourRequest>();
        }

        public ComplexTour(string name,List<int> parts,int guestId, STATUS status)
        {
            Id = -1;
            Name = name;
            GuestId = guestId;
            complexTourPartsIds = parts;
            Status = status;
            complexTourParts = new List<TourRequest>();
        }


        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                GuestId.ToString(),
                Status.ToString(),
                GetConcatedIds(complexTourPartsIds),
            };
            return csvValues;
        }

        public string GetConcatedIds(List<int> ids)
        {
            string concatenateString = "";
            foreach (int id in ids)
            {
                concatenateString += id.ToString() + "|";
            }

            return concatenateString;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            GuestId = int.Parse(values[2]);
            string status = values[3];
            switch (status)
            {
                case "ACCEPTED":
                    Status = STATUS.ACCEPTED;
                    break;
                case "INVALID":
                    Status = STATUS.INVALID;
                    break;
                default:
                    Status = STATUS.ONHOLD;
                    break;
            }
            for (int i = 4; i < values.Length - 1; i++)
            {
                complexTourPartsIds.Add(int.Parse(values[i]));
            }
        }

        

}
}
