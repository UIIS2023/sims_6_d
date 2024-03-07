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
    public class ComplexTourRepository: IComplexTourRepository
    {
        private const string FilePath = "../../../Resources/Data/complexTours.csv";

        private readonly Serializer<ComplexTour> serializer;

        private readonly List<IObserver> _observers;

        private List<ComplexTour> complexTours;
        public ComplexTourRepository()
        {
            serializer = new Serializer<ComplexTour>();
            complexTours = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, complexTours);
        }

        private int GenerateId()
        {
            if (complexTours.Count == 0)
                return 0;

            return complexTours[complexTours.Count - 1].Id + 1;
        }

        public int Add(ComplexTour complexTour)
        {
            complexTour.Id = GenerateId();
            complexTours.Add(complexTour);
            SaveInFile();
            NotifyObservers();
            return complexTour.Id;

        }

        public void Remove(int id)
        {
            ComplexTour complexTour = GetById(id);

            complexTours.Remove(complexTour);
            SaveInFile();

        }

        public ComplexTour GetById(int id)
        {
            return complexTours.Find(v => v.Id == id);
        }

        public List<ComplexTour> GetAll()
        {
            return complexTours;
        }

        public void MarkAsAccepted(int id)
        {
            ComplexTour complexTour = complexTours.Find(v => v.Id == id);
            complexTour.Status = ComplexTour.STATUS.ACCEPTED;
            SaveInFile();
            NotifyObservers();
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
