using Project.Command.OwnerCommands;
using Project.Command.OwnerCommands.AddAccommodationCommands;
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

namespace Project.ViewModel.OwnerViewModel
{
    public class AddAccommodationViewModel : ViewModelBase
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
                if(value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(_country));
                    SelectedCountryChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if(value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(_city));
                }
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(_type));
            }
        }

        private string _capacity;
        public string Capacity
        {
            get { return _capacity; }
            set
            {
                if (value == "0") return;
                if (!IsDigitsOnly(value)) return;
                _capacity = value;
                OnPropertyChanged(nameof(_capacity));
            }
        }

        private string _bookingLeadTime;
        public string BookingLeadTime
        {
            get { return _bookingLeadTime; }
            set
            {
                if (value == "0") return;
                if (!IsDigitsOnly(value)) return;
                _bookingLeadTime = value;
                OnPropertyChanged(nameof(_bookingLeadTime));
            }
        }

        private string _cancellationPeriod;
        public string CancellationPeriod
        {
            get { return _cancellationPeriod; }
            set
            {
                if (value == "0") return;
                if (!IsDigitsOnly(value)) return;
                _cancellationPeriod = value;
                OnPropertyChanged(nameof(_cancellationPeriod));
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }


        private readonly AccommodationService _accommodationService;

        private readonly ILocationRepository _locationRepository;

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }
        public ObservableCollection<string> ImageUrls { get; set; }

        public ObservableCollection<string> Types { get; set; }

        public ICommand AddImageCommand { get; }
        public ICommand BurgerMenuCommand { get; }
        public ICommand AddAccommodationCommand { get; }

        public AddAccommodationViewModel(User user, Window window)
        {
            User = user;
            Window = window;

            _accommodationService = new();
            _locationRepository = Injector.Injector.CreateInstance<ILocationRepository>();

            CancellationPeriod = "1";
            ImageUrls = new ObservableCollection<string>();
            Countries = new(_locationRepository.GetAllCountries());
            CountryCities = new();
            Types = new();
            Types.Add("House");
            Types.Add("Cottage");
            Types.Add("Appartment");

            AddImageCommand = new AddImageCommand(this);
            BurgerMenuCommand = new BurgerMenuCommand(this);
            AddAccommodationCommand = new AddAccommodationCommand(this, _accommodationService);
        }
        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private void SelectedCountryChanged()
        {
            CountryCities.Clear();
            foreach (var city in _locationRepository.GetAppropriateCities(Country))
            {
                CountryCities.Add(city);
            }
        }



    }
}
