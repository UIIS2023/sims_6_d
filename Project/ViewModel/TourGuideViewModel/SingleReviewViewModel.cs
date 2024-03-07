using Project.Command;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class SingleReviewViewModel:CloseableViewModel
    {

		private int _id;
		public int Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
				OnPropertyChanged(nameof(Id));
			}
		}

		private string _guestname = string.Empty;
		public string guestName
		{
			get
			{
				return _guestname;
			}
			set
			{
				_guestname = value;
				OnPropertyChanged(nameof(guestName));
			}
		}

		private string _tourname = string.Empty;
		public string tourName
		{
			get
			{
				return _tourname;
			}
			set
			{
				_tourname = value;
				OnPropertyChanged(nameof(tourName));
			}
		}

		private string _boardingpoint = string.Empty;
		public string boardingPoint
		{
			get
			{
				return _boardingpoint;
			}
			set
			{
				_boardingpoint = value;
				OnPropertyChanged(nameof(boardingPoint));
			}
		}

		private int _knowledgerating;
		public int knowledgeRating
		{
			get
			{
				return _knowledgerating;
			}
			set
			{
				_knowledgerating = value;
				OnPropertyChanged(nameof(knowledgeRating));
			}
		}

		private int _languagerating;
		public int languageRating
		{
			get
			{
				return _languagerating;
			}
			set
			{
				_languagerating = value;
				OnPropertyChanged(nameof(languageRating));
			}
		}

		private int _interestingrating;
		public int interestingRating
		{
			get
			{
				return _interestingrating;
			}
			set
			{
				_interestingrating = value;
				OnPropertyChanged(nameof(interestingRating));
			}
		}

		private double _avgrating;
		public double avgRating
		{
			get
			{
				return _avgrating;
			}
			set
			{
				_avgrating = value;
				OnPropertyChanged(nameof(avgRating));
			}
		}

		private string _text = string.Empty;
		public string text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				OnPropertyChanged(nameof(text));
			}
		}

        private readonly PresentGuestsService presentGuestsService;
        private readonly TourReviewService _tourReviewService;

        public SingleReviewViewModel(ReviewDisplay selected, TourReviewService tourReviewService)
        {
			presentGuestsService = new PresentGuestsService();
			_tourReviewService = tourReviewService;

            Id = selected.Id;
            guestName = selected.userName;
            tourName = selected.tourName;



            boardingPoint = presentGuestsService.GetBoardingPoint(selected.userId, selected.appointmentId);


            knowledgeRating = selected.knowledgeRating;
            languageRating = selected.languageRating;
            interestingRating = selected.interestingRating;
            avgRating = selected.avgRating;
            text = selected.review;
        }


        private RelayCommand makeInvalidCommand;
        public ICommand MakeInvalidCommand 
		{
            get
            {
                if (makeInvalidCommand == null)
                {
                    makeInvalidCommand = new RelayCommand(param => this.MakeInvalid(), param => this.CanMakeInvalid());
                }
                return makeInvalidCommand;
            }
        }

        private bool CanMakeInvalid()
        {

				return _tourReviewService.IsValid(Id);
            
        }

        private void MakeInvalid()
        {
            if (MessageBox.Show("Are you sure you want to mark this review as invalid?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //no
            }
            else
            {
                //yes
                _tourReviewService.MarkAsInvalid(Id);
            }
        }

        private RelayCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(param => this.Close(), param => this.CanClose());
                }
                return closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            this.OnClosingRequest();
        }

    }
}
