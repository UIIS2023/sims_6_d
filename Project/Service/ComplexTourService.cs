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
    public class ComplexTourService
    {
        private readonly IComplexTourRepository _complexTourRepository;
        private readonly TourRequestService _tourRequestService;

        public ComplexTourService()
        {
            _tourRequestService = new TourRequestService();
            _complexTourRepository = Injector.Injector.CreateInstance<IComplexTourRepository>();
            
        }

        public int Create(string name,int guestId,List<TourRequest> list)
        {
            List<int> partsIds = new List<int>();

            foreach (var part in list)
            {
                partsIds.Add(part.Id);
            }

            ComplexTour complexTour = new ComplexTour(name,partsIds,guestId,ComplexTour.STATUS.ONHOLD);
            int ComplexTourId = _complexTourRepository.Add(complexTour);
            return ComplexTourId;
        }


        public List<ComplexTour> GetAll()
        {
            List<ComplexTour> complexTours = _complexTourRepository.GetAll();

            foreach (var complexTour in complexTours)
            {

                    complexTour.complexTourParts.Clear();
                    foreach (int partId in complexTour.complexTourPartsIds)
                    {
                        complexTour.complexTourParts.Add(_tourRequestService.GetById(partId));
                    }
                

            }

            return complexTours;
        }

        public List<ComplexTour> GetAllOnHold()
        {
            List<ComplexTour> complexTours = _complexTourRepository.GetAll();
            List<ComplexTour> onHold = new List<ComplexTour>();
            
            foreach(ComplexTour ct in complexTours)
            {
                if(ct.Status == ComplexTour.STATUS.ONHOLD)
                {
                    onHold.Add(ct);
                }
            }

            foreach (var complexTour in onHold)
            {
                
                    complexTour.complexTourParts.Clear();
                    foreach (int partId in complexTour.complexTourPartsIds)
                    {
                        complexTour.complexTourParts.Add(_tourRequestService.GetById(partId));
                    }
                

            }

            return onHold;
        }


        public List<ComplexTour> GetAllForGuest(int guestId)
        {
            List<ComplexTour> allComplexTours = GetAll();
            List<ComplexTour> guestsComplexTours = new List<ComplexTour>();

            foreach (var complexTour in allComplexTours)
            {
                if(complexTour.GuestId == guestId)
                {
                    guestsComplexTours.Add(complexTour);
                }
            }

            return guestsComplexTours;
        }

        public void CheckStatus()
        {
            foreach(var complexTour in GetAll())
            {
                int br = 0;

                foreach(TourRequest req in complexTour.complexTourParts)
                {
                    if(req.Status == TourRequest.STATUS.ACCEPTED)
                    {
                        br++;
                    }
                }
                if(br == complexTour.complexTourParts.Count)
                {
                    MarkAsAccepted(complexTour.Id);
                }
            }
        }

        public ComplexTour GetById(int id)
        {
            return _complexTourRepository.GetById(id);
        }

        public void MarkAsAccepted(int id)
        {
            _complexTourRepository.MarkAsAccepted(id);
        }

        public void Subscribe(IObserver observer)
        {
            _complexTourRepository.Subscribe(observer);
        }
    }
}
