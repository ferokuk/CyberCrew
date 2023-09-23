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

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        HashSet<string> AllClients;
        bool FormIsValid = true;

        public RegistrationPage()
        {
            InitializeComponent();
            AllClients = DB.modelOdb.Client.Select(x => x.Nickname).ToHashSet();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new LoginPage());
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (AllClients.Contains(Login.Text.Trim()))
           {
                FormIsValid = false;
                LoginMessage.Text = "❌ этот логин уже занят";
                LoginMessage.Foreground = Brushes.Red;
           }
            Register.IsEnabled = FormIsValid;

        }
    }
}
