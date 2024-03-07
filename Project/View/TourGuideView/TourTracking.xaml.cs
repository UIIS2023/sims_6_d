using Microsoft.VisualBasic;
using Project.Controller;
using Project.Model;
using Project.Observer;
using Project.Repository;
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

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window, IObserver, INotifyPropertyChanged
    {

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;
        private readonly PresentGuestsService _presentGuestsService;
        private readonly TourPointsListService _tourPointsListService;
        private readonly TourPointService _tourPointService;



        public int tourId { get; set; }
        public int appointmentId { get; set; }
        public DateTime date { get; set; }

        public ObservableCollection<TourPoint> tourPoints { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<User> Presents { get; set; }

        int order;
        private int numberOfNextClicks = 0;

        public TourTracking(int sendedId, int appointmentid)
        {
            InitializeComponent();
            DataContext = this;

            _tourPointService = new TourPointService();
            _tourPointService.Subscribe(this);

            _tourPointsListService = new TourPointsListService();

            _tourService = new TourService();

            _appointmentService = new AppointmentService();
            _presentGuestsService = new PresentGuestsService();
            _presentGuestsService.Subscribe(this);



            tourId = sendedId;
            appointmentId = appointmentid;

            Presents = new ObservableCollection<User>(_presentGuestsService.GetUserByAppointmentId(appointmentId));

            tourPoints = new ObservableCollection<TourPoint>(_tourPointsListService.GetPointsByTourId(tourId));

            order = 0;

            CreateRadioButtons();

            
        }


        public void CreateRadioButtons()
        {
            for (int i = 0; i < tourPoints.Count; i++)
            {
                RadioButton RadBtn = new RadioButton();

                RadBtn.Name = "rad" + i;
                RadBtn.Content = tourPoints[i].PointName;
                RadBtn.IsEnabled = false;
                RadBtn.Tag = i;
                RadBtn.FontSize = 14;
                
                

                if (i == 0)
                {
                    RadBtn.IsChecked = true;
                }

                RadioStackPanel.Children.Add(RadBtn);
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tourName.Text = _tourService.GetById(tourId).Name;
            ChangeActivity(tourPoints[order]);
            
        }


        private void endTour_Click(object sender, RoutedEventArgs e)
        {
            ChangeActivity(tourPoints[order]);
            EndTheAppointment();
        }

        private void EndTheAppointment()
        {
            _appointmentService.CompleteTour(appointmentId);
            MessageBox.Show("The tour is over");
            Close();
            this.Owner.Close();
        }

        private void nextPoint_Click(object sender, RoutedEventArgs e)
        {
            ChangeActivity(tourPoints[order]);

            numberOfNextClicks++;

            int lastIndex = (tourPoints.Count - 1);

            if(numberOfNextClicks > lastIndex)
            {
                EndTheAppointment();
            }

            else
            {
                order++;
                foreach (RadioButton element in RadioStackPanel.Children)
                {
                    if ((int)(element).Tag == order)
                    {
                        element.IsChecked = true;
                    }
                }
                ChangeActivity(tourPoints[order]);
            }

            

        }

        public void ChangeActivity(TourPoint point)
        {
            TourPoint tourPoint = _tourPointService.GetById(point.Id);

            if(tourPoint.Action == true)
            {
                _tourPointService.UpdateAction(point.Id, false);
            }
            else
            {
                _tourPointService.UpdateAction(point.Id, true);
            }
        }
        
        public void Update()
        {
            UpdatePoints();
            UpdatePresents();
        }

        public void UpdatePoints()
        {
            tourPoints.Clear();

            foreach (var point in _tourPointsListService.GetPointsByTourId(tourId))
            {
                tourPoints.Add(point);
            }
        }

        public void UpdatePresents()
        {

            Presents.Clear();

            foreach (var present in _presentGuestsService.GetUserByAppointmentId(appointmentId))
            {
                Presents.Add(present);
            }
        }

        private void AddGuests_Click(object sender, RoutedEventArgs e)
        {
            AddPresentGuests addPresentGuests = new AddPresentGuests(tourId, tourPoints[order].Id, appointmentId, _presentGuestsService);
            addPresentGuests.Owner = this;
            addPresentGuests.Show();
        }
    }
}
