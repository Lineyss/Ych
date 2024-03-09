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
    /// Логика взаимодействия для PStatus.xaml
    /// </summary>
    public partial class PRole : Page
    {
        RoleTableAdapter roles = new RoleTableAdapter();
        public PRole()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var role = (RoleRow)(DataGrid.SelectedItem as DataRowView).Row;
                roles.DeleteQuery(role.ID);

                UpdateInfo();
            }
            catch(Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Title.Text))
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var role = (RoleRow)(DataGrid.SelectedItem as DataRowView).Row;
                roles.UpdateQuery(Title.Text, role.ID);

                UpdateInfo();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(Title.Text))
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                roles.InsertQuery(Title.Text);

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
                BUpdate.IsEnabled = false;
                BDelete.IsEnabled = false;
                Title.Text = "";
            }
            else if (DataGrid.SelectedIndex < roles.GetData().Count())
            {
                var role = (RoleRow)(DataGrid.SelectedItem as DataRowView).Row;
                Title.Text = role.Title;
                BUpdate.IsEnabled = true;
                BDelete.IsEnabled = true;
            }
        }

        private void UpdateInfo()
        {
            try
            {
                DataGrid.ItemsSource = roles.GetData();
            }
            catch
            {
                ModelDialog.ShowDialog(this, "Не удалось подключиться к бд");
            }
        }
    }
}
