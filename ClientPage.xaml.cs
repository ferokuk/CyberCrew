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
    
    public partial class ClientPage : Page
    {
        DB.Client User;
        
        Button currBtn;
        public ClientPage(DB.Client user)
        {
            InitializeComponent();
            this.User = user;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            DispatcherTimer balance = new DispatcherTimer();
            balance.Tick += new EventHandler(UpdateBalance_Tick);
            balance.Interval = new TimeSpan(0, 1, 0);
            balance.Start();
            Nickname.Text = user.Nickname;
            Balance.Text = user.Balance.ToString() + "₽";
            currBtn = AppsBtn;
            PageContent.Navigate(new ClientAppsPage(user));
           
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString();
            
        }
        private void UpdateBalance_Tick(object sender, EventArgs e)
        {
            Balance.Text = DBConnection.modelOdb.Client.FirstOrDefault(x => x.Nickname == User.Nickname).Balance.ToString() + "₽";

        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new LoginPage());
        }

       

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            if(currBtn == Profile) { return; }
            Profile.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = Profile;
            PageContent.Navigate(new ClientProfilePage(User));
            //AppFrame.frameMain.Navigate(new ClientProfilePage(User));
        }

        private void AppsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currBtn == AppsBtn) { return; }
            AppsBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = AppsBtn;
            PageContent.Navigate(new ClientAppsPage(User));
        }
    }
}
