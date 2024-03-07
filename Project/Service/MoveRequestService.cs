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
    public class MoveRequestService
    {
        private IMoveRequestRepository _requestRepository;

        private AccommodationReservationService _reservationService;

        public MoveRequestService()
        {
            _requestRepository = Injector.Injector.CreateInstance<IMoveRequestRepository>();
            _reservationService = new AccommodationReservationService();
            LinkRequestsAndReservations();
        }

        public void Add(MoveRequest request)
        {
            _requestRepository.Add(request);
        }

        public bool DoesRequestAlreadyExist(AccommodationReservation reservation)
        {
            MoveRequest request = _requestRepository.GetAllRequests().Find(r => (r.ReservationId == reservation.Id) && (r.Status == MoveRequestStatus.PENDING));

            return request != null;
        }

        public List<MoveRequest> GetGuestsPendingRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsAllRequests(guestId));

            foreach (var request in GetGuestsAllRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.PENDING)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        public List<MoveRequest> GetGuestsAcceptedRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsAllRequests(guestId));

            foreach (var request in GetGuestsAllRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.ACCEPTED)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        public List<MoveRequest> GetGuestsDeclinedRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsAllRequests(guestId));

            foreach (var request in GetGuestsAllRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.DECLINED)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        private List<MoveRequest> GetGuestsAllRequests(int guestId)
        {
            List<MoveRequest> requests = new();

            foreach (var request in _requestRepository.GetAllRequests())
            {
                if (request.Reservation.GuestId == guestId)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        private void LinkRequestsAndReservations()
        {
            foreach (var request in _requestRepository.GetAllRequests())
            {
                AccommodationReservation reservation = _reservationService.GetAllReservations().Find(r => r.Id == request.ReservationId);

                if(reservation != null)
                {
                    request.Reservation = reservation;
                }
                    
            }
        }

        public void SubscribeToRepository(IObserver observer)
        {
            _requestRepository.Subscribe(observer);
        }

        public bool IsAccommodationFree(AccommodationReservation reservation, DateTime startDate, DateTime endDate)
        {
            return _reservationService.IsAccommodationFree(startDate, endDate, reservation.AccommodationId);
        }


    }
}
