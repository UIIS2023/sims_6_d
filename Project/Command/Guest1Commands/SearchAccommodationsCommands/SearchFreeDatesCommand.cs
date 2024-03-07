using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest1Commands.SearchAccommodationsCommands
{
    public class SearchFreeDatesCommand : CommandBase
    {
        private readonly ReserveAccommodationViewModel viewModel;

        public SearchFreeDatesCommand(ReserveAccommodationViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.FindFreeDates(viewModel.StartDate, viewModel.EndDate);
        }
    }
}
