using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationReservationReviewService
    {
        private readonly Guest1ReviewService _guest1ReviewService;
        private readonly AccommodationReservationService _reservationService;
        private readonly IOwnerReviewRepository _ownerReviewRepository;
        private readonly Guest1NotificationService _guest1NotificationService;

        public AccommodationReservationReviewService()
        {
            //_reservationService = Injector.Injector.CreateInstance<AccommodationReservationService>();
            //_guest1ReviewService = Injector.Injector.CreateInstance<Guest1ReviewService>();
            _reservationService = new AccommodationReservationService();
            _guest1ReviewService = new Guest1ReviewService();
            _guest1NotificationService = new Guest1NotificationService();
            _ownerReviewRepository = Injector.Injector.CreateInstance<IOwnerReviewRepository>();
            LinkReservationsAndReviews();
        }

        private void LinkReservationsAndReviews()
        {
            foreach (var review in _guest1ReviewService.GetAllReviews())
            {
                var reservation = _reservationService.GetReservationById(review.ReservationId);

                if (reservation != null)
                {
                    reservation.GuestReview = review;
                    review.Reservation = reservation;
                }
            }

            foreach (var review in _ownerReviewRepository.GetAllReviews())
            {
                var reservation = _reservationService.GetReservationById(review.ReservationId);

                if (reservation != null)
                {
                    reservation.OwnerReview = review;
                    review.Reservation = reservation;
                }
            }

        }

        public void FillGuestsGivenAndRecievedReviewsLists(ObservableCollection<Guest1Review> guestReviews, ObservableCollection<OwnerReview> ownerReviews, int guestId)
        {
            foreach (var reservation in _reservationService.GetGuestsFormerReservations(guestId))
            {
                if (reservation.OwnerReview == null && reservation.GuestReview == null)
                {
                    continue;
                }

                if (reservation.EndDate.AddDays(5).Date < DateTime.Now.Date)
                {
                    if (reservation.OwnerReview != null)
                    {
                        ownerReviews.Add(reservation.OwnerReview);
                    }
                    if (reservation.GuestReview != null)
                    {
                        guestReviews.Add(reservation.GuestReview);
                    }
                }
                else
                {
                    if (reservation.OwnerReview != null && reservation.GuestReview != null)
                    {
                        ownerReviews.Add(reservation.OwnerReview);
                        guestReviews.Add(reservation.GuestReview);
                    }
                    else if (reservation.OwnerReview != null && reservation.GuestReview == null)
                    {
                        continue;
                    }
                    else if (reservation.GuestReview != null)
                    {
                        guestReviews.Add(reservation.GuestReview);
                    }
                }
            }

        }

        public void FillOwnersGivenAndRecievedReviewsLists(ObservableCollection<Guest1Review> guestReviews, ObservableCollection<OwnerReview> ownerReviews, int ownerId)
        {
            foreach (var reservation in _reservationService.GetOwnersFormerReservations(ownerId))
            {
                if (reservation.OwnerReview == null && reservation.GuestReview == null)
                {
                    continue;
                }

                if (reservation.EndDate.AddDays(5).Date < DateTime.Now.Date)
                {
                    if (reservation.OwnerReview != null)
                    {
                        ownerReviews.Add(reservation.OwnerReview);
                    }
                    if (reservation.GuestReview != null)
                    {
                        guestReviews.Add(reservation.GuestReview);
                    }
                }
                else
                {
                    if (reservation.OwnerReview != null && reservation.GuestReview != null)
                    {
                        ownerReviews.Add(reservation.OwnerReview);
                        guestReviews.Add(reservation.GuestReview);
                    }
                    else if (reservation.OwnerReview != null && reservation.GuestReview == null)
                    {
                        continue;
                    }
                    else if (reservation.GuestReview != null)
                    {
                        guestReviews.Add(reservation.GuestReview);
                    }
                }
            }

        }

        public void RemindGuestToRate(int guestId)
        {
            foreach (var reservation in _reservationService.GetGuestsFormerReservations(guestId))
            {

                if (reservation.EndDate.AddDays(5).Date >= DateTime.Now.Date && reservation.GuestReview == null)
                {
                    string msg = $"You still have not rated reservation: Accommodation Name: {reservation.Accommodation.Name}, Start date: {reservation.StartDate}, End date: {reservation.EndDate} .";
                    Guest1Notification notification = new Guest1Notification(guestId, 0, msg);
                    _guest1NotificationService.Add(notification);
                }
            }
        }




    }
}
