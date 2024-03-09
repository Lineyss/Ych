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
using WpfApp1.LibraryDataSetTableAdapters;
using WpfApp1.Views.DialogWindow;
using WpfApp1.Models;


namespace WpfApp1.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для WAuth.xaml
    /// </summary>
    public partial class WAuth : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        RoleTableAdapter roles = new RoleTableAdapter();

        public WAuth()
        {
            InitializeComponent();
        }

        private void BAuth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBLogin.Text) || string.IsNullOrWhiteSpace(TBPassword.Password))
                ModelDialog.ShowDialog(this, "Поля не могут быть пустыми");

            try
            {
                var _user = users.GetData().FirstOrDefault(user => user.Login == TBLogin.Text && user.Password == TBPassword.Password);
                
                if(_user == null)
                {
                    ModelDialog.ShowDialog(this, "Не верный логин или пароль");
                    TBPassword.Password = "";
                    return;
                }

                switch (roles.GetData().First(role => role.ID == _user.idRole).Title)
                {
                    case "admin":
                        WAdminMain wAdminMain = new WAdminMain(_user);
                        wAdminMain.Show();
                        Close();
                        return;
                    case "user":
                        WUserMain wUserMain = new WUserMain(_user);
                        wUserMain.Show();
                        Close();
                        return;
                    default:
                        ModelDialog.ShowDialog(this, "Функционал для этой роли еще не создан");
                        break;
                }
            }
            catch(Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WMainNewPassword wMainNewPassword = new WMainNewPassword();
            wMainNewPassword.Show();
            Close();
        }
    }
}
