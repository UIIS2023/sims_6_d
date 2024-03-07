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
    public class SuperGuideService
    {
        private readonly ISuperGuideRepository _superGuideRepository;
        private readonly TourService _tourService;
        private readonly TourReviewService tourReviewService;

        public SuperGuideService()
        {
            _superGuideRepository = Injector.Injector.CreateInstance<ISuperGuideRepository>();
            _tourService = new TourService();
            tourReviewService = new TourReviewService();
        }

        public int Create(int guideId,string language)
        {
            SuperGuide superGuide = new SuperGuide(guideId,language);
            int superGuideId = _superGuideRepository.Add(superGuide);
            return superGuideId;
        }

        public bool IsSuper(int guideId)
        {
            List<SuperGuide> super = GetAll();
            foreach (SuperGuide superGuide in super)
            {
                if (guideId == superGuide.GuideId)
                {
                    return true;
                }
            }
            return false;
        }

        public List<SuperGuide> GetAll()
        {
            return _superGuideRepository.GetAll();
        }

        public SuperGuide GetById(int id)
        {
            return _superGuideRepository.GetById(id);
        }

        public SuperGuide GetByGuideId(int guideId, string language)
        {
            return _superGuideRepository.GetByGuideAndLanguage(guideId, language);
        }

        public void RemoveByGuideAndLanguage(int guideId, string language)
        {
            _superGuideRepository.RemoveByGuideAndLanguage(guideId,language);
        }

        public void Subscribe(IObserver observer)
        {
            _superGuideRepository.Subscribe(observer);
        }

        public List<string> SuperLanguages(int guideId)
        {
            List<string> languages = new List<string>();

            Dictionary<string, int> counter = new Dictionary<string, int>();
            Dictionary<string, double> sumCounter = new Dictionary<string, double>();
            List<Tour> completedTours = new List<Tour>();

            foreach (Tour tour in _tourService.GetCompletedTours(guideId))
            {
                if (tour.TourAppointment.DateAndTimeOfAppointment.Year == DateTime.Today.AddYears(-1).Year)
                {
                    completedTours.Add(tour);   
                }
            }

            int value;
            double value2;

            foreach (Tour tour in completedTours)
            {
                string id = tour.Language;


                if (counter.TryGetValue(id, out value))
                {
                    counter[id] = value + 1;
                    
                    if(sumCounter.TryGetValue(id,out value2))
                    {
                        sumCounter[id] = value2 + tourReviewService.GetAvgRatingForAppointment(tour.TourAppointment.Id);
                    }
                    else
                    {
                        sumCounter[id] = tourReviewService.GetAvgRatingForAppointment(tour.TourAppointment.Id);
                    }

                }
                else
                {
                    counter[id] = 1;
                    if (sumCounter.TryGetValue(id, out value2))
                    {
                        sumCounter[id] = value2 + tourReviewService.GetAvgRatingForAppointment(tour.TourAppointment.Id);
                    }
                    else
                    {
                        sumCounter[id] = tourReviewService.GetAvgRatingForAppointment(tour.TourAppointment.Id);
                    }
                }
            }

            foreach(KeyValuePair<string, int> entry in counter)
            {
                if(entry.Value > 20 && (sumCounter[entry.Key] / entry.Value) > 4.5)
                {
                    languages.Add(entry.Key);
                }

            }


            return languages;

        }

        public void ClearAllSuperGuides()
        {
            _superGuideRepository.ClearAll();
        }


    }
}
