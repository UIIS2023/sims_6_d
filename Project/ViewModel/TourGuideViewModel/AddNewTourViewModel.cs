using Microsoft.Win32;
using Project.Controller;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Project.Command;
using Project.View.TourGuideView;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Linq;

namespace Project.ViewModel.TourGuideViewModel
{
    public class AddNewTourViewModel:CloseableViewModel, IDataErrorInfo
    {

        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

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


        private string _nameOfTour = string.Empty;
        public string NameOfTour
        {
            get
            {
                return _nameOfTour;
            }
            set
            {
                _nameOfTour = value;
                OnPropertyChanged(nameof(NameOfTour));
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

        private int _maxGuests;
        public int MaxGuests
        {
            get
            {
                return _maxGuests;
            }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        private DateTime _startDate = DateTime.Today;
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

        private DateTime _datePickerStartDate;
        public DateTime DatePickerStartDate
        {
            get
            {
                return _datePickerStartDate;
            }
            set
            {
                _datePickerStartDate = value;
                OnPropertyChanged(nameof(DatePickerStartDate));
            }
        }

        private string _startTime = string.Empty;
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private int _duration;
        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private string _coverImageUrl;
        public string CoverImageUrl
        {
            get
            {
                return _coverImageUrl;
            }
            set
            {
                _coverImageUrl = value;
                OnPropertyChanged(nameof(CoverImageUrl));
            }
        }

        private Location _location = new Location();
        public Location LocationOfTour
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(LocationOfTour));
            }
        }

        private string _startPoint = string.Empty;
        public string StartPoint
        {
            get
            {
                return _startPoint;
            }
            set
            {
                _startPoint = value;
                OnPropertyChanged(nameof(StartPoint));
            }
        }

        private string _endPoint = string.Empty;
        public string EndPoint
        {
            get
            {
                return _endPoint;
            }
            set
            {
                _endPoint = value;
                OnPropertyChanged(nameof(EndPoint));
            }
        }

        private string _anotherPoint = string.Empty;
        public string AnotherPoint
        {
            get
            {
                return _anotherPoint;
            }
            set
            {
                _anotherPoint = value;
                OnPropertyChanged(nameof(AnotherPoint));
            }
        }

        private ObservableCollection<string> _anotherPoints = new ObservableCollection<string>();
        public ObservableCollection<string> AnotherPoints
        {
            get
            {
                return _anotherPoints;
            }
            set
            {
                _anotherPoints = value;
                OnPropertyChanged(nameof(AnotherPoints));
            }
        }

        private ObservableCollection<string> _images = new ObservableCollection<string>();
        public ObservableCollection<string> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }

        private ObservableCollection<DateTime> _dates = new ObservableCollection<DateTime>();
        public ObservableCollection<DateTime> Dates
        {
            get
            {
                return _dates;
            }
            set
            {
                _dates = value;
                OnPropertyChanged(nameof(Dates));
                CheckListEmpty();
            }
        }

