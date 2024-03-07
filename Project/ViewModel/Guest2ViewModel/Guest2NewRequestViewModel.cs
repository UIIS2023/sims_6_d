using Microsoft.VisualBasic.ApplicationServices;
using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    class Guest2NewRequestViewModel : CloseableViewModel, IObserver
    {
        private string[] _countries;
        public string[] Countries
        {
            get
            {
                return _countries;
            }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        private string[] _cities;
        public string[] Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private string _country = string.Empty;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
                Cities = LoadCities();
            }
        }

        private string _city = string.Empty;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private List<string> _languages;
        public List<string> Languages
        {
            get
            {
                return _languages;
            }
            set
            {
                _languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        private string _language = string.Empty;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
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
                OnPropertyChanged(nameof(StartDate));
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
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private DateTime _datePickerDates;
        public DateTime DatePickerDates
        {
            get
            {
                return _datePickerDates;
            }
            set
            {
                _datePickerDates = value;
                OnPropertyChanged(nameof(DatePickerDates));
            }
        }

        private int _guestNumber;
        public int GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                _guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private Location _location = new Location();
        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private readonly LocationService locationService;
        private readonly TourRequestService tourRequestService;
        private readonly AppointmentService appointmentService;

        //private User Guest;

        public Guest2NewRequestViewModel()
        {
            //Guest = user;
            locationService = new LocationService();
            tourRequestService = new TourRequestService();
            appointmentService = new AppointmentService();
            appointmentService.Subscribe(this);
            Countries = locationService.GetAllCountries();
            Cities = LoadCities();
            Languages = LoadLanguages();

            DatePickerDates = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        }

        private List<string> LoadLanguages()
        {
            List<string> languages = new List<string>();
            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach(string lang in language)
            {
                languages.Add(lang);
            }
            return languages;
        }

        private string[] LoadCities()
        {
            if(Country == string.Empty)
            {
                return locationService.GetAllCities();
            }
            else
            {
                return locationService.GetAppropriateCities(Country);
            }
        }

        private RelayCommand makeNewRequestCommand;
        public ICommand MakeNewRequestCommand
        {
            get
            {
                if(makeNewRequestCommand == null)
                {
                    makeNewRequestCommand = new RelayCommand(param => this.MakeNewRequest(), param => this.CanMakeNewRequest());
                }
                return makeNewRequestCommand;
            }
            
        }

        private void MakeNewRequest()
        {
            _location = locationService.Create(City, Country);

            //TourRequest request = new TourRequest(_location.Id, Description, Language, GuestNumber, StartDate, EndDate, 4);
            tourRequestService.Create(_location.Id, Description,Language,GuestNumber,StartDate,EndDate,new DateTime(),TourRequest.STATUS.ONHOLD, 4, TourRequest.TYPE.REGULAR);
        }

        private bool CanMakeNewRequest()
        {
            return true;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
