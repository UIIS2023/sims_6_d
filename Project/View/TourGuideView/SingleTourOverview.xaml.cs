using Project.Controller;
using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Image = System.Windows.Controls.Image;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for SingleTourOverview.xaml
    /// </summary>
    public partial class SingleTourOverview : Window,INotifyPropertyChanged
    {
        private string _name = string.Empty;
        public string NameOfTour
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();

                }
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country = string.Empty;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language= string.Empty;
        public string LanguageOfTour
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private Appointment _tourappointment;
        public Appointment TourAppointment
        {
            get => _tourappointment;
            set
            {
                if(value != _tourappointment)
                {
                    _tourappointment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _coverImageUrl = string.Empty;


        public string CoverImageUrl
        {
            get => _coverImageUrl;
            set
            {
                if (value != _coverImageUrl)
                {
                    _coverImageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly ImageController _imageController;

        private readonly TourReservationService tourReservationService;


        Tour Tour { get; set; }
        public Appointment SelectedAppointment { get; set; }
        

        public ObservableCollection<User> Reservations { get; set; }

        public SingleTourOverview(Tour sendedTour)
        {
            InitializeComponent();
            DataContext = this;

            Tour = new Tour();

            Tour = sendedTour;
            _imageController = new ImageController();
            tourReservationService = new TourReservationService(); 


            Id = Tour.Id;
            NameOfTour = Tour.Name;
            City = Tour.Location.City;
            Country = Tour.Location.Country;
            Description = Tour.Description;
            LanguageOfTour = Tour.Language;
            MaxGuests = Tour.MaxGuests;
            Duration = Tour.Duration;
            TourAppointment = Tour.TourAppointment;
            Reservations = new ObservableCollection<User>(tourReservationService.GetApproprietReservations(TourAppointment.Id));

            DisableStartTourButton();
        }


        public void DisableStartTourButton()
        {
            if (Tour.TourAppointment.DateAndTimeOfAppointment.ToShortDateString() != DateTime.Today.ToShortDateString() || Tour.TourAppointment.Status == Appointment.STATUS.COMPLETED)
            {
                startTour.IsEnabled = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Image CreateImage(string imageUrl)
        {

                Image image = new Image();
                image.Width = 100;
                image.Height = 100;
                image.Source = new BitmapImage(new Uri(imageUrl));
                return image;
         
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (string url in _imageController.GetImageUrlByTourId(Id))
            {
                var image = CreateImage(url);
                imagesWrap.Children.Add(image);
            }

            
        }



        private void startTour_Click(object sender, RoutedEventArgs e)
        {
            TourTracking tourTracking = new TourTracking(Id, Tour.TourAppointment.Id);
            tourTracking.Owner = this;
            tourTracking.Show();

        }
    }
}
