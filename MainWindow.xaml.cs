using System;
using System.Collections.Generic;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

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
    
    class DBConnection
    {
        public static DB.CyberCrewEntities modelOdb;
    }
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AppFrame.frameMain = FrmMain;
            DBConnection.modelOdb = new DB.CyberCrewEntities();
            FrmMain.Navigate(new LoginPage());
            
        }

        
        
    }
}
