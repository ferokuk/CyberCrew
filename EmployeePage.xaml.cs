using CyberCrew.DB;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        Button currBtn;
        DB.Employee employee;
        public EmployeePage(DB.Employee employee)
        {
            InitializeComponent();
            // if not admin
            if (employee.PositionId != 1)
            {
                EmployeesBtn.Visibility = Visibility.Hidden;
            }
            this.employee = employee;
            currBtn = ClientsBtn;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            CurrentPageContent.Navigate(new EmployeeClientsPage(employee));
            Name.Text = ShortName.ShortenName(employee);
            Position.Text = employee.Position.Name.ToUpper();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString();
            CurrentTimeShift.Text = DateTime.Now.Hour >= 20 && DateTime.Now.Hour < 8 ? "Ночная смена" : "Дневная смена";
        }


        private void ClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currBtn == ClientsBtn) { return; }
            ClientsBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = ClientsBtn;
            CurrentPageContent.Navigate(new EmployeeClientsPage(employee));
        }
        private void BarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currBtn == BarBtn) { return; }
            BarBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = BarBtn;
            CurrentPageContent.Navigate(new BarPage());
        }
        private void EmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currBtn == EmployeesBtn) { return; }
            EmployeesBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = EmployeesBtn;
            CurrentPageContent.Navigate(new EmployeeEmployeesPage());
        }

        private void CurrentPageContent_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
       && (Keyboard.IsKeyDown(Key.Left)))
            {
                e.Handled = true;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new LoginPage());
        }

        private void CashBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currBtn == CashBtn) { return; }
            CashBtn.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF8A00");
            currBtn.Background = Brushes.Transparent;
            currBtn = CashBtn;
            CurrentPageContent.Navigate(new EmployeeCashPage(employee));
        }
    }
}
