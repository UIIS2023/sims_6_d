using Project.Controller;
using Project.Model;
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
    /// Interaction logic for OwnerMainView.xaml
    /// </summary>
    public partial class OwnerMainView : Window
    {
        private User user;

        private OwnerController controller;

        private List<AccommodationImage> tempImages;

        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<string> Countries { get; set; }

        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }

        public string SelectedCity { get; set; }
        public OwnerMainView(User u)
        {
            InitializeComponent();
            DataContext = this;

            controller = new OwnerController(u);

            Accommodations = new ObservableCollection<Accommodation>(controller.Owner.Accommodations);

            Countries = new ObservableCollection<string>();

            tempImages = new List<AccommodationImage>();

            CountryCities = new ObservableCollection<string>();

            user = u;

            FillCountriesList();
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void FillCountriesList()
        {
            foreach (var location in controller.GetLocations())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void tbMaximumGuests_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void tbAdvanceReservation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void tbCancellationPeriod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryCities.Clear();
            foreach (var location in controller.GetLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

        private void btRate_Click(object sender, RoutedEventArgs e)
        {
            if (!(SelectedAccommodation == null))
            {
                RateGuestView rateGuestView = new RateGuestView(SelectedAccommodation, user);
                try
                {
                    rateGuestView.ShowDialog();
                }
                catch
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select any accommodation.");
                return;
            }

        }



        private void btAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddLink.Text))
            {
                MessageBox.Show("Link is not specified");
                return;
            }
            try
            {
                AccommodationImage image = new AccommodationImage(0, tbAddLink.Text, controller.AccommodationRepository.GetLastId());
                tempImages.Add(image);
                MessageBox.Show("Image added successfully!");
            }
            catch
            {
                MessageBox.Show("Error occurred while adding image");
                return;
            }

        }

        AccommodationType GetAccommodationType()
        {
            AccommodationType type = AccommodationType.COTTAGE;
            if (rbApartment.IsChecked == true)
            {
                type = AccommodationType.APPARTMENT;
            }
            else if (rbHouse.IsChecked == true)
            {
                type = AccommodationType.HOUSE;
            }
            return type;
        }

        private void btAddAccommodation_click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(tbMaximumGuests.Text) || string.IsNullOrWhiteSpace(tbCancellationPeriod.Text) || string.IsNullOrWhiteSpace(tbAdvanceReservation.Text))
            {
                MessageBox.Show("Not all fields are entered.");
                return;
            }

            int guestNumber = Convert.ToInt32(tbMaximumGuests.Text);
            int cancellationPeriod = Convert.ToInt32(tbCancellationPeriod.Text);
            int advanceReservation = Convert.ToInt32(tbAdvanceReservation.Text);
            AccommodationType type = GetAccommodationType();

            if (SelectedCity == null || SelectedCountry == null)
            {
                MessageBox.Show("Please select location!");
                return;
            }
            Location location = new Location(SelectedCity, SelectedCountry);
            Accommodation accommodation = new Accommodation(name, controller.Owner.User.Id, type, location, guestNumber, advanceReservation, cancellationPeriod);
            foreach (var image in tempImages)
            {
                controller.AccommodationImageRepository.Add(image);
                accommodation.Images.Add(image);
            }

            accommodation = controller.AccommodationRepository.Add(accommodation);
            Accommodations.Add(accommodation);
            MessageBox.Show("You've successfully added accommodation to your account.");
            tempImages.Clear();
        }

    }
}
