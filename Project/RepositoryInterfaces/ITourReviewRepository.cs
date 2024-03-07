using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourReviewRepository
    {
        public int Add(TourReview tourReview);
        public List<TourReview> GetAll();
        public List<TourReview> GetGuestsReview(int id);
        public void Remove(int id);
        public TourReview GetById(int id);
        public List<TourReview> GetByAppointment(int appointmentId);
        public void MarkAsInvalid(int id);
        public bool IsValid(int id);
        public void NotifyObservers();
        public void Subscribe(IObserver observer);
        public void Unsubscribe(IObserver observer);

    }
}
