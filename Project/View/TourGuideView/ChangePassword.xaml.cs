using Project.Model;
using Project.RepositoryInterfaces;
using Project.ViewModel.TourGuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {

        private IUserRepository _userRepository;
        public event PropertyChangedEventHandler? PropertyChanged;


        private string _current = string.Empty;
        public string Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                OnPropertyChanged(nameof(Current));
            }
        }

        private string _new = string.Empty;
        public string New
        {
            get
            {
                return _new;
            }
            set
            {
                _new = value;
                OnPropertyChanged(nameof(New));
            }
        }

        private string _confirm = string.Empty;
        public string Confirm
        {
            get
            {
                return _confirm;
            }
            set
            {
                _confirm = value;
                OnPropertyChanged(nameof(Confirm));
            }
        }

        private User _currentUser;
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
        public ChangePassword(User user)
        {
            InitializeComponent();
            DataContext = this;

            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            CurrentUser = user;


        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            Current = oldPw.Password;
            New = newPw.Password;
            Confirm = confirmPw.Password;

            if (Current == string.Empty)
            {
                DialogResult dialogResult1 = System.Windows.Forms.MessageBox.Show("You must enter current password", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (New == string.Empty)
            {
                DialogResult dialogResult2 = System.Windows.Forms.MessageBox.Show("You must enter new password", "Error", MessageBoxButtons.OK);
                return;
            }
            else if (Confirm == string.Empty)
            {
                DialogResult dialogResult3 = System.Windows.Forms.MessageBox.Show("You must confirm new password", "Error", MessageBoxButtons.OK);
                return;
            }
            else if(Current != CurrentUser.Password) 
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("You entered the wrong current password", "Error", MessageBoxButtons.OK);
                return;
            }
            else if(New != Confirm)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("The new and confirmed passwords are different", "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                User changedUser = _userRepository.ChangePassword(CurrentUser, New);
                if(changedUser != null)
                {
                    System.Windows.Forms.MessageBox.Show("Password changed successfully");
                    Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("An error occurred, please try again later");
                    Close();
                }
            }

        }

        private void dismissBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
