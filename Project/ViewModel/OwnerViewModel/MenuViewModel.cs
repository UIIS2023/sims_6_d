using Project.Command.OwnerCommands;
using Project.Command.OwnerCommands.MenuNavigationCommands;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.OwnerViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(User user, Window window)
        {
            User = user;
            Window = window;

            HomeNavigationCommand = new HomeNavigationCommand(this);
            AddAccommodationCommand = new AddAccommodationNavigationCommand(this);
            ReservationsNavigationCommand = new ReservationsNavigationCommand(this);
            BackNavigationCommand = new BackNavigationCommand(this);
            LogOutCommand = new LogOutCommand(this);
        }

        public ICommand HomeNavigationCommand { get;  }
        public ICommand AddAccommodationCommand { get; }
        public ICommand ReservationsNavigationCommand { get; }
        public ICommand BackNavigationCommand { get; }
        public ICommand LogOutCommand { get; }
    }
}
