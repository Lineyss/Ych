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
using WpfApp1.Views.Pages;
using static WpfApp1.LibraryDataSet;

namespace WpfApp1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для WMain.xaml
    /// </summary>
    public partial class WAdminMain : Window
    {
        UsersRow user;
        public WAdminMain(UsersRow user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new PRole());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new PUsers());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new PBooks());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new PHistoryGet());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new PGetBook(user));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WAuth auth = new WAuth();
            auth.Show();
            Close();
        }
    }
}
