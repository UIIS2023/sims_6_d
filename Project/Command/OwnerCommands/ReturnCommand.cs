using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands
{
    public class ReturnCommand : CommandBase
    {
        private readonly ViewModelBase viewModel;

        public ReturnCommand(ViewModelBase viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            YourAccommodationsView yourAccommodationsView = new YourAccommodationsView(viewModel.User);
            yourAccommodationsView.Show();
            viewModel.Window.Close();
        }
    }
}
