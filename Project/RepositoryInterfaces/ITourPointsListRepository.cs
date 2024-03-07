using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourPointsListRepository : ISubject
    {
        public void Add(TourPointsList tourPointsList);
        public void Remove(int id);
        public TourPointsList GetById(int id);
        public TourPointsList GetByTourId(int id);
        public List<TourPointsList> GetAll();

    }
}
