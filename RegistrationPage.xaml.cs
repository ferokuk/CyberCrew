using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        HashSet<string> AllLogins = new HashSet<string>();
        HashSet<string> AllEmails = new HashSet<string>();
        private string badEmoji = "❌ ";
        private string goodEmoji = "✅ ";
        

        public RegistrationPage()
        {
            InitializeComponent();
            AllLogins = DBConnection.modelOdb.Client.Select(x => x.Nickname).ToHashSet();
            AllEmails = DBConnection.modelOdb.Client.Select(x => x.Email).ToHashSet();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new LoginPage());
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regex = new Regex("^[a-zA-Z]\\w*$");
            if (!regex.IsMatch(Login.Text))
            {
                Register.IsEnabled = false;
                LoginMessage.Text = badEmoji + "введите корректный логин";
                LoginMessage.Foreground = Brushes.Red;
                return;
            }
            if (4 > Login.Text.Length)
            {
                Register.IsEnabled = false;
                LoginMessage.Text = badEmoji + "длина логина должна быть > 3 символов";
                LoginMessage.Foreground = Brushes.Red;
                return;
            }
            LoginMessage.Text = "⏳ проверка логина...";
            LoginMessage.Foreground = Brushes.Blue;

            if (AllLogins.Contains(Login.Text))
            {
                
                Register.IsEnabled = false;
                LoginMessage.Text = badEmoji + "логин уже занят";
                LoginMessage.Foreground = Brushes.Red;
            }
            else
            {
                LoginMessage.Text = goodEmoji + "логин доступен";
                LoginMessage.Foreground = Brushes.Green;

            }
            Register.IsEnabled = IsFormValid();

        }

        private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepeatPassword.Password != Password.Password && RepeatPassword.Password.Trim() != "")
            {
                PasswordMessage.Text = badEmoji + "пароли не совпадают";
                PasswordMessage.Foreground = Brushes.Red;
            }
            else
            {
                PasswordMessage.Text = ""; ;
            }
            Register.IsEnabled = IsFormValid();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var regex = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])(?!.*\\s)[a-zA-Z0-9#$@!%&*?]{8,}$");
            if (!regex.IsMatch(Password.Password))
            {
                Register.IsEnabled = false;
                PasswordMessage.Text = badEmoji + "введите корректный пароль";
                PasswordMessage.Foreground = Brushes.Red;
                return;
            }

            if (RepeatPassword.Password != Password.Password && RepeatPassword.Password.Trim() != "")
            {
                PasswordMessage.Text = badEmoji + "пароли не совпадают";
                PasswordMessage.Foreground = Brushes.Red;
            }
            else
            {
                PasswordMessage.Text = ""; ;
            }
            
            Register.IsEnabled = IsFormValid();
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (!regex.IsMatch(Email.Text))
            {
                EmailMessage.Text = badEmoji + "введите правильную почту";
                EmailMessage.Foreground = Brushes.Red;
                Register.IsEnabled = false;
                return;
            }
            EmailMessage.Text = "⏳ проверка почты...";
            EmailMessage.Foreground = Brushes.Blue;
            if (AllEmails.Contains(Email.Text))
            {
                EmailMessage.Text = badEmoji + "почта уже занята";
                EmailMessage.Foreground = Brushes.Red;
               
                Register.IsEnabled = false;

            }
            else
            {
                EmailMessage.Text = goodEmoji + "почта доступна";
                EmailMessage.Foreground = Brushes.Green;

            }
            Register.IsEnabled = IsFormValid();
        }
    
        async private void Register_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Login: {Login.Text}{Login.Text.Length}\nEmail: {Email.Text} {Email.Text.Length}\nPassword: {Password.Password} {Password.Password.Length}\nRepeatPassword: {RepeatPassword.Password} {RepeatPassword.Password.Length}\n");
            var hashedPassword = Hash.HashPassword(Password.Password);
           
            DB.Client newClient = new DB.Client()
            {
                Nickname = Login.Text.Trim(),
                Email = Email.Text.Trim(),
                HashedPassword = hashedPassword,
                IsEmailConfirmed = false,
                RegisteredAt = DateTime.Now,
                Balance = 0

            };
            try
            {
                if(DBConnection.modelOdb.Client.FirstOrDefault(x => x.Nickname == newClient.Nickname) != null)
                {
                    RegisterMessage.Text = badEmoji + "данный логин уже занят.";
                    RegisterMessage.Foreground = Brushes.Red;
                    return;
                }
                DBConnection.modelOdb.Client.Add(newClient);
                DBConnection.modelOdb.SaveChanges();
                RegisterMessage.Text = "Вы успешно зарегистрировались!\n Переход на страницу входа...";
                RegisterMessage.Foreground = Brushes.Green;
                await Task.Delay(2000);
                AppFrame.frameMain.Navigate(new LoginPage());

            }
            catch (Exception ex)
            {
                RegisterMessage.Text = badEmoji + "произошла ошибка.\nПовторите попвытку позже.";
                RegisterMessage.Foreground = Brushes.Red;
            }
            

        }
        
        
        
        private bool IsFormValid()
        {
            var email = Email.Text.Trim();
            var login = Login.Text.Trim();
            var pass = Password.Password.Trim();
            var passRepeat = RepeatPassword.Password.Trim();
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(email) && // valid email
                 new Regex("^[a-zA-Z]\\w*$").IsMatch(login) && // valid login
                 new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])(?!.*\\s)[a-zA-Z0-9#$@!%&*?]{8,}$").IsMatch(pass) && // valid password
                 pass == passRepeat && 
                 !AllEmails.Contains(email) && !AllLogins.Contains(login);
        }
    }
}
