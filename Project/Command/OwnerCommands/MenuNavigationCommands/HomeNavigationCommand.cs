using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.MenuNavigationCommands
{
    public class HomeNavigationCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public HomeNavigationCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {

            YourAccommodationsView yourAccommodationsView = new YourAccommodationsView(viewModelBase.User);
            yourAccommodationsView.Show();
            viewModelBase.Window.Close();
        }
    }
}
