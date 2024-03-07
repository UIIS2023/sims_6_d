using Project.View.Guest1View;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest1Commands.WindowLinkCommands
{
    public class SearchAccommodationsLinkCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public SearchAccommodationsLinkCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            SearchAccommodationsView searchAccommodationsView = new SearchAccommodationsView(viewModelBase.User);
            searchAccommodationsView.Show();
            viewModelBase.Window.Close();
        }
    }
}
