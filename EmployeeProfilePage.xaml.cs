using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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
    /// Логика взаимодействия для EmployeeProfilePage.xaml
    /// </summary>

    public partial class EmployeeProfilePage : Page
    {
        DB.Employee employee;
        Frame prevFrame;
        public EmployeeProfilePage(DB.Employee employee, Frame prevFrame)
        {
            InitializeComponent();

            this.employee = employee;
            this.prevFrame = prevFrame;
            Positions.ItemsSource = DBConnection.modelOdb.Position.Select(x => x.Name).ToList();
            Positions.SelectedIndex = employee.PositionId - 1;

            Positions.IsEnabled = employee.Position.Name != "admin";
            IsWorking.IsEnabled = employee.Position.Name != "admin";
            SaveEmployee.Click += SaveEmployee_Click;

            DataContext = this.employee;

            if (employee.IsWorking)
            {
                IsWorking.Text = "работает";
                IsWorking.Foreground = Brushes.Green;
            }
            else
            {
                IsWorking.Text = "уволен";
                IsWorking.Foreground = Brushes.Red;
            }

            female.IsChecked = employee.Gender == "f" ? true : false;
            male.IsChecked = employee.Gender == "m" ? true : false;

        }
        public EmployeeProfilePage(Frame prevFrame)
        {
            InitializeComponent();

            SaveEmployee.Click += AddEmployee_Click;
            this.prevFrame = prevFrame;
            Positions.ItemsSource = DBConnection.modelOdb.Position.Select(x => x.Name).Where(Name => Name != "admin").ToList();
            Positions.SelectedIndex = 1;
            EmploymentDate.Text = DateTime.Now.Date.ToString();
            ChangePassword.Visibility = Visibility.Hidden;
            IsWorking.Visibility = Visibility.Hidden;
            WorkStatus.Visibility = Visibility.Hidden;
            male.IsChecked = true;

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
            prevFrame.Navigate(new EmployeeEmployeesPage());
        }

        private void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            employee.Gender = (bool)female.IsChecked ? "f" : "m";
            employee.DateOfBirth = (DateTime)DateOfBirth.SelectedDate;
            employee.EmploymentDate = (DateTime)EmploymentDate.SelectedDate;
            employee.PositionId = Positions.SelectedIndex + 1;

            try
            {
                DBConnection.modelOdb.Employee.AddOrUpdate(employee);
                DBConnection.modelOdb.SaveChanges();
                prevFrame.Navigate(new EmployeeEmployeesPage());
            }
            catch { new MessageWindow("Произошла ошибка.").ShowDialog(); }

        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumberError.Text = INNError.Text = DateOfBirthError.Text = "";
            if (!new Regex("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$").IsMatch(PhoneNumber.Text))
            {
                PhoneNumberError.Text = "неверный формат номера телефона";

            }
            if (!Int64.TryParse(INN.Text, out _) || INN.Text.Length < 12)
            {
                INNError.Text = "неверный формат ИНН";


            }
            if (!DateOfBirth.SelectedDate.HasValue || DateTime.Now.Year - DateOfBirth.SelectedDate.Value.Year < 14)
            {
                DateOfBirthError.Text = "неверная дата";

            }
            if (PhoneNumberError.Text.Length + INNError.Text.Length + DateOfBirthError.Text.Length > 0 || Surname.Text.Trim().Length == 0 || FirstName.Text.Trim().Length == 0)
            {
                return;
            }
            var newEmployee = new DB.Employee()
            {
                FirstName = FirstName.Text.Trim(),
                Surname = Surname.Text.Trim(),
                Patronymic = Patronymic.Text.Trim(),
                INN = INN.Text.Trim(),
                DateOfBirth = (DateTime)DateOfBirth.SelectedDate,
                EmploymentDate = (DateTime)EmploymentDate.SelectedDate,
                IsWorking = true,
                Gender = (bool)male.IsChecked ? "m" : "f",
                PhoneNumber = PhoneNumber.Text.Trim(),
                PositionId = Positions.SelectedIndex + 2
            };
            try
            {
                DBConnection.modelOdb.Employee.AddOrUpdate(newEmployee);
                DBConnection.modelOdb.SaveChanges();
                prevFrame.Navigate(new EmployeeEmployeesPage());
            }
            catch { new MessageWindow("Произошла ошибка.").Show(); }

        }

        private void IsWorking_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            employee.IsWorking = !employee.IsWorking;
            if (employee.IsWorking)
            {
                IsWorking.Text = "работает";
                IsWorking.Foreground = Brushes.Green;
            }
            else
            {
                IsWorking.Text = "уволен";
                IsWorking.Foreground = Brushes.Red;
            }
        }

        
    }

}
