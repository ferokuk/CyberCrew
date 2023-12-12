using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для ClientProfilePage.xaml
    /// </summary>
    public partial class ClientProfilePage : Page
    {
        DB.Client user;
        DB.Employee employee;
        Frame prevPageFrame;
        string code;
        public ClientProfilePage(DB.Client user)
        {

            InitializeComponent();
            this.user = user;
            DataContext = user;
            Plus100.Visibility = Visibility.Hidden;
            Plus500.Visibility = Visibility.Hidden;
            GoBack.Visibility = Visibility.Hidden;
            RegistrationDate.Text = "В CyberCrew с: " + user.RegisteredAt.ToString();
            if (user.IsEmailConfirmed)
            {
                ConfirmEmail.Visibility = Visibility.Hidden;
                IsEmailConfirmed.Text = "подтверждён";
                IsEmailConfirmed.Foreground = Brushes.Green;
            }
            else
            {
                if(!String.IsNullOrEmpty(user.EmailMessageCode))
                {
                    Code.Visibility = Visibility.Visible;
                    CodeMessage.Visibility = Visibility.Visible;
                    ConfirmCode.Visibility = Visibility.Visible;
                    ConfirmEmail.Content = "Отправить код ещё раз";
                }
                IsEmailConfirmed.Text = "не подтверждён";
                IsEmailConfirmed.Foreground = Brushes.Red;
            }
            
        }
        // Для просмотра сотрудником
        public ClientProfilePage(DB.Client user, DB.Employee employee, Frame prevPageFrame)
        {


            InitializeComponent();
            this.DataContext = user;
            this.user = user;
            this.employee = employee;
            this.prevPageFrame = prevPageFrame;
            BalanceAmount.Text = user.Balance.ToString();
            ChangePassword.Content = "Сбросить пароль";
            ChangePassword.Click += ResetPassword_Click;
            ChangePassword.Click -= ChangePassword_Click;
            ConfirmEmail.Visibility = Visibility.Hidden;
            BalanceAmount.Visibility = Visibility.Visible;
            AddBalanceAmount.Visibility = Visibility.Visible;
            RegistrationDate.Text = "В CyberCrew с: " + user.RegisteredAt.ToString();
            if (user.IsEmailConfirmed)
            {
                ConfirmEmail.Visibility = Visibility.Hidden;
                IsEmailConfirmed.Text = "подтверждён";
                IsEmailConfirmed.Foreground = Brushes.Green;
            }
            else
            {
                IsEmailConfirmed.Text = "не подтверждён";
                IsEmailConfirmed.Foreground = Brushes.Red;
            }
        }


        private void Apps_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ClientAppsPage(user));
        }

        private void ConfirmEmail_Click(object sender, RoutedEventArgs e)
        {
            Code.Visibility = Visibility.Visible;
            CodeMessage.Visibility = Visibility.Visible;
            ConfirmCode.Visibility = Visibility.Visible;
            ConfirmEmail.Content = "Отправить код ещё раз";
            code = GenerateCode(6);
            user.EmailMessageCode = Hash.HashPassword(code);
            DBConnection.modelOdb.Client.AddOrUpdate(user);
            DBConnection.modelOdb.SaveChanges();
            SendCodeToEmail(code);
        }
        private void SendCodeToEmail(string code)
        {
            string jsonFilePath = "../../Resources/config.json"; // Укажите путь к вашему JSON файлу
            string jsonContent = File.ReadAllText(jsonFilePath);
            var config = JsonConvert.DeserializeObject<Config>(jsonContent);



            string messageForm = File.ReadAllText("../../Resources/message_form.html");
            var variables = new Dictionary<string, string>
            {
                { "NICKNAME", user.Nickname },
                { "CODE", code }
            };

            // Замените места вида {{НАЗВАНИЕ}} на переменные в строке
            foreach (var variable in variables)
            {
                string placeholder = $"{{{{{variable.Key}}}}}";
                messageForm = messageForm.Replace(placeholder, variable.Value);
            }

            // от кого отправляем
            MailAddress from = new MailAddress(config.Email, config.OrganizationName);
            // кому отправляем
            MailAddress to = new MailAddress(user.Email);
            // создаем объект сообщения
            MailMessage mail = new MailMessage(from, to);
            // тема письма
            mail.Subject = "Подтвердите почту";
            // текст письма
            mail.Body = messageForm;
            // письмо представляет код html
            mail.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient(config.SmtpAddress, config.SmtpPortNumber);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(config.Email, config.EmailPassword);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
            }
            catch
            {
                new MessageWindow("Произошла ошибка, повторите попытку позже.").ShowDialog();
            }
        }
        private string GenerateCode(int length)
        {
            const string allowedCharacters = "ABCDEFGHJKMNPQRSTUVWXYZ23456789"; // Исключены "l", "I", "0", "O"
            Random random = new Random();
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, allowedCharacters.Length);
                code.Append(allowedCharacters[index]);
            }

            return code.ToString();
        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            Code.Text = Code.Text.ToUpper();
            Code.CaretIndex = Code.Text.Length;
        }

        private async void ConfirmCode_Click(object sender, RoutedEventArgs e)
        {
            if (Hash.HashPassword(Code.Text) != user.EmailMessageCode)
            {
                CodeEnterInfo.Text = "неверный код";
                CodeEnterInfo.Foreground = Brushes.Red;
                return;
            }
            user.IsEmailConfirmed = true;
            DBConnection.modelOdb.Client.AddOrUpdate(user);
            DBConnection.modelOdb.SaveChanges();
            new MessageWindow("Вы успешно подтвердили почту").ShowDialog();
            await Task.Delay(2000);
            CodeEnterInfo.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            ConfirmCode.Visibility = Visibility.Hidden;
            ConfirmEmail.Visibility = Visibility.Hidden;
            IsEmailConfirmed.Text = "подтверждён";
            IsEmailConfirmed.Foreground = Brushes.Green;
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Смена пароля!");
        }
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сброс пароля!");
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            prevPageFrame.Navigate(new EmployeeClientsPage(employee));
        }

        private void AddBalanceAmount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal newValue = Decimal.Parse(BalanceAmount.Text);
                decimal oldValue = user.Balance;

                user.Balance = newValue;

                DB.MoneyIncome moneyIncome = new DB.MoneyIncome()
                {
                    IncomeDateTime = DateTime.Now,
                    // пополнение
                    IncomeSourceId = 1,
                    MoneyAmount = newValue - oldValue,
                    EmployeeId = employee.EmployeeId,
                    ClientId = user.ClientId
                };
                DBConnection.modelOdb.MoneyIncome.AddOrUpdate(moneyIncome);
                DBConnection.modelOdb.Client.AddOrUpdate(user);
                DBConnection.modelOdb.SaveChanges();
                prevPageFrame.Navigate(new EmployeeClientsPage(employee));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Plus100_Click(object sender, RoutedEventArgs e)
        {
            BalanceAmount.Text = (Decimal.Parse(BalanceAmount.Text) + 100).ToString();
        }
        private void Plus500_Click(object sender, RoutedEventArgs e)
        {
            BalanceAmount.Text = (Decimal.Parse(BalanceAmount.Text) + 500).ToString();
        }

        private void BalanceAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = new Regex("[^0-9,]+").IsMatch(e.Text);

        }
    }
}
