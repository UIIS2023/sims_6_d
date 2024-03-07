using Project.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Project.Command.OwnerCommands.YourAccommodationsCommands
{
    public class NextImageCommand : CommandBase
    {
        private readonly MoreAccommodationInfoViewModel _viewModel;

        public NextImageCommand(MoreAccommodationInfoViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Index++;

            if (_viewModel.Index > _viewModel.Images.Count - 1)
            {
                _viewModel.Index = 0;
            }

            _viewModel.ImageUrl = _viewModel.Images[_viewModel.Index].Url;
        }
    }
}
