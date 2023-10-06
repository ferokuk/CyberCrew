using System;
using System.Collections.Generic;
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
    class DB
    {
        public static CyberCrewEntities modelOdb;
    }
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AppFrame.frameMain = FrmMain;
            DB.modelOdb = new CyberCrewEntities();
            FrmMain.Navigate(new LoginPage());
            
        }

        
        
    }
}
