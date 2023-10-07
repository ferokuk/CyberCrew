using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using Newtonsoft.Json;
using System.IO;
using ControlzEx.Standard;
using System.Data.Entity.Migrations;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для ClientProfilePage.xaml
    /// </summary>
    public partial class ClientProfilePage : Page
    {
        DB.Client user;
        string code;
        public ClientProfilePage(DB.Client user)
        {

            InitializeComponent();
            this.DataContext = user;
            this.user = user;
            RegistrationDate.Text = "В CyberCrew с: " + user.RegisteredAt.ToLocalTime().ToString();
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
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка!" + ex);
                return;
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
            if(Code.Text != code)
            {
                CodeEnterInfo.Text = "неверный код";
                CodeEnterInfo.Foreground = Brushes.Red;
                return;
            }
            user.IsEmailConfirmed = true;
            DBConnection.modelOdb.Client.AddOrUpdate(user);
            DBConnection.modelOdb.SaveChanges();
            CodeEnterInfo.Text = "успешно";
            CodeEnterInfo.Foreground = Brushes.Green;
            await Task.Delay(2000);
            CodeEnterInfo.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            ConfirmCode.Visibility = Visibility.Hidden;
            ConfirmEmail.Visibility = Visibility.Hidden;
            CodeEnterInfo.Visibility = Visibility.Hidden;
            IsEmailConfirmed.Text = "подтверждён";
            IsEmailConfirmed.Foreground = Brushes.Green;
        }
    }
}
