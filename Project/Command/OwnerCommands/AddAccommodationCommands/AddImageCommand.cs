using Project.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands.AddAccommodationCommands
{
    public class AddImageCommand : CommandBase
    {
        private readonly AddAccommodationViewModel viewModel;

        public AddImageCommand(AddAccommodationViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(viewModel.ImageUrl))
            {
                return;
            }

            if (viewModel.ImageUrls.Contains(viewModel.ImageUrl))
            {
                return;
            }

            viewModel.ImageUrls.Add(viewModel.ImageUrl);
            viewModel.ImageUrl = string.Empty;
        }
    }
}
