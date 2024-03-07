using Project.Controller;
using Project.Model;
using Project.Service;
using Project.ViewModel.Guest1ViewModel;
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

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for ReserveAccommodationWindow.xaml
    /// </summary>
    public partial class ReserveAccommodationWindow : Window
    {
        /*//public Guest1Controller Controller { get; set; }

        private readonly AccommodationReservationService accommodationReservationService;
        private readonly User user;
        public Accommodation Accommodation { get; set; }

        int repetition = 0;

        public ObservableCollection<AccommodationReservation> FreeReservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        public ReserveAccommodationWindow(Accommodation accommodation, User u)
        {
            InitializeComponent();
            DataContext = this;
            //Controller = controller;
            accommodationReservationService = new AccommodationReservationService();
            Accommodation = accommodation;
            user = u;
            FreeReservations = new ObservableCollection<AccommodationReservation>();

        }


        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate < DateTime.Now.Date)
            {
                MessageBox.Show("You have not chosen valid end date!", "Input error: End date", MessageBoxButton.OK, MessageBoxImage.Error);
                dpEnd.SelectedDate = DateTime.Now.Date;
            }

        }

        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDate < DateTime.Now.Date)
            {
                string sMessageBoxText = $"You have not chosen valid start date!";
                string sCaption = "Input error: Start date";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;


                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                dpStart.SelectedDate = DateTime.Now.Date;
            }

        }

        private void btSearchFreeDates_Click(object sender, RoutedEventArgs e)
        {

            FindFreeDates(StartDate, EndDate);

        }

        private void FindFreeDates(DateTime startDate, DateTime endDate)
        {

            if (repetition == 0)
                if (!CheckConditions()) return;

            double days = Convert.ToDouble(tbDays.Text);

            double daysBetween = (endDate - startDate).TotalDays;

            FreeReservations.Clear();

            FillFreeReservationsList(startDate, endDate, days);

            RemoveReservedDates(startDate, endDate);

            if (FreeReservations.Count == 0)
            {

                repetition++;
                FindFreeDates(endDate.AddDays(1), endDate.AddDays(daysBetween + 1));

            }
            else if (FreeReservations.Count > 0 && repetition > 0)
            {
                tbNotFound.Text = $"We have not been able to find free dates in the next {repetition * (int)daysBetween} days. Here are some alternatives:";
                repetition = 0;
            }
            else
            {
                tbNotFound.Text = string.Empty;
            }


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

        private bool IsGuestsDigit()
        {
            if (!IsDigitsOnly(tbGuests.Text))
            {
                string sMessageBoxText = $"Number of guests field must contain only digits!";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Input error: Number of guests";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }

            return true;
        }

        private bool IsDaysEmpty()
        {
            if (string.IsNullOrWhiteSpace(tbDays.Text))
            {
                string sMessageBoxText = $"Please enter number of days.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                string sCaption = "Missing input";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return true;
            }
            return false;
        }

        private bool IsDaysDigit()
        {
            if (!IsDigitsOnly(tbDays.Text))
            {
                string sMessageBoxText = $"Number of days field must contain only digits!";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Input error: Number of days";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }

            return true;
        }

        private bool CheckMaxGuestsLimit(int numOfGuests)
        {
            if (numOfGuests > Accommodation.MaxGuests)
            {
                string sMessageBoxText = $"Maximum number of guests for this accommodation is {Accommodation.MaxGuests}!";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Exceeded number of guests";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }

            return true;
        }

        private bool CheckMinReservationLimit(int numOfDays)
        {
            if (numOfDays < Accommodation.MinReservationDays)
            {
                string sMessageBoxText = $"Minimal reservation for this accommodation is {Accommodation.MinReservationDays} days!";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Minimal reservation limit";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }

            return true;
        }

        private bool IsEndBeforeStart()
        {
            if (StartDate.Date > EndDate.Date)
            {
                string sMessageBoxText = $"Start date cannot be before end date!";
                string sCaption = "Date not valid";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;


                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                dpStart.SelectedDate = EndDate;

                return true;
            }

            return false;
        }

        private bool IsEndDateValid(double numOfDays)
        {
            if (StartDate.Date.AddDays(numOfDays) > EndDate.Date)
            {
                string sMessageBoxText = $"Chosen start and end date does not match entered numer of days!";
                string sCaption = "Date not valid";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;


                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                dpStart.SelectedDate = EndDate;

                return false;
            }

            return true;
        }

        private bool CheckConditions()
        {
            if (IsGuestsEmpty()) return false;

            if (IsDaysEmpty()) return false;

            if (!IsGuestsDigit()) return false;

            if (!IsDaysDigit()) return false;

            int guests = Convert.ToInt32(tbGuests.Text);

            if (!CheckMaxGuestsLimit(guests)) return false;

            double days = Convert.ToDouble(tbDays.Text);

            if (!CheckMinReservationLimit((int)days)) return false;


            // Date check
            if (IsEndBeforeStart()) return false;

            if (!IsEndDateValid(days)) return false;

            return true;
        }

        private List<AccommodationReservation> GetReservationsInRange(DateTime startDate, DateTime endDate)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();

            foreach (var reservation in accommodationReservationService.GetAllReservations())
            {
                if (reservation.AccommodationId == Accommodation.Id)
                {
                    if ((reservation.StartDate > endDate) || (reservation.EndDate < startDate))
                        continue;

                    reservations.Add(reservation);
                }


            }

            return reservations;
        }

        private void btReserve_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Choose a reservation first!", "Reservation not chosen",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            var reservation = accommodationReservationService.GetAllReservations().Find(r => (r.AccommodationId == SelectedReservation.AccommodationId) &&
                                                    (r.GuestId == user.Id) &&
                                                    (r.StartDate == SelectedReservation.StartDate) &&
                                                    (r.EndDate == SelectedReservation.EndDate));
            if (reservation != null)
            {
                MessageBox.Show("You have already made this reservation!", "Reservation already exists", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to reserve this accommodation at chosen date?\n\nAccommodation Name: {Accommodation.Name}\nNumber of guests: {tbGuests.Text}\nStart date: {SelectedReservation.StartDate}\nEnd date: {SelectedReservation.EndDate}", "Confirm reservation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SelectedReservation.Accommodation = Accommodation;
                SelectedReservation.Guest = user;
                accommodationReservationService.Add(SelectedReservation);
                Close();

            }

        }

        private List<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();
            double i = 0;
            while (startDate.AddDays(i) <= endDate)
            {
                dates.Add(startDate.AddDays(i));
                i++;
            }
            return dates;
        }

        private void RemoveReservedDates(DateTime startDate, DateTime endDate)
        {
            List<AccommodationReservation> reservations = new(GetReservationsInRange(startDate, endDate));
            List<AccommodationReservation> temp = new(FreeReservations);

            foreach (var reservation in temp)
            {

                List<AccommodationReservation> takenReservation = reservations.FindAll(r => reservation.StartDate > r.EndDate || reservation.EndDate < r.StartDate);

                if (takenReservation.Count() != reservations.Count())
                {
                    FreeReservations.Remove(reservation);
                }

            }
        }

        private void FillFreeReservationsList(DateTime startDate, DateTime endDate, double numOfDays)
        {
            List<DateTime> dates = new(GetDatesInRange(startDate, endDate));
            int guests = Convert.ToInt32(tbGuests.Text);

            foreach (var date in dates)
            {


                if (date.AddDays(numOfDays) > endDate)
                {
                    break;
                }

                AccommodationReservation reservation =
                    new(0, date, date.AddDays(numOfDays), user.Id, Accommodation.Id, guests);

                FreeReservations.Add(reservation);

            }
        }*/

        private readonly ReserveAccommodationViewModel viewModel;

        public ReserveAccommodationWindow(Accommodation accommodation, User user)
        {
            InitializeComponent();
            viewModel = new ReserveAccommodationViewModel(accommodation, user, this);
            this.DataContext = viewModel;
        }





    }
}
