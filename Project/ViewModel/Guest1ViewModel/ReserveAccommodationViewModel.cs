using Project.Command.Guest1Commands.SearchAccommodationsCommands;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.Guest1ViewModel
{
    public class ReserveAccommodationViewModel : ViewModelBase
    {
        private string _guests;
        public string Guests
        {
            get
            {
                return _guests;
            }
            set
            {
                if (value == "0") { return; }
                if (!IsDigitsOnly(value)) { return; }
                _guests = value;
                OnPropertyChanged(nameof(_guests));
            }
        }

        private string _days;
        public string Days
        {
            get
            {
                return _days;
            }
            set
            {
                if (value == "0") { return; }
                if (!IsDigitsOnly(value)) { return; }
                _days = value;
                OnPropertyChanged(nameof(_days));
            }
        }

        private string _notFoundText;
        public string NotFoundText
        {
            get
            {
                return _notFoundText;
            }
            set
            {
                _notFoundText = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(_startDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(_endDate));
            }
        }


        private readonly AccommodationReservationService accommodationReservationService;
        public Accommodation Accommodation { get; set; }

        int repetition = 0;

        public ObservableCollection<AccommodationReservation> FreeReservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }

        public ICommand ReserveCommand { get; }
        public ICommand SearchCommand { get; }



        public ReserveAccommodationViewModel(Accommodation accommodation, User u, Window window)
        {
            User = u;
            Window = window;

            accommodationReservationService = new AccommodationReservationService();
            Accommodation = accommodation;
            FreeReservations = new ObservableCollection<AccommodationReservation>();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;

            ReserveCommand = new ReserveAccommodationCommand(this, accommodationReservationService);
            SearchCommand = new SearchFreeDatesCommand(this);

        }


        public void FindFreeDates(DateTime startDate, DateTime endDate)
        {

            if (repetition == 0)
                if (!CheckConditions()) return;

            double days = Convert.ToDouble(Days);

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
                NotFoundText = $"We have not been able to find free dates in the next {repetition * (int)daysBetween} days. Here are some alternatives:";
                repetition = 0;
            }
            else
            {
                NotFoundText = string.Empty;
            }


        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private bool IsGuestsEmpty()
        {
            if (string.IsNullOrWhiteSpace(Guests))
            {
                MessageBox.Show("Please enter number of guests.", "Missing input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
        }

        private bool IsGuestsDigit()
        {
            if (!IsDigitsOnly(Guests))
            {
                MessageBox.Show("Number of guests field must contain only digits!", "Input error: Number of guests", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool IsDaysEmpty()
        {
            if (string.IsNullOrWhiteSpace(Days))
            {
                MessageBox.Show("Please enter number of days.", "Missing input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
        }

        private bool IsDaysDigit()
        {
            if (!IsDigitsOnly(Days))
            {
                MessageBox.Show("Number of days field must contain only digits!", "Input error: Number of days", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckMaxGuestsLimit(int numOfGuests)
        {
            if (numOfGuests > Accommodation.MaxGuests)
            {
                MessageBox.Show($"Maximum number of guests for this accommodation is {Accommodation.MaxGuests}!", "Exceeded number of guests", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckMinReservationLimit(int numOfDays)
        {
            if (numOfDays < Accommodation.MinReservationDays)
            {
                MessageBox.Show($"Minimal reservation for this accommodation is {Accommodation.MinReservationDays} days!", "Minimal reservation limit", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool IsEndBeforeStart()
        {
            if (StartDate.Date > EndDate.Date)
            {
                MessageBox.Show("Start date cannot be before end date!", "Date not valid", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }

            return false;
        }

        private bool IsEndDateValid(double numOfDays)
        {
            if (StartDate.Date.AddDays(numOfDays) > EndDate.Date)
            {
                MessageBox.Show("Chosen start and end date does not match entered numer of days!", "Date not valid", MessageBoxButton.OK, MessageBoxImage.Error);
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

            int guests = Convert.ToInt32(Guests);

            if (!CheckMaxGuestsLimit(guests)) return false;

            double days = Convert.ToDouble(Days);

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
            int guests = Convert.ToInt32(Guests);

            foreach (var date in dates)
            {


                if (date.AddDays(numOfDays) > endDate)
                {
                    break;
                }

                AccommodationReservation reservation = new(0, date, date.AddDays(numOfDays), User.Id, Accommodation.Id, guests);

                FreeReservations.Add(reservation);

            }
        }

    }
}
