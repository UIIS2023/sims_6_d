using Project.View.Guest1View;
using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.MenuNavigationCommands
{
    public class AddAccommodationNavigationCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public AddAccommodationNavigationCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            AddAccommodationForm addAccommodationForm = new AddAccommodationForm(viewModelBase.User);
            addAccommodationForm.Show();
            viewModelBase.Window.Close();
        }
    }
}
