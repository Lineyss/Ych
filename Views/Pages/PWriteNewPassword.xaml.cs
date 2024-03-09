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
using WpfApp1.LibraryDataSetTableAdapters;
using WpfApp1.Models;
using WpfApp1.Views.Windows;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PWriteNewPassword.xaml
    /// </summary>
    public partial class PWriteNewPassword : Page
    {
        UsersTableAdapter users = new UsersTableAdapter();
        Frame Frame;
        string email;
        public PWriteNewPassword(Frame Frame, string email)
        {
            InitializeComponent();

            this.Frame = Frame;
            this.email = email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Password == PasswordConfirm.Password)
            {
                WAuth auth = new WAuth();
                auth.Show();
                Window ownerWindow = Window.GetWindow(Frame);

                users.UpdatePassword(Password.Password, email);

                ownerWindow.Close();
                return;
            }

            PasswordConfirm.Password = "";
            ModelDialog.ShowDialog(this, "Пароли не совпадают");
        }
    }
}
