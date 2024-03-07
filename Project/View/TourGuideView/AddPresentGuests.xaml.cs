using Project.Model;
using Project.Repository;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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


    public partial class AddPresentGuests : Window
    {

        public ObservableCollection<User> Reservations { get; set; }

        public UserRepository userRepository;

        private readonly PresentGuestsService presentGuestsService;
        private readonly TourReservationService tourReservationService;


        public int tourId { get; set; }
        public int tourPointId { get; set; }
        public int appointmentId { get; set; }
        public User SelectedUser { get; set; }

        public List<PresentGuests> presentGuests { get; set; }

        public AddPresentGuests(int id , int tourpointid, int appointmentid, PresentGuestsService pService)
        {
            InitializeComponent();
            DataContext = this;

            tourId = id;
            tourPointId = tourpointid;
            appointmentId = appointmentid;

            userRepository = new UserRepository();

            presentGuestsService = pService;
            tourReservationService = new TourReservationService();
            
            
            presentGuests = new List<PresentGuests>();

            presentGuests = presentGuestsService.GetByAppointmentId(appointmentId);

            Reservations = new ObservableCollection<User>(presentGuestsService.GetNotPresentGuests(appointmentid));
        }

        private void Dismiss_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSelectedGuest_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedUser != null)
            {
                PresentGuests presentGuest = new PresentGuests();
                presentGuest.GuestId = SelectedUser.Id;
                presentGuest.TourId = tourId;
                presentGuest.AppointmentId = appointmentId;
                presentGuest.TourPointId = tourPointId;

                presentGuestsService.Create(presentGuest);

                Close();
            }
        }
    }
}
