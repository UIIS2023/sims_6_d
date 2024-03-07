using Project.Command.Guest1Commands.SearchAccommodationsCommands;
using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.Model;
using Project.RepositoryInterfaces;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Project.ViewModel.Guest1ViewModel
{
    public class SearchAccommodationsViewModel :ViewModelBase
    {
		private string _name;
		public string Name
		{
			get 
			{ 
				return _name;
			}
			set 
			{ 
				_name = value;
				OnPropertyChanged(nameof(_name));
			}
		}

        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(_country));
                SelectedCountryChanged();
            }
        }

		private string _city;

		public string City
		{
			get { return _city; }
			set 
			{ 
				_city = value;
				OnPropertyChanged(nameof(_city));
			}
		}

        private string _guests;
        public string Guests
        {
            get
            {
                return _guests;
            }
            set
            {
                if(value == "0") { return; }
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

        private bool _isHouse;
        public bool IsHouse
        {
            get
            {
                return _isHouse;
            }
            set
            {
                _isHouse = value;
                OnPropertyChanged(nameof(_isHouse));
            }
        }

        private bool _isCottage;
        public bool IsCottage
        {
            get
            {
                return _isCottage;
            }
            set
            {
                _isCottage = value;
                OnPropertyChanged(nameof(_isCottage));
            }
        }

        private bool _isAppartment;
        public bool IsAppartment
        {
            get
            {
                return _isAppartment;
            }
            set
            {
                _isAppartment = value;
                OnPropertyChanged(nameof(_isAppartment));
            }
        }

        private readonly AccommodationService _accommodationService;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        public ICommand ProfileLinkCommand { get; }
        public ICommand YourReservationsLinkCommand { get; }
        public ICommand MoveReservationLinkCommand { get; }
        public ICommand SearchAccommodationsLinkCommand { get; }
        public ICommand SearchAccommodationsCommand { get; }
        public ICommand MakeReservationCommand { get; }


        public SearchAccommodationsViewModel(User user, Window window)
        {
            User = user;
            Window = window;

            ProfileLinkCommand = new ProfileLinkCommand(this);
            YourReservationsLinkCommand = new YourReservationsLinkCommand(this);
            MoveReservationLinkCommand = new MoveReservationLinkCommand(this);
            SearchAccommodationsLinkCommand = new SearchAccommodationsLinkCommand(this);
            SearchAccommodationsCommand = new SearchAccommodationCommand(this);
            MakeReservationCommand = new MakeReservationCommand(this);

            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());
            Countries = new ObservableCollection<string>(_accommodationService.GetAllAccommodationCountries());
            CountryCities = new ObservableCollection<string>();
            

        }

        public void FilterAccommodations()
        {

            ReInitializeAccommodations();

            if (!IsFieldEmpty(Name))
            {
                FilterAccommodationsByName();
            }

            if (!IsFieldEmpty(Country))
            {
                FilterAccommodationsByLocation();
            }


            if (!IsFieldEmpty(Guests))
            {
                if (!IsDigitsOnly(Guests.ToString()))
                {
                    InputErrorMessageBox("Number of guests");
                    return;
                }

                FilterAccommodationsByGuests();
            }

            if (!IsFieldEmpty(Days))
            {
                if (!IsDigitsOnly(Days.ToString()))
                {
                    InputErrorMessageBox("Number of days");
                    return;
                }

                FilterAccommodationsByDays();
            }

            if (!IsAppartment && !IsCottage && !IsHouse)
            {
                return;
            }

            FilterAccommodationsByType();

        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private bool IsFieldEmpty(string fieldInput)
        {
            return string.IsNullOrWhiteSpace(fieldInput);
        }

        private void InputErrorMessageBox(string fieldName)
        {
            string sMessageBoxText = $"{fieldName} field must contain only digits!";
            string sCaption = $"Input error: {fieldName}";
            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Error;

            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private void FilterAccommodationsByName()
        {

            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (!accommodation.Name.Contains(Name))
                {
                    Accommodations.Remove(accommodation);
                }
            }

        }

        private void FilterAccommodationsByLocation()
        {
            bool isCityChosen = false;
            if (!IsFieldEmpty(City))
            {
                isCityChosen = true;
            }

            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (accommodation.Location.Country != Country)
                {
                    Accommodations.Remove(accommodation);
                }
                else if (isCityChosen && accommodation.Location.City != City)
                {
                    Accommodations.Remove(accommodation);
                }
                else
                    continue;

            }
        }

        private void ReInitializeAccommodations()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in _accommodationService.GetAllAccommodations())
            {
                Accommodations.Add(accommodation);
            }
        }

        private void FilterAccommodationsByGuests()
        {
            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            int guests = Convert.ToInt32(Guests);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (guests > accommodation.MaxGuests)
                {
                    Accommodations.Remove(accommodation);
                }
            }
        }

        private void FilterAccommodationsByDays()
        {
            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            int days = Convert.ToInt32(Days);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (days < accommodation.MinReservationDays)
                {
                    Accommodations.Remove(accommodation);
                }
            }
        }

        private void FilterAccommodationsByType()
        {
            List<Accommodation> temp = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in temp)
            {
                if (accommodation.AccommodationType == AccommodationType.HOUSE)
                {
                    if (!IsHouse)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
                else if (accommodation.AccommodationType == AccommodationType.APPARTMENT)
                {
                    if (!IsAppartment)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
                else
                {
                    if (!IsCottage)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
            }
        }

        public void SelectedCountryChanged()
        {
            CountryCities.Clear();
            foreach(var city in _accommodationService.GetAllAccommodationCitiesByCountry(Country))
            {
                CountryCities.Add(city);
            }
        }


    }
}
