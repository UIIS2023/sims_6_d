using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IMoveRequestRepository
    {
        public MoveRequest Add(MoveRequest request);

        public MoveRequest Update(MoveRequest request);

        public MoveRequest Remove(int id);

        public MoveRequest GetRequestById(int id);

        public List<MoveRequest> GetAllRequests();

        public void Subscribe(IObserver observer);        

        public void Unsubscribe(IObserver observer);

        public void NotifyObservers();
    }
}
