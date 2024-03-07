using Microsoft.Win32.SafeHandles;
using Project.Model;
using Project.Observer;
using Project.RepositoryInterfaces;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class TourReviewRepository : ISubject, ITourReviewRepository
    {

        private const string FilePath = "../../../Resources/Data/tourreviews.csv";

        private readonly Serializer<TourReview> serializer;

        private readonly List<IObserver> _observers;

        private List<TourReview> tourReviews;


        public TourReviewRepository()
        {
            serializer = new Serializer<TourReview>();
            tourReviews = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourReviews);
        }

        private int GenerateId()
        {
            if (tourReviews.Count == 0)
                return 0;

            return tourReviews[tourReviews.Count - 1].Id + 1;
        }

        public int Add(TourReview tourReview)
        {
            tourReview.Id = GenerateId();
            tourReviews.Add(tourReview);
            SaveInFile();
            NotifyObservers();

            return tourReview.Id;

        }


        public List<TourReview> GetAll()
        {
            return tourReviews;
        }


        public List<TourReview> GetGuestsReview(int id)
        {
            List<TourReview> guestReviews = new List<TourReview>();
            List<TourReview> tourReviews = GetAll();
            foreach(var review in tourReviews)
            {
                if(review.GuestId == id)
                {
                    guestReviews.Add(review);
                }
            }
            return guestReviews;
        }

        public void Remove(int id)
        {
            TourReview tourReview = GetById(id);
            tourReviews.Remove(tourReview);
            SaveInFile();
            NotifyObservers();
        }

        public TourReview GetById(int id)
        {
            return tourReviews.Find(v => v.Id == id);
        }

        public void MarkAsInvalid(int id)
        {
            TourReview tourReview = tourReviews.Find(v => v.Id == id);
            tourReview.IsValid = false;
            SaveInFile();
            NotifyObservers();
        }

        public bool IsValid(int id)
        {
            TourReview tourReview = tourReviews.Find(v => v.Id == id);
            if(tourReview != null)
            {
                return tourReview.IsValid;
            }
            return false;
            
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public List<TourReview> GetByAppointment(int appointmentId)
        {
            List<TourReview> tourRevs = new List<TourReview>();
            foreach(TourReview tr in tourReviews)
            {
                if(tr.AppointmentId == appointmentId)
                {
                    tourRevs.Add(tr);
                }
            }
            return tourRevs;
        }
    }
}
