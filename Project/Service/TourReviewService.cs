using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourReviewService
    {
        private ITourReviewRepository tourReviewRepository;
        //TourReviewRepository tourReviewRepository;
        AppointmentService appointmentService;
        TourService tourService;
        UserRepository userRepository;

        public TourReviewService()
        {
            
            //tourReviewRepository = new TourReviewRepository();
            tourReviewRepository = Injector.Injector.CreateInstance<ITourReviewRepository>();
            appointmentService = new AppointmentService();
            tourService = new TourService();

            userRepository = new UserRepository();
        }


        public int Create(int appointmentId, int guestId, int knowledge, int language, int interesting,string text )
        {
            TourReview tourReview = new TourReview(appointmentId,guestId,knowledge,language,interesting,text);

            int tourReviewId = tourReviewRepository.Add(tourReview);

            return tourReviewId;

        }

        public TourReview GetById(int id)
        {
            return tourReviewRepository.GetById(id);
        }

        public List<TourReview> GetAll()
        {
            return tourReviewRepository.GetAll();
        }

        public List<TourReview> GetAll(int guideId)
        {
            List<TourReview> tourReviews = new List<TourReview>();
            List<TourReview> allTourReviews = new List<TourReview>(tourReviewRepository.GetAll());

            foreach (TourReview tourReview in allTourReviews)
            {
                Tour tour = new Tour();
                int tourId = appointmentService.GetTourId(tourReview.AppointmentId);
                tour = tourService.GetById(tourId);
                if(tour.GuideId == guideId)
                {
                    tourReviews.Add(tourReview);
                }
            }
            return tourReviews;
        }


        public List<ReviewDisplay> GetReviewForDisplay(int guideId)
        {
            List<ReviewDisplay> list = new List<ReviewDisplay>();


            List<TourReview> tourReviews = GetAll(guideId);

            foreach (TourReview review in tourReviews)
            {
                ReviewDisplay sm = new ReviewDisplay();
                int tourId = appointmentService.GetTourId(review.AppointmentId);
                sm.Id = review.Id;
                sm.tourName = tourService.GetById(tourId).Name;
                sm.userName = userRepository.GetById(review.GuestId).Username;
                sm.appointment = appointmentService.GetById(review.AppointmentId).DateAndTimeOfAppointment;
                sm.avgRating = Math.Round(((double)review.InterestingRating + (double)review.GuideLanguageRating + (double)review.GuideKnowledgeRating) / 3, 2);
                sm.validity = review.IsValid;
                sm.review = review.ReviewText;
                sm.appointmentId = review.AppointmentId;
                sm.userId = review.GuestId;
                sm.knowledgeRating = review.GuideKnowledgeRating;
                sm.languageRating = review.GuideLanguageRating;
                sm.interestingRating = review.InterestingRating;

                list.Add(sm);
            }


            return list;
        }

        public List<TourReview> GetGuestsReviews(int id)
        {
            return tourReviewRepository.GetGuestsReview(id);

        }

        public void MarkAsInvalid(int id)
        {
            tourReviewRepository.MarkAsInvalid(id);
        }

        public bool IsValid(int id)
        {
            return tourReviewRepository.IsValid(id);
        }

        public void Subscribe(IObserver observer)
        {
            tourReviewRepository.Subscribe(observer);
        }

        public List<TourReview> GetByAppointment(int appointmentId)
        {
            return tourReviewRepository.GetByAppointment(appointmentId);
        }

        public double GetAvgRatingForAppointment(int appointmentId)
        {
            List<TourReview> tourReviews = GetByAppointment(appointmentId);
            double sum = 0;
            foreach(TourReview tr in tourReviews)
            {
                double avg = 0;
                avg = (tr.GuideLanguageRating + tr.GuideKnowledgeRating + tr.InterestingRating) / 3;
                sum += avg;
            }

            if (tourReviews.Count() > 0)
            {
                return sum / tourReviews.Count();
            }
            else return 0;
            
        }


    }
}
