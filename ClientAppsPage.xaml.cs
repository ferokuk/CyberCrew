using CyberCrew.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для ClientAppsPage.xaml
    /// </summary>
    public class StringTruncationConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // Обрезаем строку до первых 8 символов
                if (stringValue.Length > 8)
                {
                    return stringValue.Substring(0, 8).Trim() + "...";
                }
                else
                {
                    return stringValue;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class ClientAppsPage : Page
    {
        DB.Client User;


        private string DirPath = "A:\\Clones\\CyberCrew\\CyberCrew";
        public ClientAppsPage(DB.Client User)
        {
            InitializeComponent();
            AppsManager.User = User;
            var apps = DBConnection.modelOdb.Software.ToList();
            DataContext = apps;
            Apps.ItemsSource = apps;
            this.User = User;
        }


        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var app = Apps.SelectedItem as DB.Software;
            AppsManager.runningApps.Add(app.ApplicationName.ToLower());
            if (AppsManager.timer.IsEnabled)
            {
                try
                {
                    Process.Start(DirPath + app.PathToExe);

                }
                catch
                {
                    MessageBox.Show("Отсутствует исполняемый файл");
                }
                return;
            }


            try
            {
                Process.Start(DirPath + app.PathToExe);

                AppsManager.timer.Start();

            }
            catch
            {
                //timer.Stop();
                MessageBox.Show("Отсутствует исполняемый файл");
            }
        }
    }
}
