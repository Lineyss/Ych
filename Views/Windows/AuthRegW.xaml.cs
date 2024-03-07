using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp1.Views.DialogWindow;
using WpfApp1.Views.Pages;
using WpfApp1.Views.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        LibrarianTableAdapter librarianTable = new LibrarianTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthP authP = new AuthP();

            if (authP.GetType() != frame.Content.GetType())
            {
                frame.Navigate(authP);
                return;
            }
            authP = (AuthP)frame.Content;

            if(string.IsNullOrWhiteSpace(authP.TBPassword.Password) || string.IsNullOrWhiteSpace(authP.TBLogin.Text))
            {
                ShowDialogItem("Все поля должны быть заполненны");
                return;
            }

            if (CheckExistUser(users, authP))
            {
                WReader wReader = new WReader();
                wReader.Show();
                Close();
                return;
            }

            if(CheckExistUser(librarianTable, authP))
            {
                WLibraryan wLibraryan = new WLibraryan();
                wLibraryan.Show();
                Close();
                return;
            }

            authP.TBLogin.Text = "";
            authP.TBPassword.Password = "";

            ShowDialogItem("Не верный логин или пароль");
        }

        private void ShowDialogItem(string text)
        {
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.Text.Text = text;
            dialogWindow.Show();
            IsEnabled = false;
            dialogWindow.Closed += DialogWindow_Closed;
        }

        private void DialogWindow_Closed(object sender, EventArgs e)
        {
            IsEnabled = true;
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            RegP regP = new RegP();

            if (regP.GetType() != frame.Content.GetType())
            {
                frame.Navigate(regP);
                return;
            }
            regP = (RegP)frame.Content;
            
            if (string.IsNullOrWhiteSpace(regP.TBPassword.Password) || string.IsNullOrWhiteSpace(regP.TBLogin.Text) || string.IsNullOrWhiteSpace(regP.TBEmail.Text) || regP.Role.SelectedIndex == -1)
            {
                ShowDialogItem("Все поля должны быть заполненны");
                return;
            }

            if (CheckExistUser(users, regP) || CheckExistUser(librarianTable, regP))
            {
                ShowDialogItem("Пользователь с таким логином уже существует");

                regP.TBLogin.Text = "";
                regP.TBPassword.Password = "";
                regP.TBEmail.Text = "";

                return;
            }

            try
            {
                if(regP.Role.SelectedIndex == 0)
                    users.InsertQuery(regP.TBLogin.Text, regP.TBPassword.Password);
                else
                    librarianTable.InsertQuery(regP.TBLogin.Text, regP.TBPassword.Password);

                frame.Navigate(new AuthP());
            }
            catch(Exception ex)
            {
                ShowDialogItem(ex.Message);
            }
        }

        private bool CheckExistUser(UsersTableAdapter tableData, AuthP authP)
        {
            var a = tableData.GetData().FirstOrDefault(element => element.Login == authP.TBLogin.Text 
                && element.Password == authP.TBPassword.Password) == null ? false : true;

            return a;
        }

        private bool CheckExistUser(LibrarianTableAdapter tableData, AuthP authP)
        {
            return tableData.GetData().FirstOrDefault(element => element.Login == authP.TBLogin.Text 
                && element.Password == authP.TBPassword.Password) == null ? false : true;
        }

        private bool CheckExistUser(UsersTableAdapter tableData, RegP authP)
        {
            return tableData.GetData().FirstOrDefault(element => element.Login == authP.TBLogin.Text
                && element.Password == authP.TBPassword.Password) == null ? false : true;
        }

        private bool CheckExistUser(LibrarianTableAdapter tableData, RegP authP)
        {
            return tableData.GetData().FirstOrDefault(element => element.Login == authP.TBLogin.Text
                && element.Password == authP.TBPassword.Password) == null ? false : true;
        }

    }
}
