using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
<<<<<<< Updated upstream

=======
    /// 
    
>>>>>>> Stashed changes
    class AppFrame
    {
        public static Frame frameMain;
    }
    public static class Hash
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    public static class ShortName
    {
        public static string ShortenName(DB.Employee employee)
        {
            string res = $"{employee.Surname} {employee.FirstName.Substring(0, 1)}.";
            if (!String.IsNullOrEmpty(employee.Patronymic))
            {
                res += employee.Patronymic.Substring(0, 1) + ".";
            }
            return res;
        }
    }
    class DBConnection
    {
        public static DB.CyberCrewEntities modelOdb;
    }
    public partial class MainWindow
    {

    public class Config
    {

        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpAddress { get; set; }
        public int SmtpPortNumber { get; set; }
        public string OrganizationName { get; set; }

    }

    public class AppsManager
    {
        public static DB.Client User;
        public static DispatcherTimer timer = new DispatcherTimer();
        public static List<string> runningApps;
        public static void KillRunningApps()
        {
            foreach (var app in runningApps)
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
            runningApps.Clear();
        }
        private static void CheckProcesses()
        {
            var localProcesses = Process.GetProcesses().Select(x => x.ProcessName.ToLower()).ToHashSet();
            List<string> currentApps = runningApps.ToList();
            foreach (string app in currentApps)
            {
                if (!localProcesses.Contains(app))
                {
                    runningApps.Remove(app);
                }

            }


        }
        public static void ReduceUserBalance_Tick(object sender, EventArgs e)
        {
            CheckProcesses();
            if (runningApps.Count == 0 || User.Balance == 0)
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
                KillRunningApps();
                return;
            }

            try
            {
                User.Balance -= Convert.ToDecimal(1);
                DBConnection.modelOdb.Client.AddOrUpdate(User);
                DBConnection.modelOdb.SaveChanges();

            }
            catch (Exception ex)
            {
                User.Balance += Convert.ToDecimal(1);
                MessageBox.Show(ex.Message);
            }

        }
    }
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            AppFrame.frameMain = FrmMain;

            DBConnection.modelOdb = new DB.CyberCrewEntities();

            AppsManager.runningApps = new List<string>();
            AppsManager.timer.Tick += new EventHandler(AppsManager.ReduceUserBalance_Tick);
            AppsManager.timer.Interval = new TimeSpan(0, 0, 1);

            LanguageProperty.OverrideMetadata(typeof(FrameworkElement),new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            FrmMain.Navigate(new LoginPage());

        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
        && (Keyboard.IsKeyDown(Key.Left)))
            {
                e.Handled = true;
            }
        }
    }
}
