using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace CyberCrew
{
    partial class DialogWindow : MetroWindow
    {

        public DialogWindow(string message)
        {
            InitializeComponent();
            Message.Text = message;
        }

        

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }

        private void ok_MouseEnter(object sender, MouseEventArgs e)
        {
            OK.Background = Brushes.DarkGreen;
        }
        private void ok_MouseLeave(object sender, MouseEventArgs e)
        {
            OK.Background = Brushes.Green;
        }
        private void no_MouseEnter(object sender, MouseEventArgs e)
        {
            NO.Background = (Brush)new BrushConverter().ConvertFromString("#760E08");
        }
        private void no_MouseLeave(object sender, MouseEventArgs e)
        {
            NO.Background = Brushes.DarkRed;
        }
    }
}
