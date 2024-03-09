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
    /// Логика взаимодействия для PGetBook.xaml
    /// </summary>
    public partial class PGetBook : Page
    {
        BooksTableAdapter books = new BooksTableAdapter();
        HistoryGetTableAdapter historyGet = new HistoryGetTableAdapter();
        UsersRow user;
        public PGetBook(UsersRow user)
        {
            InitializeComponent();
            UpdateData();

            this.user = user;
        }

        private void Get_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = (BooksRow)(DataGrid.SelectedItem as DataRowView).Row;
                books.StatusUpdate("Выдан", book.ID);

                historyGet.InsertQuery(DateTime.Now, user.ID, book.ID);

                UpdateData();
            }
            catch (Exception ex)
            {
                ModelDialog.ShowDialog(this, ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataGrid.SelectedIndex > -1 && DataGrid.SelectedIndex < books.GetData().Count())
            {
                var book = (BooksRow)(DataGrid.SelectedItem as DataRowView).Row;

                if(book.Status == "Доступно")
                    BGet.IsEnabled = true;
            }
            else
            {
                BGet.IsEnabled = false;
            }
        }

        private void UpdateData()
        {
            try
            {
                DataGrid.ItemsSource = books.GetData();
            }
            catch
            {
                try
                {
                    DataGrid.ItemsSource = books.GetData();
                }
                catch
                {
                    ModelDialog.ShowDialog(this, "Не удалось подключиться к бд");
                }
            }
        }
    }
}
