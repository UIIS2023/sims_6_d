using Project.View.Guest1View;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest1Commands.WindowLinkCommands
{
    public class ProfileLinkCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public ProfileLinkCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            ProfileWindow profileWindow = new ProfileWindow(viewModelBase.User);
            profileWindow.Show();
            viewModelBase.Window.Close();
        }
    }
}
