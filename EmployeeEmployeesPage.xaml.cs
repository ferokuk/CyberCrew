using CyberCrew.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для EmployeeEmployeesPage.xaml
    /// </summary>
    public partial class EmployeeEmployeesPage : Page
    {
        ObservableCollection<DB.Employee> ObColl = new ObservableCollection<DB.Employee>();
        public EmployeeEmployeesPage()
        {
            InitializeComponent();
            EmployeesGrid.ItemsSource = DBConnection.modelOdb.Employee.ToList();
            ObColl = DBConnection.modelOdb.Employee.Local;
            EmployeesGrid.Loaded += (sender, e) =>
            {
                HideColumns();


            };


        }
        private void FilterByText(object sender, FilterEventArgs e)
        {
            var obj = e.Item as DB.Employee;
            if (obj != null)
            {
                if (obj.FirstName.Contains(SearchText.Text) ||
                    obj.Surname.Contains(SearchText.Text) ||
                    obj.Patronymic.Contains(SearchText.Text)
                    )
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };

            _itemSourceList.Filter += new FilterEventHandler(FilterByText);

            ICollectionView Itemlist = _itemSourceList.View;

            EmployeesGrid.ItemsSource = Itemlist;

            HideColumns();

        }

        private void EmployeesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var employee = EmployeesGrid.SelectedItem as DB.Employee;
            if (employee == null) { return; }
            EmployeeProfileContent.Navigate(new EmployeeProfilePage(employee, EmployeeProfileContent));
        }

        private void HideColumns()
        {
            EmployeesGrid.Columns[0].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[4].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[5].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[7].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[8].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[10].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[11].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[12].Visibility = Visibility.Hidden;
            EmployeesGrid.Columns[13].Visibility = Visibility.Hidden;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProfileContent.Navigate(new EmployeeProfilePage(EmployeeProfileContent));
        }
    }
}
