using Project.Controller;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for ReserveTourSpotView.xaml
    /// </summary>
    public partial class ReserveTourSpotView : Window
    {
        public Guest2Controller Controller { get; set; }
        public Tour Tour { get; set; }

        public ObservableCollection<TourAppointments> TourSpots { get; set; }
        public ObservableCollection<string> GuestCoupons { get; set; }
        public ObservableCollection<Coupon> Coupons { get; set; }
        public Tour SelectedTour { get; set; }

        public string SelectedCoupon { get; set; }

        private readonly CouponService couponService;
        


        public ReserveTourSpotView(Guest2Controller controller, Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            Controller = controller;
            couponService = new CouponService(controller.Guest.User);
            Coupons = new ObservableCollection<Coupon>(couponService.GetGuest2Coupons());
            Tour = tour;
            TourSpots = new ObservableCollection<TourAppointments>();
            GuestCoupons = new ObservableCollection<string>();
            FillCouponsList();
        }

        private void btSearchAvailableTours_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckConditions()) return;

            //double numberOfGuests = Convert.ToDouble(tbGuests.Text);

            //List<TourReservation> reservationsInSameLocation = new List<TourReservation>(GetToursInSameLocation());
            FindAlternativesOnSameLocation(Tour);
        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private bool IsGuestsEmpty()
        {
            if (string.IsNullOrWhiteSpace(tbGuests.Text))
            {
                string sMessageBoxText = $"Please enter number of guests.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                string sCaption = "Missing input";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return true;
            }
            return false;
        }

        private bool IsGuestsDigits()
        {
            if (!IsDigitsOnly(tbGuests.Text))
            {
                string sMessageBoxText = $"Number of guests field must contain only digits.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Input error - Number of guests";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }
            return true;
        }

        private bool CheckMaxGuestsLimit(int numberOfGuests)
        {
            if(numberOfGuests > Tour.MaxGuests)
            {
                string sMessageBoxText = $"Maximum number of guests for this tour is {Tour.MaxGuests}";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Exceeded number of guests";
                MessageBox.Show(sMessageBoxText,sCaption, btnMessageBox, icnMessageBox);
                return false;

            }
            return true;
        }

        private bool CheckConditions()
        {
            if (IsGuestsEmpty()) return false;
            if(!IsGuestsDigits()) return false;

            int numberOfGuests = Convert.ToInt32(tbGuests.Text);
            if(!CheckMaxGuestsLimit(numberOfGuests)) return false;

            return true;
        }
        
        public void FindAlternativesOnSameLocation(Tour tour)
        {
            List<Tour> tours = Controller.FindAllAlternatives(tour, Convert.ToInt32(tbGuests.Text));
            alternativesDataGrid.ItemsSource = new ObservableCollection<Tour>(tours);
        }
        private List<TourReservation> GetToursInSameLocation()
        {
            List<TourReservation> reservations = new List<TourReservation>();
            List<TourReservation> toursInSameLocation = new List<TourReservation>();

            foreach(var reservation in reservations)
            {
                if(reservation.TourId == Tour.Id)
                {
                    if ((reservation.Tour.City == Tour.City) && (reservation.Tour.MaxGuests <= Tour.MaxGuests))
                        continue;
                    toursInSameLocation.Add(reservation);
                }

            }

            return toursInSameLocation;
        }

        private void btReserve_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour == null)
            {
                string sMessageBoxText = $"Choose a reservation first";
                string sCaption = "Reservation not chosen";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return;
            }

            var reservation = Controller.Guest.Reservations.Find(r => (r.TourId == SelectedTour.Id) && (r.StartDate == SelectedTour.TourAppointment.DateAndTimeOfAppointment));

            if(reservation != null)
            {
                string sMessageBoxText = $"YOu have already made this reservation!";
                string sCaption = "Reservation already exists";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to reserve spots for this tour?", "Confirm reservation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                if (SelectedCoupon != null)
                {
                    couponService.ChangeCouponToUsed(4);
                }
                TourReservation tourReservation = new TourReservation(SelectedTour.TourAppointment.TourId, new DateTime(), new DateTime(), 4, SelectedTour.Id);
                Controller.AddReservation(tourReservation);
                this.Close();
            }

        }

        private void cbCoupons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FillCouponsList()
        {
            foreach (var coupon in Controller.GetGuestsCoupons())
            {
                if (coupon.GuestId == Controller.Guest.User.Id)
                {
                    GuestCoupons.Add(coupon.Id.ToString() );
                }
            }


        }
    }
}
