using Project.View;
using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Command.OwnerCommands.MenuNavigationCommands
{
    class BackNavigationCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;
        private Window PreviousWindow { get; set; }

        public BackNavigationCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            Window previousWindow = new Window();
            if (SignInView.PreviousWindow != null)
            {
                // PreviousWindow.Visibility = Visibility.Collapsed;
                PreviousWindow = SignInView.PreviousWindow;
                PreviousWindow.Show();
                viewModelBase.Window.Close();
            }
            /*YourAccommodationsView yourAccommodationsView = new YourAccommodationsView(viewModelBase.User);
            yourAccommodationsView.Show();
            viewModelBase.Window.Close();*/
        }
    }
}
