using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourPointService
    {
        //TourPointRepository tourPointRepository;
        private ITourPointRepository tourPointRepository;

        public TourPointService()
        {
            tourPointRepository = Injector.Injector.CreateInstance<ITourPointRepository>();
        }

        public void Subscribe(IObserver observer)
        {
            tourPointRepository.Subscribe(observer);
        }
        public int Create(string name, bool action)
        {

            TourPoint tourPoint = new TourPoint(name, action);
            return tourPointRepository.Add(tourPoint);

        }

        public void UpdateAction(int id, bool action)
        {
            tourPointRepository.Update(id, action);
        }

        public TourPoint GetById(int id)
        {
            return tourPointRepository.GetById(id);
        }

        public string GetPointNameById(int pointId)
        {
            return GetById(pointId).PointName;
        }
    }
}
