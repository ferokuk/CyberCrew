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
using System.Windows.Threading;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class AppsPage : Page
    {
        DB.Client User;
       
        public AppsPage(DB.Client user)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();

            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            this.User = user;
            Nickname.Text = user.Nickname;
            Balance.Text = user.Balance.ToString() + "₽";
            Apps.ItemsSource = DBConnection.modelOdb.Software.ToList();
           
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new LoginPage());
        }

        private void Apps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var app = Apps.SelectedItem as DB.Software;
            MessageBox.Show(app.PathToExe);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Navigate(new ClientProfilePage(User));
            //AppFrame.frameMain.Navigate(new ClientProfilePage(User));
        }
    }
}
