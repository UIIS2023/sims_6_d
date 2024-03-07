using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Guest2
    {
        public User User { get; set; }

        public List<TourReservation> Reservations { get; set; } 
        public List<Appointment> AppointmentsForReview { get; set; }
        public List<TourReservation> TourReservationForReview { get; set; }
        public List<TourReview> TourReviews { get; set; }
        public List<Coupon> Coupons { get; set; }

        public Guest2()
        {
            Reservations = new List<TourReservation>();
            AppointmentsForReview = new List<Appointment>();
            TourReservationForReview = new List<TourReservation>();
            TourReviews = new List<TourReview>();
            User = new User();
            Coupons = new List<Coupon>();
        }

        public Guest2(User user)
        {
            User = user;
            Reservations = new List<TourReservation>();
            AppointmentsForReview = new List<Appointment>();
            TourReservationForReview = new List<TourReservation>();
            TourReviews = new List<TourReview>();
            Coupons = new List<Coupon>();
        }
    }
}
