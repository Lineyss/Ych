using System.Collections.Generic;
using WpfApp1.Views.DialogWindow;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Text;
using System;

namespace WpfApp1.Models
{
    internal class ModelDialog
    {
        private static object view;

        private static void DialogWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                var win = (Window)view;
                win.IsEnabled = true;
            }
            catch
            {
                var page = (Page)view;
                page.IsEnabled = true;
            }
        }

        public static void ShowDialog(object views, string message)
        {
            dialogWindow dialogWindow = new dialogWindow();
            dialogWindow.Closed += DialogWindow_Closed;
            dialogWindow.Text.Text = message;
            dialogWindow.Show();

            try
            {
                var win = (Window)views;
                view = win;
                win.IsEnabled = false;
            }
            catch
            {
                var page = (Page)views;
                view = page;
                page.IsEnabled = false;
            }
        }
    }
}
