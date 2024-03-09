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
    /// Логика взаимодействия для PHistotyGet.xaml
    /// </summary>
    public partial class PHistoryGet : Page
    {
        HistoryGetTableAdapter historyGet = new HistoryGetTableAdapter();

        BooksTableAdapter books = new BooksTableAdapter();
        UsersTableAdapter users = new UsersTableAdapter();
        public PHistoryGet()
        {
            InitializeComponent();
            DDate.SelectedDate = DateTime.Now.Date;
            UpdateInfo();
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _historyGet = (HistoryGetRow)(DataGrid.SelectedItem as DataRowView).Row;
                historyGet.DeleteQuery(_historyGet.ID);

                books.StatusUpdate("Доступно", _historyGet.idBooks);

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
                if (CUsers.SelectedIndex == -1 ||
                    CBooks.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(DDate.SelectedDate.ToString()))
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var _historyGet = (HistoryGetRow)(DataGrid.SelectedItem as DataRowView).Row;

                var book = (BooksRow)(CBooks.SelectedItem as DataRowView).Row;
                var user = (UsersRow)(CUsers.SelectedItem as DataRowView).Row;

                if(book.ID != _historyGet.idBooks)
                {
                    books.StatusUpdate("Доступно", _historyGet.idBooks);
                    books.StatusUpdate("Выдан", _historyGet.idBooks);
                }

                historyGet.UpdateQuery(DDate.SelectedDate, user.ID, book.ID, _historyGet.ID);

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
                if (CUsers.SelectedIndex == -1 ||
                    CBooks.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(DDate.SelectedDate.ToString()))
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var book = (BooksRow)(CBooks.SelectedItem as DataRowView).Row;
                var user = (UsersRow)(CUsers.SelectedItem as DataRowView).Row;

                books.StatusUpdate("Доступно", book.ID);

                historyGet.InsertQuery(DDate.SelectedDate, user.ID, book.ID);
                UpdateInfo();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1)
            {
                BUpdate.IsEnabled = false;
                BDelete.IsEnabled = false;

            }
            else if(DataGrid.SelectedIndex < historyGet.GetData().Count())
            {
                var _historyGet = (HistoryGetRow)(DataGrid.SelectedItem as DataRowView).Row;

                FillComboBox(CUsers, _historyGet.idUsers);
                FillComboBox(CBooks, _historyGet.idBooks);

                DDate.SelectedDate = _historyGet.GetDate;

                BUpdate.IsEnabled = true;
                BDelete.IsEnabled = true;
            }
        }

        private void UpdateInfo()
        {
            try
            {
                CBooks.ItemsSource = books.GetData();
                CUsers.ItemsSource = users.GetData();

                CBooks.DisplayMemberPath = "Title";
                CUsers.DisplayMemberPath = "Email";

                DataGrid.ItemsSource = historyGet.GetData();
            }
            catch
            {
                ModelDialog.ShowDialog(this, "Не удалось подключиться к бд");
            }
        }

        private void FillComboBox(ComboBox comboBox, int ID)
        {
            if (comboBox == CUsers)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    var us = (UsersRow)(comboBox.Items[i] as DataRowView).Row;

                    if (us.ID == ID)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    var book = (BooksRow)(comboBox.Items[i] as DataRowView).Row;

                    if (book.ID == ID)
                    {
                        comboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
