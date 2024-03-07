using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest1Commands.SearchAccommodationsCommands
{
    public class SearchAccommodationCommand : CommandBase
    {
        private readonly SearchAccommodationsViewModel _viewModel;

        public SearchAccommodationCommand(SearchAccommodationsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.FilterAccommodations();
        }
    }
}
