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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PWriteCod.xaml
    /// </summary>
    public partial class PWriteCod : Page
    {
        int Kod;
        string email;
        Frame Frame;
        public PWriteCod(Frame Frame, int Kod, string email)
        {
            InitializeComponent();
            this.Frame = Frame;
            this.Kod = Kod;
            this.email = email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Convert.ToInt32(PKod.Text) == Kod)
                {
                    Frame.Navigate(new PWriteNewPassword(Frame, email));
                    return;
                }

                ModelDialog.ShowDialog(this, "Не верный код.");
            }
            catch
            {
                ModelDialog.ShowDialog(this, "Не верный код.");
            }
        }

        private void PKod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true; // Отменяет ввод символа, если он не является цифрой
                    break;
                }
            }
        }
    }
}
