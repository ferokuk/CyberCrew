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
using System.Windows.Shapes;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для ClientProfilePage.xaml
    /// </summary>
    public partial class ClientProfilePage : Page
    {
        DB.Client user;
        public ClientProfilePage(DB.Client user)
        {
            
            InitializeComponent();
            this.user = user;
            
        }

       

        private void Apps_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AppsPage(user));
        }
    }
}
