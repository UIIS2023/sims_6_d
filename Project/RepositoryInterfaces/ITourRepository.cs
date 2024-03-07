using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface ITourRepository:ISubject
    {
        public int Add(Tour tour);
        public void Remove(int id);
        public Tour GetById(int id);
        public List<Tour> GetAll(int guideId);
        public List<Tour> GetAll();
        public List<Tour> FindAllAlternatives(Tour tour, int numberOfGuests);

    }
}
