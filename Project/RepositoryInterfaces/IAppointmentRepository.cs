using Project.Model;
using Project.Observer;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.RepositoryInterfaces
{
    public interface IAppointmentRepository: ISubject
    {
        public void Add(Appointment appointment);

        public void Remove(int id);

        public Appointment GetById(int id);

        public int GetTourIdById(int id);


        public Appointment GetByDateAndTour(DateTime date, int id);
        public List<Appointment> GetAll();

        public void Cancel(int id);
        public void RefreshAppointments();
        public void CompleteTour(int id);
    }
}
