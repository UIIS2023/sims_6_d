using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
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

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for AccommodationInfoWindow.xaml
    /// </summary>
    public partial class AccommodationInfoWindow : Window
    {
        public Accommodation ChosenAccommodation { get; set; }
        private User user;

        public List<AccommodationImage> Images { get; set; }

        int i = 0;

        public AccommodationInfoWindow(Accommodation accommodation, User u)
        {
            InitializeComponent();
            DataContext = this;
            user = u;
            ChosenAccommodation = accommodation;
            Images = new List<AccommodationImage>(ChosenAccommodation.GetAccommodationImages());
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            i--;

            if (i < 0)
            {
                i = Images.Count - 1;
            }

            picHolder.Source = new BitmapImage(new Uri(Images[i].Url, UriKind.RelativeOrAbsolute));
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            i++;

            if (i > Images.Count - 1)
            {
                i = 0;
            }

            picHolder.Source = new BitmapImage(new Uri(Images[i].Url, UriKind.RelativeOrAbsolute));
        }

        private void btMakeReserv_Click(object sender, RoutedEventArgs e)
        {
            ReserveAccommodationWindow reserveWindow = new ReserveAccommodationWindow(ChosenAccommodation, user);
            reserveWindow.Show();
        }
    }
}
