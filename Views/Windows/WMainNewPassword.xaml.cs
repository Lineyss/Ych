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

namespace WpfApp1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для WMainNewPassword.xaml
    /// </summary>
    public partial class WMainNewPassword : Window
    {
        public WMainNewPassword() 
        {
            InitializeComponent();
            Frame.Navigate(new PWriteEmail(Frame));
        }
    }
}
