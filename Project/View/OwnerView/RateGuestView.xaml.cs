using Project.Controller;
using Project.Model;
using Project.Repository;
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

namespace Project.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RateGuestView.xaml
    /// </summary>
    public partial class RateGuestView : Window
    {
        private OwnerController controller;

        private readonly UserRepository _repository;

        public String Names { get; set; }

        public User Owner { get; set; }

        public List<AccommodationReservation> Reservations { get; set; }

        public ObservableCollection<User> Guests { get; set; }

        public User ChosenGuest { get; set; }

        public RateGuestView(Accommodation selectedAccommodation, User user)
        {
            InitializeComponent();
            Owner = user;
            DataContext = this;
            Guests = new ObservableCollection<User>();
            _repository = new UserRepository();
            controller = new OwnerController(user);
            Reservations = new List<AccommodationReservation>();
            CreateReservations(selectedAccommodation);
            FillGuests();
            if (Guests.Count == 0)
            {
                MessageBox.Show("There are no guests to rate in this accommodation.");
                this.Close();
            }
        }

        private void FillGuests()
        {
            foreach (var res in Reservations)
            {
                Guests.Add(_repository.GetById(res.GuestId));                //Guests.Add(UserRepository.GetById(res.GuestId));
            }
        }

        private void CreateReservations(Accommodation selectedAccommodation)
        {
            DateTime today = DateTime.Today;
            List<AccommodationReservation> temp = new List<AccommodationReservation>(controller.AccommodationReservationRepository.GetReservationsByAccommodationId(selectedAccommodation.Id));
            int days;
            foreach (var reservation in temp)
            {
                if (today >= reservation.EndDate)
                {
                    TimeSpan difference = today - reservation.EndDate;
                    days = difference.Days;
                    if (days < 5)
                    {
                        this.Reservations.Add(reservation);
                    }
                }

            }

        }

        private void tbCleanliness_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 5;
        }

        private void tbHousePolicies_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {
            int ownerId = Owner.Id;
            int guestId = ChosenGuest.Id;
            int cleanliness = GetCleanliness();
            if (cleanliness == 0) { return; }
            int housePolicies = GetHousePolicies();
            if (housePolicies == 0) { return; }
            string comment = tbComment.Text;
            comment = comment.Replace(System.Environment.NewLine, " ");
            //OwnerReview ownerReview = new OwnerReview(ownerId,guestId,cleanliness,housePolicies,comment);
            //controller.OwnerReviewRepository.AddOrUpdate(ownerReview);
            MessageBox.Show("Comment added successfully");
            this.Close();
        }

        private int GetCleanliness()
        {
            int cleanliness;
            if (string.IsNullOrWhiteSpace(tbCleanliness.Text))
            {
                MessageBox.Show("Cleanliness field is empty");
                return 0;
            }
            cleanliness = Convert.ToInt32(tbCleanliness.Text);
            return cleanliness;
        }
        private int GetHousePolicies()
        {
            int housePolicies;
            if (string.IsNullOrWhiteSpace(tbHousePolicies.Text))
            {
                MessageBox.Show("House Policies field is empty");
                return 0;
            }
            housePolicies = Convert.ToInt32(tbHousePolicies.Text);
            return housePolicies;
        }

    }
}
