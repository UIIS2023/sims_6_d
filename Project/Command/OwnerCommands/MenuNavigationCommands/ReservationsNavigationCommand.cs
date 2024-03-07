using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.MenuNavigationCommands
{
    public class ReservationsNavigationCommand : CommandBase
    {
        private readonly ViewModelBase _viewModelBase;

        public ReservationsNavigationCommand(ViewModelBase viewModelBase)
        {
            _viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            UpcomingReservationsView upcomingReservationsView = new UpcomingReservationsView(_viewModelBase.User);
            upcomingReservationsView.Show();
            _viewModelBase.Window.Close();
        }
    }
}
