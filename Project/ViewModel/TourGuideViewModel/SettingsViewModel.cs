using Project.Command;
using Project.Model;
using Project.RepositoryInterfaces;
using Project.Service;
using Project.View.TourGuideView;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class SettingsViewModel:CloseableViewModel
    {

        private IUserRepository _userRepository;

        private User _currentUser = new User();
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public EventHandler CloseRequested;


        private readonly TourService _tourService;
        private readonly CouponService _couponeService;


        public SettingsViewModel(Model.User user)
        {
            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            _tourService = new TourService();
            _couponeService = new CouponService();
            CurrentUser = user;
        }

        private RelayCommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (changePasswordCommand == null)
                {
                    changePasswordCommand = new RelayCommand(param => this.ChangePassword(), param => this.CanChangePassword());
                }
                return changePasswordCommand;
            }
        }



        private bool CanChangePassword()
        {
            return true;
        }

        private void ChangePassword()
        {
            ChangePassword changePasswordView = new ChangePassword(CurrentUser);
            changePasswordView.Show();
        }

        private bool CanQuit()
        {
            return true;
        }

        private RelayCommand quitCommand;
        public ICommand QuitCommand
        {
            get
            {
                if (quitCommand == null)
                {
                    quitCommand = new RelayCommand(param => this.Quit(), param => this.CanQuit());
                }
                return quitCommand;
            }
        }
        private void Quit()
        {

            if (System.Windows.MessageBox.Show("Are you sure you want to quit your job? This decision is final and cannot be reversed.",
                    "Quit job",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _couponeService.GuideQuitJobCorection(CurrentUser.Id);
                _tourService.CancelAllToursOfGude(CurrentUser);
                RemoveUser(CurrentUser.Id);
                RestartApp();
            }
        }

        private void RestartApp()
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        private void RemoveUser(int id)
        {
            _userRepository.Remove(id);
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
            this.OnClosingRequest();
        }

    }
}
