using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourPointRepository: ISubject
    {
        public int Add(TourPoint tourPoint);
        public void Update(int id, bool action);
        public void Remove(int id);
        public TourPoint GetById(int id);
        public List<TourPoint> GetAll();
    }
}