        private bool _isListEmpty = true;
        public bool IsListEmpty
        {
            get
            {
                return _isListEmpty;
            }
            set
            {
                _isListEmpty = value;
                OnPropertyChanged(nameof(IsListEmpty));
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

        private string _county = string.Empty;
        public string Country
        {
            get
            {
                return _county;
            }
            set
            {
                _county = value;
                OnPropertyChanged(nameof(Country));
                LoadCities();
            }
        }


        //public AddSharedViewModel SharedViewModel { get; set; }

        private AddSharedViewModel _sharedViewModel;
        public AddSharedViewModel SharedViewModel
        {
            get
            {
                return _sharedViewModel;
            }
            set
            {
                _sharedViewModel = value;
                OnPropertyChanged(nameof(SharedViewModel));
                Country = _sharedViewModel.Country;
                City = _sharedViewModel.City;
            }
        }



        private readonly ImageController _imageController;

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;
        private readonly TourPointService _tourPointService;
        private readonly TourPointsListService _tourPointsListService;
        private readonly LocationService _locationService;
        private readonly TourRequestService _tourRequestService;
        private readonly NotificationService _notificationService;
        private readonly ComplexTourService _complexTourService;

        List<int> pointsIds = new List<int>();

        private User Guide;

        

        public bool IsEnabled { get; set; }
        public int RequestId { get; set; }


        public AddNewTourViewModel(TourService tourService, AppointmentService appointmentService, User user, AddSharedViewModel sharedViewModel)
        {
            _imageController = new ImageController();
            _tourPointsListService = new TourPointsListService();
            _tourPointService = new TourPointService();
            _locationService = new LocationService();
            _tourService = tourService;
            _appointmentService = appointmentService;
            _complexTourService = new ComplexTourService();
            _tourRequestService = new TourRequestService();
            _notificationService = new NotificationService();
            SharedViewModel = sharedViewModel;

            

            Countries = _locationService.GetAllCountries();
            Cities = LoadCities();
            Languages = LoadLanguages();
            Guide = user;
            IsEnabled = true;
            DatePickerStartDate = DateTime.Today;
        }

        public AddNewTourViewModel(AddSharedViewModel sharedViewModel, User user, int requestId, TourService tourService, AppointmentService appointmentService)
        {
            _imageController = new ImageController();
            _tourPointsListService = new TourPointsListService();
            _tourPointService = new TourPointService();
            _locationService = new LocationService();
            _tourService = tourService;
            _appointmentService = appointmentService;
            _complexTourService = new ComplexTourService();
            _tourRequestService = new TourRequestService();
            _notificationService = new NotificationService();


            SharedViewModel = sharedViewModel;

            Countries = _locationService.GetAllCountries();
            Cities = LoadCities();
            Languages = LoadLanguages();
            DatePickerStartDate = DateTime.Today;


            Dates.Add(SharedViewModel.Appointment);
            Guide = user;

            RequestId = requestId;
            IsEnabled = false;
        }

        public AddNewTourViewModel(AddSharedViewModel sharedViewModel, User user, int requestId, TourService tourService, AppointmentService appointmentService, ComplexTourService complexTourService)
        {
            _imageController = new ImageController();
            _tourPointsListService = new TourPointsListService();
            _tourPointService = new TourPointService();
            _locationService = new LocationService();
            _tourService = tourService;
            _appointmentService = appointmentService;
            _complexTourService = complexTourService;
            _tourRequestService = new TourRequestService();
            _notificationService = new NotificationService();


            SharedViewModel = sharedViewModel;

            Countries = _locationService.GetAllCountries();
            Cities = LoadCities();
            Languages = LoadLanguages();
            DatePickerStartDate = DateTime.Today;


            Dates.Add(SharedViewModel.Appointment);
            Guide = user;

            RequestId = requestId;
            IsEnabled = false;
        }



        private string[] LoadCities()
        {
            if (SharedViewModel.Country == string.Empty)
            {
                return _locationService.GetAllCities();
            }
            else
            {
                return _locationService.GetAppropriateCities(SharedViewModel.Country);
            }
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


        public void CheckListEmpty()
        {
            if(Dates.Count == 0)
            {
                IsListEmpty = true;
            }
            else { IsListEmpty = false; }
        }



        /* ------------- COMANDS ------------------- */

        private RelayCommand addDateCommand;
        public ICommand AddDateCommand
        {
            get
            {
                if (addDateCommand == null)
                {
                    addDateCommand = new RelayCommand(param => this.AddDate(), param => this.CanAddDate());
                }
                return addDateCommand;
            }
        }
        private bool CanAddDate()
        {
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d$";
            return Regex.IsMatch(StartTime, pattern) && (StartTime != null) && (StartDate.Date >= DateTime.Today);

        }
        private void AddDate()
        {
            DateTime dateAndTime = _tourService.BuildDate(StartDate, StartTime);

            Dates.Add(dateAndTime);
            CheckListEmpty();
            StartTime = "";
        }

        private RelayCommand addPictureCommand;
        public ICommand AddPictureCommand
        {
            get
            {
                if (addPictureCommand == null)
                {
                    addPictureCommand = new RelayCommand(param => this.AddPicture(), param => this.CanAddPicture());
                }
                return addPictureCommand;
            }
        }
        private bool CanAddPicture()
        {
            return true;
        }

        private void AddPicture()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelHeight = 200;
                bitmap.EndInit();

                Images.Add(openFileDialog.FileName);
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
            SharedViewModel = new AddSharedViewModel();
            this.OnClosingRequest();
        }

        private RelayCommand addPointCommand;
        public ICommand AddPointCommand
        {
            get
            {
                if (addPointCommand == null)
                {
                    addPointCommand = new RelayCommand(param => this.AddPoint(), param => this.CanAddPoint());
                }
                return addPointCommand;
            }
        }

        private void AddPoint()
        {
            if (AnotherPoint != "")
            {
                int anotherTourPointId = _tourPointService.Create(AnotherPoint, false);
                pointsIds.Add(anotherTourPointId);
            }

            AnotherPoints.Add(AnotherPoint);
            AnotherPoint = "";
            
        }

        private bool CanAddPoint()
        {
            return AnotherPoint != string.Empty && StartPoint != string.Empty && EndPoint != string.Empty;
        }


        private RelayCommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(param => this.Submit(), param => this.CanSubmit());
                }
                return submitCommand;
            }
        }

        private void Submit()
        {
            //PRAVLJENJE LOCATION-a

            _location = _locationService.Create(SharedViewModel.City, SharedViewModel.Country);

            if (!IsEnabled)
            {
                _tourRequestService.MarkAsAccepted(RequestId);
            }
            
            //KREIRANJE TOUR-a

            int tourId = _tourService.Create(_location, NameOfTour, Description, SharedViewModel.Language, SharedViewModel.GuestNumber, Duration, Guide.Id);

            //KREIRANJE APPOINTMENT-a

            foreach (DateTime date in Dates)
            {
                _appointmentService.Create(tourId, date);

                if (!IsEnabled)
                {
                    _tourRequestService.AddAcceptedAppointment(RequestId,date,Guide.Id);
                }
            }

            Dates.Clear();

            //KREIRANJE POINTS-a 

            if (StartPoint != "" && EndPoint != "")
            {
                int startPointId = _tourPointService.Create(StartPoint, false);
                int endPointId = _tourPointService.Create(EndPoint, false);
                pointsIds.Insert(0, startPointId);
                pointsIds.Add(endPointId);
            }

            _tourPointsListService.Create(tourId, pointsIds);

            //SKLADISTENJE SLIKA

            foreach (string image in Images)
            {
                _imageController.Create(image, tourId, Model.PictureType.TOUR);
            }
            Images.Clear();

            if (!IsEnabled)
            {
                string message = $"Guide {Guide.Username} has created a tour according to your request " +
                    $"({_tourRequestService.GetById(RequestId).Location.Country},{_tourRequestService.GetById(RequestId).Location.City},{_tourRequestService.GetById(RequestId).Language})" +
                    $" and scheduled it to start on {_tourRequestService.GetById(RequestId).AcceptedAppointment}. Name of tour is {NameOfTour}.";
                _notificationService.Create(_tourRequestService.GetById(RequestId).GuestId,message);
                MessageBox.Show("The tour was successfully created against the tour request");
                _complexTourService.CheckStatus();
            }

            Close();

        }

        private bool CanSubmit()
        {
            return NameOfTour!=string.Empty && SharedViewModel.Country!=string.Empty && SharedViewModel.City!=string.Empty && Dates.Count!=0 && Duration!=0 
                && SharedViewModel.Language!=string.Empty && SharedViewModel.GuestNumber!=0 && StartPoint!="" && EndPoint!="" && Description!=string.Empty;
        }


        private RelayCommand createSuggestionCommand;
        public ICommand CreateSuggestionCommand
        {
            get
            {
                if (createSuggestionCommand == null)
                {
                    createSuggestionCommand = new RelayCommand(param => this.CreateSuggestion(), param => this.CanCreateSuggestion());
                }
                return createSuggestionCommand;
            }

        }



        private bool CanCreateSuggestion()
        {
            return IsEnabled;
        }

        private void CreateSuggestion()
        {
            Suggestion suggestion = new Suggestion(SharedViewModel);
            suggestion.Show();

        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "NameOfTour":
                        if (string.IsNullOrWhiteSpace(NameOfTour))
                            result = "Name cannot be empty";
                        break;
                    case "SharedViewModel.GuestNumber":
                        if (SharedViewModel.GuestNumber == 0)
                            result = "Enter the number of guests";
                        else if(string.IsNullOrWhiteSpace(SharedViewModel.GuestNumber.ToString()))
                            result = "Enter the number of guests";
                        break;

                    case "Dates":
                            if (IsListEmpty)
                            result = "You must add at least one appointment to list";
                        break;

                    case "StartPoint":
                        if (string.IsNullOrWhiteSpace(StartPoint))
                            result = "Start point cannot be empty";
                        break;

                    case "EndPoint":
                        if (string.IsNullOrWhiteSpace(EndPoint))
                            result = "End point cannot be empty";
                        break;

                    case "Description":
                        if (string.IsNullOrWhiteSpace(Description))
                            result = "You must add a description";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged(nameof(ErrorCollection));

                return result;
            }
        }

        




    }
}
