using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
using System.Windows.Threading;

namespace CyberCrew
{
    /// <summary>
    /// Логика взаимодействия для EmployeeClientsPage.xaml
    /// </summary>
    public partial class EmployeeClientsPage : Page
    {
        ObservableCollection<DB.Client> ObColl = new ObservableCollection<DB.Client>();
        DB.Employee employee;
        public EmployeeClientsPage(DB.Employee employee)
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = DBConnection.modelOdb.Client.ToList();
            this.employee = employee;
            ObColl = DBConnection.modelOdb.Client.Local;
            ClientsGrid.Loaded += (sender, e) =>
            {
                HideColumns();
            };

        }
        
        private void MyFilter(object sender, FilterEventArgs e)
        {
            var obj = e.Item as DB.Client;
            if (obj != null)
            {
                if (obj.Nickname.ToLower().Contains(SearchText.Text) || obj.Email.ToLower().Contains(SearchText.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }
        private void ClientsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var client = ClientsGrid.SelectedItem as DB.Client;
            if(client == null) { return; }
            ClientProfileContent.Navigate(new ClientProfilePage(client, employee, ClientProfileContent));
        }
       
        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };

            _itemSourceList.Filter += new FilterEventHandler(MyFilter);

            ICollectionView Itemlist = _itemSourceList.View ;
            ClientsGrid.ItemsSource = Itemlist;
            HideColumns();
        }
        
        private void HideColumns()
        {
            ClientsGrid.Columns[0].Visibility = Visibility.Hidden;
            ClientsGrid.Columns[2].Visibility = Visibility.Hidden;
            ClientsGrid.Columns[4].Visibility = Visibility.Hidden;
            ClientsGrid.Columns[7].Visibility = Visibility.Hidden;
        }
    }
}
