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
using WPF_for_bokstore.ViewModels;

namespace WPF_for_bokstore
{
    /// <summary>
    /// Interaction logic for StockBalance.xaml
    /// </summary>
    public partial class StockBalance : Page
    {
        public StockBalance()
        {
            InitializeComponent();
            DataContext = new StockBalanceViewModel();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
