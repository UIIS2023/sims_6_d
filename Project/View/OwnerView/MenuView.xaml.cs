using Project.Model;
using Project.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View.OwnerView
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        private readonly MenuViewModel menuViewViewModel;
             
        public MenuView(User user)
        {
            InitializeComponent();
            menuViewViewModel = new(user, this);
            DataContext = menuViewViewModel;

        }

        /*private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignInView.PreviousWindow != null)
            {
                SignInView.PreviousWindow.Show();
                Close();
            }
        }*/
    }
}
