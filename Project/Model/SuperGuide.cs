using Project.Serializer;
using ScottPlot.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Project.Model
{
    public class SuperGuide: ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public string Language { get; set; }

        public SuperGuide()
        {
            Id = -1;
            GuideId = -1;
            Language = string.Empty;
        }

        public SuperGuide(int guideId, string language)
        {
            Id = -1;
            GuideId = guideId;
            Language = language;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                GuideId.ToString(),
                Language,

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuideId = int.Parse(values[1]);
            Language = values[2];
        }
    }
}
