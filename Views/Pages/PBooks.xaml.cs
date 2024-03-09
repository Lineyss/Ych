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
    /// Логика взаимодействия для PBooks.xaml
    /// </summary>
    public partial class PBooks : Page
    {
        BooksTableAdapter books = new BooksTableAdapter();
        public PBooks()
        {
            InitializeComponent();
            UpdateInfo();
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var book = (BooksRow)(DataGrid.SelectedItem as DataRowView).Row;
                books.DeleteQuery(book.ID);

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
                if(string.IsNullOrWhiteSpace(TTitle.Text)           ||
                   string.IsNullOrWhiteSpace(TAuthor.Text)          ||
                   string.IsNullOrWhiteSpace(TPublication.Text)     ||
                   string.IsNullOrWhiteSpace(TISBN.Text)            ||
                   string.IsNullOrWhiteSpace(TGenre.Text)           ||
                   string.IsNullOrWhiteSpace(TYearsOfRelease.Text)  ||
                   ComboBox.SelectedIndex == -1)
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var book = (BooksRow)(DataGrid.SelectedItem as DataRowView).Row;

                string ISBN = "ISBN-" + TISBN.Text;
                int Years = Convert.ToInt32(TYearsOfRelease.Text);

                books.UpdateQuery(TTitle.Text,
                                  TAuthor.Text,
                                  TPublication.Text,
                                  ISBN,
                                  TGenre.Text,
                                  Years,
                                  ComboBox.SelectedValue.ToString(),
                                  book.ID);

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
                if (string.IsNullOrWhiteSpace(TTitle.Text)          ||
                    string.IsNullOrWhiteSpace(TAuthor.Text)         ||
                    string.IsNullOrWhiteSpace(TPublication.Text)    ||
                    string.IsNullOrWhiteSpace(TISBN.Text)           ||
                    string.IsNullOrWhiteSpace(TGenre.Text)          ||
                    string.IsNullOrWhiteSpace(TYearsOfRelease.Text) ||
                    ComboBox.SelectedIndex == -1)
                {
                    ModelDialog.ShowDialog(this, "Все поля должны быть заполненны");
                    return;
                }

                var status = ComboBox.SelectedValue.ToString().Split(' ')[1];
                string ISBN = "ISBN-" + TISBN.Text;
                int Years = Convert.ToInt32(TYearsOfRelease.Text);

                books.InsertQuery(TTitle.Text,
                                  TAuthor.Text,
                                  TPublication.Text,
                                  ISBN,
                                  TGenre.Text,
                                  Years,
                                  status);

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

                TTitle.Text = "";
                TAuthor.Text = "";
                TPublication.Text = "";
                TISBN.Text = "";
                TGenre.Text = "";
                TYearsOfRelease.Text = "";
                ComboBox.SelectedIndex = -1;
            }
            else if (DataGrid.SelectedIndex < books.GetData().Count())
            {
                var role = (BooksRow)(DataGrid.SelectedItem as DataRowView).Row;

                TTitle.Text = role.Title;
                TAuthor.Text = role.Author;
                TPublication.Text = role.Publication;
                TISBN.Text = role.ISBN.Split('-')[1];
                TGenre.Text = role.Genre;
                TYearsOfRelease.Text = role.YearOfRelease.ToString();

                ComboBox.SelectedIndex = role.Status == "Доступно" ? 0 : 1;

                BUpdate.IsEnabled = true;
                BDelete.IsEnabled = true;
            }
        }

        private void UpdateInfo()
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

        private void TISBN_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
