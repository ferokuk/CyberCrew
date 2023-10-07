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
                    return stringValue.Substring(0, 8).Trim()+"...";
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
        List<string> runningApps = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();
        private string DirPath = "A:\\Clones\\CyberCrew\\CyberCrew";
        public ClientAppsPage(DB.Client User)
        {
            InitializeComponent();
            Apps.ItemsSource = DBConnection.modelOdb.Software.ToList();
            this.User = User;
        }
        private void CheckProcesses()
        {
            var localProcesses = Process.GetProcesses().Select(x => x.ProcessName.ToLower()).ToHashSet();
            List<string> currentApps = runningApps.ToList();
            foreach(string app in currentApps)
            {
                if(!localProcesses.Contains(app))
                {
                    runningApps.Remove(app);
                }
                 
            }
            

        }
        private void ReduceUserBalance_Tick(object sender, EventArgs e)
        {
            CheckProcesses();
            if(runningApps.Count == 0)
            {
                timer.Stop();
                return;
            }
            if (User.Balance <= 2)
            {
                User.Balance = 0;
                DBConnection.modelOdb.Client.AddOrUpdate(User);
                DBConnection.modelOdb.SaveChanges();
                timer.Stop();
                foreach(var app in runningApps)
                {
                    foreach (var process in Process.GetProcessesByName(app))
                    {
                        try
                        {
                            process.Kill();

                        }
                        catch
                        {
                            MessageBox.Show($"error killing {app}-{process}");
                        }
                    }
                }
                return;
            }
            
            try
            {
            User.Balance -= Convert.ToDecimal(2);
            DBConnection.modelOdb.Client.AddOrUpdate(User);
            DBConnection.modelOdb.SaveChanges();

            }
            catch(Exception ex)
            {
                User.Balance += Convert.ToDecimal(2);
                MessageBox.Show(ex.Message);
            }
        }
        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var app = Apps.SelectedItem as DB.Software;
            runningApps.Add(app.ApplicationName.ToLower());
            if(timer.IsEnabled)
            {
                Process.Start(DirPath + app.PathToExe);
                return;
            }
            timer.Tick += new EventHandler(ReduceUserBalance_Tick);
            timer.Interval = new TimeSpan(0, 0, 5);
            try
            {
                Process.Start(DirPath + app.PathToExe);

                timer.Start();

            }
            catch
            {
                timer.Stop();
                MessageBox.Show("Отсутствует исполняемый файл");
            }
        }
    }
}
