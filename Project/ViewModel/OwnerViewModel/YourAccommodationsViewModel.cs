using Project.Command.OwnerCommands;
using Project.Command.OwnerCommands.MenuNavigationCommands;
using Project.Command.OwnerCommands.YourAccommodationsCommands;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.OwnerViewModel
{
    public class YourAccommodationsViewModel : ViewModelBase
    {
        private readonly AccommodationService _accommodationService;

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public YourAccommodationsViewModel(User user, Window window) 
        {
            User = user;
            Window = window;
            _accommodationService = new();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllOwnerAccommodations(user.Id));
            BurgerMenuCommand = new BurgerMenuCommand(this);
            MoreInfoCommand = new MoreInfoCommand(this);

        }

        public ICommand BurgerMenuCommand { get;  }
        public ICommand MoreInfoCommand { get; }
        

    }
}
