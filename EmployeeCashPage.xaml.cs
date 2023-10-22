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
    /// Логика взаимодействия для EmployeeCashPage.xaml
    /// </summary>
    public partial class EmployeeCashPage : Page
    {
        DB.Employee employee;
        public EmployeeCashPage(DB.Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            MoneyDate.SelectedDate = DateTime.Today;
            
        }
        private void CalculateMoney(DateTime? date)
        {
            
            var moneyIncome = DBConnection.modelOdb.MoneyIncome.ToList();
            var employees = DBConnection.modelOdb.Employee.ToList();
            var incomeSource = DBConnection.modelOdb.IncomeSource.ToList();
            var clients = DBConnection.modelOdb.Client.ToList();
            var res = from money in moneyIncome
                      join emp in employees on money.EmployeeId equals emp.EmployeeId
                      join client in clients on money.ClientId equals client.ClientId
                      join source in incomeSource on money.IncomeSourceId equals source.IncomeSourceId
                      where money.IncomeDateTime.Date == date.Value
                      orderby money.IncomeDateTime descending
                      select new
                      {
                          Date = money.IncomeDateTime,
                          Employee = ShortName.ShortenName(emp),
                          Client = client.Nickname,
                          Source = source.SourceName,
                          Money = money.MoneyAmount
                      };
            Total.Text = "Всего: " + res.Sum(x => x.Money).ToString() + "₽";
            IncomeGrid.ItemsSource = res;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateMoney(MoneyDate.SelectedDate);
        }
    }
}
