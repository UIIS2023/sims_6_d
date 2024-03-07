using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourReview: ISerializable
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int GuestId { get; set; }
        public int GuideKnowledgeRating { get; set; }
        public int GuideLanguageRating { get; set; }
        public int InterestingRating { get; set; }
        public string ReviewText { get; set; }
        public bool IsValid { get; set; }

        public TourReview()
        {
            Id = -1;
            AppointmentId = -1;
            GuestId = -1;
            GuideKnowledgeRating = -1;
            InterestingRating = -1;
            GuideLanguageRating = -1;
            ReviewText = "";
            IsValid = true;
        }

        public TourReview(int appointmentId, int guestId, int knowledgeRating,int languageRating,int interestingRating, string reviewText)
        {
            Id = -1;
            AppointmentId = appointmentId;
            GuestId = guestId;
            GuideKnowledgeRating = knowledgeRating;
            GuideLanguageRating = languageRating;
            InterestingRating = interestingRating;
            ReviewText = reviewText;
            IsValid = true;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                AppointmentId.ToString(),
                GuestId.ToString(),
                GuideKnowledgeRating.ToString(),
                GuideLanguageRating.ToString(),
                InterestingRating.ToString(),
                ReviewText,
                IsValid.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AppointmentId = int.Parse(values[1]);
            GuestId = int.Parse(values[2]);
            GuideKnowledgeRating = int.Parse(values[3]);
            GuideLanguageRating= int.Parse(values[4]);
            InterestingRating = int.Parse(values[5]);
            ReviewText = values[6];  
            IsValid = bool.Parse(values[7]);
        }
    }
}
