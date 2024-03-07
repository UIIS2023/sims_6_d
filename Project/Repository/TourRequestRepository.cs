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
    public class TourRequestRepository : ISubject,ITourRequestRepository
    {

        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<TourRequest> serializer;

        private readonly List<IObserver> _observers;

        private List<TourRequest> requests;
        public TourRequestRepository()
        {
            serializer = new Serializer<TourRequest>();
            requests = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, requests);
        }

        private int GenerateId()
        {
            if (requests.Count == 0)
                return 0;

            return requests[requests.Count - 1].Id + 1;
        }

        public int Add(TourRequest request)
        {
            request.Id = GenerateId();
            requests.Add(request);
            SaveInFile();
            NotifyObservers();
            return request.Id;

        }

        public void Remove(int id)
        {
            TourRequest request = GetById(id);

            requests.Remove(request);
            SaveInFile();

        }

        public TourRequest GetById(int id)
        {
            return requests.Find(v => v.Id == id);
        }

        public List<TourRequest> GetAll()
        {
            return requests;
        }

        public void MarkAsAccepted(int id)
        {
            TourRequest tourRequest = requests.Find(v => v.Id == id);
            tourRequest.Status = TourRequest.STATUS.ACCEPTED;
            SaveInFile();
            NotifyObservers();
        }
        
        public void MarkAsExpired()
        {
            foreach(var request in requests)
            {
                TimeSpan timeRemaining = request.StartDate - DateTime.Now;
                if(timeRemaining.TotalHours < 48 && request.Status == TourRequest.STATUS.ONHOLD)
                {
                    request.Status = TourRequest.STATUS.EXPIRED;
                    SaveInFile();
                    NotifyObservers();
                }
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

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void AddAcceptedAppointment(int id, DateTime date, int guideId)
        {
            TourRequest tourRequest = requests.Find(v => v.Id == id);
            tourRequest.AcceptedAppointment = date;
            tourRequest.GuideId = guideId;
            SaveInFile();
            NotifyObservers();
        }
    }
}
