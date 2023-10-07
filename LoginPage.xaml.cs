using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        public LoginPage()
        {
            InitializeComponent();
            string jsonFilePath = "../../Resources/config.json"; // Укажите путь к вашему JSON файлу
            string jsonContent = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<Config>(jsonContent);
            LoginBtn.IsEnabled = false;
            Login.Text = config.Login;
            Password.Password = config.Password;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new RegistrationPage());
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex("^[a-zA-Z]\\w*$");
            if (!regex.IsMatch(Login.Text))
            {
                LoginBtn.IsEnabled = false;
                LoginMessage.Text = "введите корректный логин";
                LoginMessage.Foreground = Brushes.Red;
                return;
            }
            else
            {
                LoginMessage.Text = ""; ;
            }
            LoginBtn.IsEnabled = Login.Text.Trim() != "" && Password.Password.Trim() != "";
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var regex = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])(?!.*\\s)[a-zA-Z0-9#$@!%&*?]{8,}$");
            if (!regex.IsMatch(Password.Password))
            {
                LoginBtn.IsEnabled = false;
                PasswordMessage.Text = "введите корректный пароль";
                PasswordMessage.Foreground = Brushes.Red;
                return;
            }
            else
            {
                PasswordMessage.Text = ""; ;
            }
            LoginBtn.IsEnabled = Login.Text.Trim() != "" && Password.Password.Trim() != "";
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var passw = Hash.HashPassword(Password.Password);
            var user = DBConnection.modelOdb.Client.FirstOrDefault(x => x.Nickname == login && x.HashedPassword == passw);
            if (user == null)
            {
                LoginInfo.Text = "Неправильный логин или пароль.";
                LoginInfo.Foreground = Brushes.Red;
                return;
            }
            AppFrame.frameMain.Navigate(new ClientPage(user));
        }
    }
}
