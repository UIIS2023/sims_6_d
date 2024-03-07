using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Model;
using Project.Service;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.Repository;
using System.Collections.ObjectModel;
using Project.Observer;
using Project.ViewModel;
using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.ViewModel.Guest1ViewModel;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MoveReservationWindow.xaml
    /// </summary>
    public partial class MoveReservationWindow : Window
    {
        private readonly MoveReservationViewModel moveReservationViewModel;
        public MoveReservationWindow(User user)
        {
            InitializeComponent();
            moveReservationViewModel = new MoveReservationViewModel(user, this);
            DataContext = moveReservationViewModel;
        }
    }
}
