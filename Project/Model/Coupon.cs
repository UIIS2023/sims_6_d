using Project.Controller;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Project.Model
{
    public class Coupon: ISerializable
    {
        public enum STATUS { USED, NOTUSED}
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int GuideId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public STATUS Status { get; set; }

        public Coupon()
        {
            Id = -1;
            GuestId = -1;
            GuideId = -1;
            ExpiryDate = DateTime.Now;
            Status = STATUS.NOTUSED;
        }

        public Coupon(int guestId, DateTime dateOfExpire,int guideId)
        {   
            Id = -1;
            GuestId = guestId;
            GuideId = guideId;
            ExpiryDate = dateOfExpire;
            Status = STATUS.NOTUSED;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                GuestId.ToString(),
                GuideId.ToString(),
                ExpiryDate.ToString(),
                Status.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            GuideId = int.Parse(values[2]);
            ExpiryDate = DateTime.Parse(values[3]);
            string status = values[4];
            switch(status)
            {
                case "USED":
                    Status = STATUS.USED;
                    break;
                case "NOTUSED":
                    Status = STATUS.NOTUSED; 
                    break;
                default:
                    Status = STATUS.NOTUSED;
                    break;
            }
        }
    }
}
