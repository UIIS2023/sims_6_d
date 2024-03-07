using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.TourGuideViewModel
{
    public class QuitSharedViewModel: ViewModelBase
    {

		private bool _isQuit = false;
		public bool IsQuit
		{
			get
			{
				return _isQuit;
			}

			set
			{
				_isQuit = value;
				OnPropertyChanged(nameof(IsQuit));
			}
		}
	}
}
