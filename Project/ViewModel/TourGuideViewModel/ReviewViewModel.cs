using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View.TourGuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class ReviewViewModel:ViewModelBase, IObserver
    {
        private readonly TourReviewService _tourReviewService;

        private ObservableCollection<ReviewDisplay> _tourReviews;
        public ObservableCollection<ReviewDisplay> TourReviews
        {
            get
            {
                return _tourReviews;
            }
            set
            {
                _tourReviews = value;
                OnPropertyChanged(nameof(TourReviews));
            }
        }

        private ReviewDisplay _selectedReview;
		public ReviewDisplay SelectedReview
		{
			get
			{
				return _selectedReview;
			}
			set
			{
				_selectedReview = value;
				OnPropertyChanged(nameof(SelectedReview));

			}
		}

        public int GuideId { get; set; }

        public ReviewViewModel(int guideId)
        {
			_tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);

            GuideId = guideId;
            
            TourReviews = new ObservableCollection<ReviewDisplay>(_tourReviewService.GetReviewForDisplay(GuideId));
        }



        private RelayCommand seeDetailsCommand;
        public ICommand SeeDetailsCommand
		{
            get
            {
                if (seeDetailsCommand == null)
                {
                    seeDetailsCommand = new RelayCommand(param => this.SeeDetails(), param => this.CanSeeDetails());
                }
                return seeDetailsCommand;
            }
        }

        private bool CanSeeDetails()
        {
            return SelectedReview != null;
        }

        private void SeeDetails()
        {
                SingleReview singleReview = new SingleReview(SelectedReview, _tourReviewService);
                singleReview.Show();
        }

        public void Update()
        {
            TourReviews.Clear();
            foreach(var review in _tourReviewService.GetReviewForDisplay(GuideId))
            {
                TourReviews.Add(review);
            }
        }
    }
}
