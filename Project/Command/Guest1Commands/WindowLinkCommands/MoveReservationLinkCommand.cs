using Project.View.Guest1View;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest1Commands.WindowLinkCommands
{
    public class MoveReservationLinkCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public MoveReservationLinkCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            MoveReservationWindow moveReservationWindow = new MoveReservationWindow(viewModelBase.User);
            moveReservationWindow.Show();
            viewModelBase.Window.Close();
        }
    }
}
