using Project.Model;
using Project.RepositoryInterfaces;
using Project.Serializer;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class MoveRequestRepository : ISubject, IMoveRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/moveRequests.csv";

        private readonly Serializer<MoveRequest> _serializer;

        private List<MoveRequest> _requests;

        private List<IObserver> _observers;

        public MoveRequestRepository()
        {
            _serializer = new Serializer<MoveRequest>();
            _requests = _serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }


        private void SaveInFile()
        {
            _serializer.ToCSV(FilePath, _requests);
        }

        private int GenerateId()
        {
            if (_requests.Count == 0) return 0;
            return _requests[_requests.Count - 1].Id + 1;
        }

        public MoveRequest Add(MoveRequest request)
        {
            request.Id = GenerateId();
            _requests.Add(request);
            SaveInFile();
            NotifyObservers();
            return request;
        }

        public MoveRequest Update(MoveRequest request)
        {
            MoveRequest oldRequest = GetRequestById(request.Id);
            if (oldRequest == null) return null;

            oldRequest.ReservationId = request.ReservationId;
            oldRequest.Status = request.Status;
            oldRequest.OwnerMessage = request.OwnerMessage;
            oldRequest.GuestMessage = request.GuestMessage;
            oldRequest.NewStartDate = request.NewStartDate;
            oldRequest.NewEndDate = request.NewEndDate;


            SaveInFile();
            NotifyObservers();
            return oldRequest;
        }

        public MoveRequest Remove(int id)
        {
            MoveRequest request = GetRequestById(id);
            if (request == null) return null;

            _requests.Remove(request);
            SaveInFile();
            NotifyObservers();
            return request;
        }

        public MoveRequest GetRequestById(int id)
        {
            return _requests.Find(v => v.Id == id);
        }

        public List<MoveRequest> GetAllRequests()
        {
            return _requests;
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
    }
}
