using Project.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project.ViewModel.TourGuideViewModel
{
    public class TutorialViewModel: ViewModelBase
    {
        private bool isPlaying;
        private TimeSpan totalTime;
        private TimeSpan currentTime;
        private double sliderValue;
        private DispatcherTimer timer;
        private MediaPlayer mediaPlayer;

        public TutorialViewModel()
        {
            isPlaying = false;
            mediaPlayer = new MediaPlayer();
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            //PlayPauseCommand = new RelayCommand(PlayPause);
            //SliderDragStartedCommand = new RelayCommand(SliderDragStarted);
            //SliderDragCompletedCommand = new RelayCommand(SliderDragCompleted);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand PlayPauseCommand { get; }
        public ICommand SliderDragStartedCommand { get; }
        public ICommand SliderDragCompletedCommand { get; }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set { SetProperty(ref isPlaying, value); }
        }

        public TimeSpan TotalTime
        {
            get { return totalTime; }
            set { SetProperty(ref totalTime, value); }
        }

        public TimeSpan CurrentTime
        {
            get { return currentTime; }
            set { SetProperty(ref currentTime, value); }
        }

        public double SliderValue
        {
            get { return sliderValue; }
            set { SetProperty(ref sliderValue, value); }
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        private void PlayPause()
        {
            if (IsPlaying)
            {
                mediaPlayer.Pause();
            }
            else
            {
                mediaPlayer.Play();
            }

            IsPlaying = !IsPlaying;
        }

        private void SliderDragStarted()
        {
            mediaPlayer.Pause();
        }

        private void SliderDragCompleted()
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(SliderValue * TotalTime.TotalSeconds);
            mediaPlayer.Play();
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            TotalTime = mediaPlayer.NaturalDuration.TimeSpan;
            SliderValue = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            IsPlaying = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = mediaPlayer.Position;
            SliderValue = CurrentTime.TotalSeconds / TotalTime.TotalSeconds;
        }

        public void LoadVideo(string videoPath)
        {
            mediaPlayer.Open(new Uri(videoPath));
        }
    }
}
