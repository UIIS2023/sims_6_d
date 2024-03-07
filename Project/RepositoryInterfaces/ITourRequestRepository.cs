using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourRequestRepository:ISubject
    {
        public int Add(TourRequest request);
        public void Remove(int id);
        public TourRequest GetById(int id);
        public List<TourRequest> GetAll();
        public void MarkAsAccepted(int id);
        public void MarkAsExpired();
        public void AddAcceptedAppointment(int id,DateTime date, int guideId);
    }
}
