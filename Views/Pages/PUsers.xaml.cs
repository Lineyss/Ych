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
using WpfApp1.Models;
using static WpfApp1.LibraryDataSet;

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PUsers.xaml
    /// </summary>
    public partial class PUsers : Page
    {
        UsersTableAdapter users = new UsersTableAdapter();
        RoleTableAdapter roles = new RoleTableAdapter();
        public PUsers()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = (UsersRow)(DataGrid.SelectedItem as DataRowView).Row;
                users.DeleteQuery(user.ID);

                UpdateInfo();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(TLogin.Text)       ||
                   string.IsNullOrWhiteSpace(TPassword.Text)    ||
                   string.IsNullOrWhiteSpace(TEmail.Text)       ||
                   string.IsNullOrWhiteSpace(TName.Text)        ||
                   ComboBox.SelectedIndex == -1)
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var role = (RoleRow)(ComboBox.SelectedItem as DataRowView).Row;
                var user = (UsersRow)(DataGrid.SelectedItem as DataRowView).Row;

                users.UpdateQuery(TLogin.Text,
                                  TPassword.Text,
                                  TName.Text,
                                  TEmail.Text,
                                  role.ID,
                                  user.ID);

                UpdateInfo();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TLogin.Text)      ||
                    string.IsNullOrWhiteSpace(TPassword.Text)   ||
                    string.IsNullOrWhiteSpace(TEmail.Text)      ||
                    string.IsNullOrWhiteSpace(TName.Text)       ||
                    ComboBox.SelectedIndex == -1)
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var role = (RoleRow)(ComboBox.SelectedItem as DataRowView).Row;

                users.InsertQuery(TLogin.Text,
                                  TPassword.Text,
                                  TName.Text,
                                  TEmail.Text,
                                  role.ID);
                UpdateInfo();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataGrid.SelectedIndex == -1)
            {
                TLogin.Text = "";
                TPassword.Text = "";
                TEmail.Text = "";
                TName.Text = "";

                BUpdate.IsEnabled = false;
                BDelete.IsEnabled = false;
            }
            else if (DataGrid.SelectedIndex < users.GetData().Count())
            {
                var user = (UsersRow)(DataGrid.SelectedItem as DataRowView).Row;

                TLogin.Text = user.Login;
                TPassword.Text = user.Password;
                TEmail.Text = user.Email;
                TName.Text = user.Name;

                for (int i = 0;i<ComboBox.Items.Count;i++)
                {
                    var role = (RoleRow)(ComboBox.Items[i] as DataRowView).Row;

                    if (role.ID == user.idRole)
                    {
                        ComboBox.SelectedIndex = i;
                        break;
                    }
                }

                BUpdate.IsEnabled = true;
                BDelete.IsEnabled = true;
            }
        }

        private void UpdateInfo()
        {
            try
            {
                ComboBox.ItemsSource = roles.GetData();
                ComboBox.DisplayMemberPath = "Title";
                DataGrid.ItemsSource = users.GetData();
            }
            catch
            {
                ModelDialog.ShowDialog(this, "Не удалось подключиться к бд");
            }
        }
    }
}
