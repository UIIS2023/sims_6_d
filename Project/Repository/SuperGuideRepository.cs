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
    public class SuperGuideRepository : ISuperGuideRepository
    {
        private const string FilePath = "../../../Resources/Data/superguides.csv";

        private readonly Serializer<SuperGuide> serializer;
        private readonly List<IObserver> _observers;

        private List<SuperGuide> superGuides;

        public SuperGuideRepository()
        {
            serializer = new Serializer<SuperGuide>();
            superGuides = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, superGuides);
        }

        private int GenerateId()
        {
            if (superGuides.Count == 0)
                return 0;

            return superGuides[superGuides.Count - 1].Id + 1;
        }
        public int Add(SuperGuide superGuide)
        {
            superGuide.Id = GenerateId();
            superGuides.Add(superGuide);
            SaveInFile();
            NotifyObservers();

            return superGuide.Id;
        }

        public List<SuperGuide> GetAll()
        {
            return superGuides;
        }

        public SuperGuide GetByGuideId(int guideId)
        {
            return superGuides.Find(v => v.GuideId == guideId);
        }

        public SuperGuide GetByGuideAndLanguage(int guideId, string language)
        {
            return superGuides.Find(v => v.GuideId == guideId && v.Language == language);
        }

        public SuperGuide GetById(int id)
        {
            return superGuides.Find(v => v.Id == id);
        }

        public void RemoveByGuideAndLanguage(int guideId, string language)
        {
            SuperGuide superGuide = GetByGuideAndLanguage(guideId,language);

            superGuides.Remove(superGuide);
            SaveInFile();
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void ClearAll()
        {
            superGuides.Clear();
            SaveInFile();
        }


    }
}
