using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    class Guest2TourRequestsViewModel : CloseableViewModel, IObserver
    {
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

        private readonly LocationService locationService;
        private readonly TourRequestService tourRequestService;
        private readonly TourService tourService;
        private readonly AppointmentService appointmentService;

        List<TourRequest> tourRequests = new List<TourRequest>();

        private User Guest;

        public Guest2TourRequestsViewModel(User user)
        {
            Guest = user;

            locationService = new LocationService();
            tourRequestService = new TourRequestService();
            tourService = new TourService();
            tourService.Subscribe(this);
            appointmentService = new AppointmentService();
            appointmentService.Subscribe(this);
            Countries = locationService.GetAllCountries();
            Cities = LoadCities();

            tourRequests = tourRequestService.GetAllGuestsTourRequests(3);
            Requests = new ObservableCollection<TourRequest>(tourRequestService.GetAllGuestsTourRequests(4));
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

        public void Update()
        {
            throw new NotImplementedException();
        }

        private RelayCommand openNewRequestCommand;

        public ICommand OpenNewRequestCommand
        {
            get
            {
                if(openNewRequestCommand == null)
                {
                    openNewRequestCommand = new RelayCommand(param => this.OpenNewRequest(), param => this.CanOpenNewRequest());
                }
                return openNewRequestCommand;
            }
        }

       private bool CanOpenNewRequest()
        {
            return true;
        }

        public void OpenNewRequest()
        {
            Guest2NewRequest newRequest = new Guest2NewRequest(Guest);
            newRequest.Show();
        }

        private RelayCommand openRequestsStatisticCommand;
        public ICommand OpenRequestsStatisticCommand
        {
            get
            {
                if( openRequestsStatisticCommand == null)
                {
                    openRequestsStatisticCommand = new RelayCommand(param => this.OpenRequestsStatistic(), param => this.CanOpenRequestsStatistic());
                }
                return openRequestsStatisticCommand;
            }
        }

        private bool CanOpenRequestsStatistic()
        {
            return true;
        }

        public void OpenRequestsStatistic()
        {
            Guest2RequestsStatistic requestsStatistic = new Guest2RequestsStatistic(Guest);
            requestsStatistic.Show();
        }
    }
}
