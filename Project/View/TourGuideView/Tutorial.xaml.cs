using Project.ViewModel.TourGuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _minutesTime;
        public string MinutesTime
        {
            get
            {
                return _minutesTime;
            }
            set
            {
                _minutesTime = value;
                OnPropertyChanged(nameof(MinutesTime));
            }
        }

        private string _playpause = string.Empty;
        public string PlayPause
        {
            get
            {
                return _playpause;
            }
            set
            {
                _playpause = value;
                OnPropertyChanged(nameof(PlayPause));
            }
        }

        private bool _isPlayed = false;
        public bool IsPalayed
        {
            get
            {
                return _isPlayed;
            }
            set
            {
                _isPlayed = value;
                OnPropertyChanged(nameof(IsPalayed));
                if (_isPlayed)
                {
                    ImageSource = "../../Resources/Images/pause.png";
                }
                else
                {
                    ImageSource = "../../Resources/Images/paper-plane.png";
                }
            }
        }


        private double videoTime;
        public double VideoTime
        {
            get
            {
                return videoTime;
            }
            set
            {
                if (videoTime != value)
                {
                    videoTime = Math.Round(value, 0);
                    OnPropertyChanged(nameof(VideoTime));

                    // Update the mediaElement1.Position
                    mediaElement1.Position = TimeSpan.FromSeconds(videoTime);
                    int min = Convert.ToInt32(videoTime) / 60;
                    int sec = Convert.ToInt32(videoTime) % 60;
                    MinutesTime = $"{min}:{sec}";

                }
            }
        }

        private double videoDuration;
        public double VideoDuration
        {
            get { return videoDuration; }
            set
            {
                if (videoDuration != value)
                {
                    videoDuration = value;
                    OnPropertyChanged(nameof(VideoDuration));
                }
            }
        }

        private string _imageSource = string.Empty;
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        DispatcherTimer timer;
        public Tutorial()
        {
            InitializeComponent();
            DataContext = this;

            timer =  new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            VideoTime = mediaElement1.Position.TotalSeconds;

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
            PlayPause = "Pause";
            IsPalayed = true;
            ImageSource = "../../Resources/Images/pause.png";


        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
            PlayPause = "Play";
            IsPalayed = false;
            ImageSource = "../../Resources/Images/paper-plane.png";

        }

        private void slider_seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Position = TimeSpan.FromSeconds(slider_seek.Value);
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement1.NaturalDuration.TimeSpan;
            VideoDuration = ts.TotalSeconds;
            slider_seek.Maximum = VideoDuration;
            
        }

        private void playPause_Click(object sender, RoutedEventArgs e)
        {
            if (IsPalayed)
            {
                mediaElement1.Pause();
                PlayPause = "Play";
                IsPalayed = false;
            }
            else
            {
                mediaElement1.Play();
                PlayPause = "Pause";
                IsPalayed = true;
            }

        }
    }
}
