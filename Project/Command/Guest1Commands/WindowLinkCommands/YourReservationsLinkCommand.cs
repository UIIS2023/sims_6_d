using Project.View.Guest1View;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Command.Guest1Commands.WindowLinkCommands
{
    public class YourReservationsLinkCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public YourReservationsLinkCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            YourReservationsWindow yourReservationsWindow = new YourReservationsWindow(viewModelBase.User);
            yourReservationsWindow.Show();
            viewModelBase.Window.Close();
        }
    }
}
