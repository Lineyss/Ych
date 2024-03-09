using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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

namespace WpfApp1.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PWriteEmail.xaml
    /// </summary>
    public partial class PWriteEmail : Page
    {
        UsersTableAdapter users = new UsersTableAdapter();
        Frame Frame;
        public PWriteEmail(Frame Frame)
        {
            InitializeComponent();
            this.Frame = Frame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var email = users.GetData().FirstOrDefault(user => user.Email == Email.Text);
            if (email == null)
            {
                ModelDialog.ShowDialog(this, "Аккакунта с такой почтой не существует");
                return;
            }

            Random rnd = new Random();

            int kod = rnd.Next(1000, 6000);

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("ychprak@rambler.ru", "Library");
            // кому отправляем
            MailAddress to = new MailAddress(Email.Text);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Код подтверждения";
            // текст письма
            m.Body = $"<h2>Код подтверждения</h2> \n Код: {kod}";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.rambler.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("ychprak@rambler.ru", "YchPrak123");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(m);
                Frame.Navigate(new PWriteCod(Frame, kod, Email.Text));
            }
            catch
            {
                ModelDialog.ShowDialog(this, "Не удалось отправить код.");
            }

        }
    }
}
