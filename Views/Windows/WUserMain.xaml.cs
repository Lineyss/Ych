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
    /// Логика взаимодействия для WUserMain.xaml
    /// </summary>
    public partial class WUserMain : Window
    {
        public WUserMain(UsersRow user)
        {
            InitializeComponent();
            Frame.Navigate(new PGetBook(user));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WAuth auth = new WAuth();
            auth.Show();
            Close();
        }
    }
}
