using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View.TourGuideView
{
    public class ReviewDisplay
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }
        public string tourName { get; set; }
        public DateTime appointment { get; set; }
        public int knowledgeRating { get; set; }
        public int languageRating { get; set; }
        public int interestingRating { get; set; }
        public double avgRating { get; set; }
        public bool validity { get; set; }
        public string review { get; set; }
        public int appointmentId { get; set; }

        public ReviewDisplay()
        {
            Id = -1;
            userName = "";
            userId = -1;
            tourName = "";
            appointment = DateTime.Now;
            avgRating = 0;
            knowledgeRating = 0;
            languageRating = 0;
            interestingRating = 0;
            validity = true;
            review = "";
            appointmentId = 0;

        }

        public ReviewDisplay(string userName,int userid, string tourName, DateTime appointment, double avgRating, bool validity,string revw, int appId)
        {
            this.userName = userName;
            this.userId = userid;
            this.tourName = tourName;
            this.appointment = appointment;
            this.avgRating = avgRating;
            this.validity = validity;
            this.review = revw;
            this.appointmentId = appId;
        }
    }
}
