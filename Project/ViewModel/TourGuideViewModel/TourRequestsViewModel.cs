using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class TourRequestsViewModel:CloseableViewModel, IObserver
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
				if(City != string.Empty)
				{
					City = string.Empty;
				}
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

		private DateTime _endaDate;
		public DateTime EndDate
		{
			get
			{
				return _endaDate;
			}
			set
			{
				_endaDate = value;
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

		private ObservableCollection<TourRequest> _requests;
		public ObservableCollection<TourRequest> Requests
		{
			get
			{
				return _requests;
			}
			set
			{
				_requests = value;
				OnPropertyChanged(nameof(Requests));
			}
		}

		private ObservableCollection<ComplexTour> _complexTours;
		public ObservableCollection<ComplexTour> ComplexTours
		{
			get
			{
				return _complexTours;
			}
			set
			{
				_complexTours = value;
				OnPropertyChanged(nameof(ComplexTours));
			}
		}

		private TourRequest _selectedRequest;
		public TourRequest SelectedRequest
		{
			get
			{
				return _selectedRequest;
			}
			set
			{
				_selectedRequest = value;
				OnPropertyChanged(nameof(SelectedRequest));
			}
		}

		private ComplexTour _selectedComplexTour;
		public ComplexTour SelectedComplexTour
		{
			get
			{
				return _selectedComplexTour;
			}
			set
			{
				_selectedComplexTour = value;
				OnPropertyChanged(nameof(SelectedComplexTour));
			}
		}

		private bool _isVisible;
		public bool IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				_isVisible = value;
				OnPropertyChanged(nameof(IsVisible));
			}
		}

		private readonly LocationService locationService;
		private readonly TourRequestService tourRequestService;
		private readonly TourService tourService;
		private readonly AppointmentService appointmentService;
		private readonly ComplexTourService complexTourService;

        List<TourRequest> tourRequests = new List<TourRequest>();

		private User Guide;

        public TourRequestsViewModel(User user)
        {
            Guide = user;

            locationService = new LocationService();
			tourRequestService = new TourRequestService();
			complexTourService = new ComplexTourService();
			complexTourService.Subscribe(this);

			tourService = new TourService();
			tourService.Subscribe(this);

			appointmentService = new AppointmentService();
			appointmentService.Subscribe(this);

			Countries = locationService.GetAllCountries();
			Cities = LoadCities();
			Languages = LoadLanguages();

			DatePickerDates = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			IsVisible = false;

			tourRequests = tourRequestService.GetAllNotAcceptedAndNotExpired();
			Requests = new ObservableCollection<TourRequest>(tourRequestService.GetAllNotAcceptedAndNotExpired());


			ComplexTours = new ObservableCollection<ComplexTour>(complexTourService.GetAllOnHold());
			
        }

		private List<TourRequest> ApplyingFilter(List<TourRequest> torReq)
		{
			TourRequestFilter filter = new TourRequestFilter();
			List<TourRequest> req = filter.Filtering(tourRequests, Country, City, Language, GuestNumber, StartDate, EndDate);

			return req;
		}

		private List<string> LoadLanguages()
		{
			List<string> languages = new List<string>();
            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach (string element in language)
            {
				languages.Add(element);
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

        private RelayCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(param => this.Close(), param => this.CanClose());
                }
                return closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            this.OnClosingRequest();
        }

        private RelayCommand clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get
            {
                if (clearFilterCommand == null)
                {
                    clearFilterCommand = new RelayCommand(param => this.ClearFilter(), param => this.CanClearFilter());
                }
                return clearFilterCommand;
            }
        }

		private bool CanClearFilter()
		{
			return true;
		}

		private void ClearFilter()
		{
			Country = string.Empty;
			City = string.Empty;
			Language = string.Empty;
			GuestNumber = 0;
			Requests.Clear();
			StartDate = DateTime.MinValue;
			EndDate = DateTime.MinValue;
            foreach (var el in tourRequests)
            {
                Requests.Add(el);
            }

        }

        private RelayCommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get
            {
                if (applyFilterCommand == null)
                {
                    applyFilterCommand = new RelayCommand(param => this.ApplyFilter(), param => this.CanApplyFilter());
                }
                return applyFilterCommand;
            }
        }

		private bool CanApplyFilter()
		{
			return true;
		}

		private void ApplyFilter()
		{
			Requests.Clear();
            foreach (var el in tourRequests)
            {
                Requests.Add(el);
            }

            List<TourRequest> tr = new List<TourRequest>();
			foreach(var element in Requests)
			{
				tr.Add(element);
			}
			Requests.Clear();
			foreach(var el in ApplyingFilter(tr))
			{
				Requests.Add(el);
			}
		}

        private RelayCommand acceptCommand;
        public ICommand AcceptCommand
        {
            get
            {
                if (acceptCommand == null)
                {
                    acceptCommand = new RelayCommand(param => this.Accept(), param => this.CanAccept());
                }
                return acceptCommand;
            }
        }

        private bool CanAccept()
        {
            return SelectedRequest!=null;
        }

        private void Accept()
        {
			RequestDatePicker datePicker = new RequestDatePicker(SelectedRequest, Guide, tourService, appointmentService);
			datePicker.Show();
			SelectedRequest = null;
        }

        private RelayCommand openStatisticCommand;
        public ICommand OpenStatisticCommand
        {
            get
            {
                if (openStatisticCommand == null)
                {
                    openStatisticCommand = new RelayCommand(param => this.OpenStatistic(), param => this.CanOpenStatistic());
                }
                return openStatisticCommand;
            }
        }

        private bool CanOpenStatistic()
        {
            return true;
        }

        private void OpenStatistic()
        {
			RequestStatistic requestStatistic = new RequestStatistic();
			requestStatistic.Show();
        }

        private RelayCommand viewDetailsCommand;
        public ICommand ViewDetailsCommand
        {
            get
            {
                if (viewDetailsCommand == null)
                {
                    viewDetailsCommand = new RelayCommand(param => this.ViewDetails(), param => this.CanViewDetails());
                }
                return viewDetailsCommand;
            }
        }

        private bool CanViewDetails()
        {
			return SelectedComplexTour != null ;
        }

        private void ViewDetails()
        {
			ComplexTourDetails complexTourDetails = new ComplexTourDetails(SelectedComplexTour,Guide,complexTourService);
			complexTourDetails.Show();
        }

        public void Update()
		{
			UpdateRequests();
			UpdateComplex();
		}

        public void UpdateRequests()
        {
            Requests.Clear();

            foreach (TourRequest request in tourRequestService.GetAllNotAcceptedAndNotExpired())
            {
                Requests.Add(request);
            }
        }

		public void UpdateComplex()
		{
			ComplexTours.Clear();
			foreach (ComplexTour complexTour in complexTourService.GetAllOnHold())
			{
				ComplexTours.Add(complexTour);
			}
		}
	}
}
