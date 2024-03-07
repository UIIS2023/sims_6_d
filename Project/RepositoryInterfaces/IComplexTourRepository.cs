using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IComplexTourRepository: ISubject
    {
        public int Add(ComplexTour tour);
        public void Remove(int id);
        public ComplexTour GetById(int id);
        public List<ComplexTour> GetAll();
        public void MarkAsAccepted(int id);
    }
}
