using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Project.Repository;
using Project.Model;
using Project.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.View.TourGuideView;
using Project.Controller;
using Project.View.Guest1View;
using Project.Service;
using Project.View.OwnerView;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        private readonly UserRepository _repository;
        private readonly SuperGuestService _superGuestService;
        private readonly SuperGuideService _superGuideService;
        public static Window PreviousWindow { get; set; }
        //private readonly TourGuideController _controller;
        //private readonly TourService _tourService;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInView()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _superGuestService = new SuperGuestService();
            _superGuideService = new SuperGuideService();

            RefreshSuperGuides();
            //_tourService = new TourService();
            //_controller = new TourGuideController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case Role.OWNER:
                            /* OwnerMainView ownerMainView = new OwnerMainView(user);
                             ownerMainView.Show();*/
                            /*MenuView menuView = new MenuView(user);
                            menuView.Show();*/
                            YourAccommodationsView yourAccommodationsView = new YourAccommodationsView(user);
                            yourAccommodationsView.Show();
                            Close();
                            break;

                        case Role.GUIDE:
                            TourGuideMainView tourGuideMainView = new TourGuideMainView(user);
                            tourGuideMainView.Show();
                            Close();
                            break;

                        case Role.GUEST1:
                            _superGuestService.UpdateGuestStatus(user);
                            ProfileWindow profileWindow = new ProfileWindow(user);
                            profileWindow.Show();
                            Close();
                            profileWindow.RemindGuestToRate();
                            profileWindow.ShowNotifications();
                            break;

                        default:
                            Guest2View guest2View = new Guest2View(user);
                            guest2View.Show();
                            Close();
                            break;


                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }

        private void RefreshSuperGuides()
        {
            List<SuperGuide> superGuides = new List<SuperGuide> ();
            foreach(User user in _repository.GetAllGuide())
            {
                    foreach(string language in _superGuideService.SuperLanguages(user.Id))
                    {
                        superGuides.Add(new SuperGuide(user.Id, language));
                    }
            }

            _superGuideService.ClearAllSuperGuides();

            foreach(SuperGuide superGuide in superGuides)
            {
                _superGuideService.Create(superGuide.GuideId, superGuide.Language);
            }
        }

    }
}
