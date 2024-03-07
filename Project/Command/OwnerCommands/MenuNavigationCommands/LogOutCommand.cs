using Project.View;
using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.MenuNavigationCommands
{
    public class LogOutCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public LogOutCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            SignInView signInView = new SignInView();
            signInView.Show();
            viewModelBase.Window.Close();
        }
    }
}
