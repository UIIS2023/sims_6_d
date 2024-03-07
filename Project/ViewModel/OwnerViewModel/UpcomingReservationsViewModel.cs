using System;
using Project.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Project.Service;
using System.Windows;
using System.Windows.Input;
using Project.Command.OwnerCommands;

namespace Project.ViewModel.OwnerViewModel
{
    public class UpcomingReservationsViewModel : ViewModelBase
    {
        private readonly AccommodationReservationService _accommodationReservationService;
        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }

        public ICommand BurgerMenuCommand { get; }

        public UpcomingReservationsViewModel(User user, Window window)
        {
            User = user;
            Window = window;

            _accommodationReservationService = new AccommodationReservationService();
            CurrentReservations = new(_accommodationReservationService.GetOwnersCurrentReservations(User.Id));
            BurgerMenuCommand = new BurgerMenuCommand(this);
        }


    }
}
