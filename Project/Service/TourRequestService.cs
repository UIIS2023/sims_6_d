using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        private readonly LocationService locationService;
        public TourRequestService()
        {
            tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
            locationService = new LocationService();
        }

        public void Create(int locationId, string description, string language, int guestNum, DateTime startDate, DateTime endDate, DateTime acceptedAppointment, TourRequest.STATUS status,int guestId, TourRequest.TYPE requestType)
        {
            TourRequest tourRequest = new TourRequest(locationId,description,language,guestNum,startDate,endDate,acceptedAppointment,status,guestId, requestType);

            int requestId = tourRequestRepository.Add(tourRequest);
        }

        public TourRequest GetById(int id)
        {
            TourRequest tr = tourRequestRepository.GetById(id);
            tr.Location = locationService.GetById(tr.LocationId);
            return tr;
        }

        public List<TourRequest> GetAllRegular()
        {
            List<TourRequest> requests = new List<TourRequest>();

            foreach(TourRequest req in tourRequestRepository.GetAll())
            {
                if(req.RequestType == TourRequest.TYPE.REGULAR)
                {
                    req.Location = locationService.GetById(req.LocationId);
                    requests.Add(req);
                }
                
            }

            return requests;
        }

        public List<TourRequest> GetAllNotAcceptedAndNotExpired()
        {
            tourRequestRepository.MarkAsExpired();
            List<TourRequest> requests = new List<TourRequest>();

            foreach (TourRequest req in tourRequestRepository.GetAll())
            {
                req.Location = locationService.GetById(req.LocationId);
                if(req.Status != TourRequest.STATUS.ACCEPTED && req.Status != TourRequest.STATUS.EXPIRED && req.RequestType == TourRequest.TYPE.REGULAR)
                {
                    requests.Add(req);
                }
                
            }
            return requests;
        }

        public List<TourRequest> GetAllGuestsTourRequests(int guestId)
        {
            tourRequestRepository.MarkAsExpired();
            List<TourRequest> requests = new List<TourRequest>();
            foreach(TourRequest request in tourRequestRepository.GetAll())
            {
                if(request.GuestId == guestId)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        public List<TourRequest> GetAllGuideAccepted(int guideId)
        {
            List<TourRequest> requests = new List<TourRequest>();
            foreach (TourRequest request in tourRequestRepository.GetAll())
            {
                if (request.GuideId == guideId)
                {
                    requests.Add(request);
                }
            }

            return requests;

        }

        public List<TourRequest> GetAllGuideAcceptedThisYear(int guideId)
        {
            List<TourRequest> requests = new List<TourRequest>();
            foreach (TourRequest request in tourRequestRepository.GetAll())
            {
                if (request.GuideId == guideId && request.AcceptedAppointment.Year == DateTime.Today.Year)
                {
                    requests.Add(request);
                }
            }

            return requests;

        }

        public void Remove(int id)
        {
            tourRequestRepository.Remove(id);
        }

        public void MarkAsAccepted(int id)
        {
            tourRequestRepository.MarkAsAccepted(id);
        }

        public void AddAcceptedAppointment(int id, DateTime date, int guideId)
        {
            tourRequestRepository.AddAcceptedAppointment(id, date, guideId);
        }
    }
}
