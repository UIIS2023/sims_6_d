using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    class Guest2RequestsStatisticViewModel : ViewModelBase
    {
        private TourRequestFilter filter = new TourRequestFilter();

        

        private List<TourRequest> _appropriateRequests = new List<TourRequest>();
        public List<TourRequest> AppropriateRequests
        {
            get
            {
                return _appropriateRequests;
            }
            set
            {
                _appropriateRequests = value;
                OnPropertyChanged(nameof(AppropriateRequests));
            }
        }

        private ProgressBar _progressBarCountry;
        public ProgressBar ProgressBarCountry
        {
            get
            {
                return _progressBarCountry;
            }
            set
            {
                _progressBarCountry = value;
                OnPropertyChanged(nameof(ProgressBarCountry));
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
                //AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
            }
        }

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
                //AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
                if(City != string.Empty)
                {
                    City = string.Empty;
                }
                Cities = LoadCities();
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
                //AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);
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

        private string _year = string.Empty;
        public string Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                SelectedYearChanged();
            }
        }

        private List<string> _years = new List<string>();
        public List<string> Years
        {
            get
            {
                return _years;
            }
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private List<string> _statuses = new List<string>();
        public List<string> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                _statuses = value;
                OnPropertyChanged(nameof(Statuses));
            }
        }

        private readonly TourRequestService _tourRequestService;
        private readonly LocationService locationService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        //public ObservableCollection<string> Countriess { get; set; }
        public int SelectedCountry { get; set; }

        public Guest2RequestsStatisticViewModel()
        {
            _tourRequestService = new TourRequestService();
            locationService = new LocationService();
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAllGuestsTourRequests(4));
            //Countriess = new ObservableCollection<string>(TourRequests.Select(t => t.Location.Country).Distinct());
            //TourRequests = _tourRequestService.GetAll();
            //AppropriateRequests = filter.StatisticFiltering(TourRequests, Country, City, Language, Year);

            Years = GetYears();
            Languages = LoadLanguages();
            Countries = locationService.GetAllCountries();
            Cities = LoadCities();
            Statuses = GetStatuses();
        }

        

        

        private List<string> GetYears()
        {
            List<string> years = new List<string>();
            for(int i = 2010; i < 2024; i++)
            {
                years.Add(i.ToString());
            }
            return years;
        }

        private List<string> GetStatuses()
        {
            List<string> statuses = new List<string>();
            statuses.Add("EXPIRED");
            statuses.Add("ACCEPTED");
            return statuses;
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

        private RelayCommand clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get
            {
                if(clearFilterCommand == null)
                {
                    clearFilterCommand = new RelayCommand(param => this.ClearFilter(), param => this.CanClearFilter());
                }
                return clearFilterCommand;
            }
        }

        private void ClearFilter()
        {
            Country = string.Empty;
            City = string.Empty;
            Language = string.Empty;
            Year = string.Empty;
            Status = string.Empty;
        }

        private bool CanClearFilter()
        {
            return true;
        }

        public void SelectedYearChanged()
        {

        }

        

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
