using Project.View.OwnerView;
using Project.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.YourAccommodationsCommands
{
    public class MoreInfoCommand : CommandBase
    {
        private readonly YourAccommodationsViewModel _yourAccommodationsViewModel;

        public MoreInfoCommand(YourAccommodationsViewModel yourAccommodationsViewModel)
        {
            _yourAccommodationsViewModel = yourAccommodationsViewModel;
        }

        public override void Execute(object? parameter)
        {
            MoreAccommodationInfoView moreAccommodationInfoView = new MoreAccommodationInfoView(_yourAccommodationsViewModel.User, _yourAccommodationsViewModel.SelectedAccommodation);
            moreAccommodationInfoView.Show();
            _yourAccommodationsViewModel.Window.Close();
        }
    }
}
