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
using WPF_for_bokstore.Models;
using WPF_for_bokstore.ViewModels;

namespace WPF_for_bokstore
{
    /// <summary>
    /// Interaction logic for StockBalance.xaml
    /// </summary>
    public partial class StockBalance : Page
    {
        private StockBalanceViewModel _viewModel;
        public StockBalance()
        {
            InitializeComponent();
            _viewModel = new StockBalanceViewModel();
            DataContext = _viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedStore == null)
            {
                MessageBox.Show("Select a store", "No store Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();

            if (addBookWindow.IsSaved && addBookWindow.SelectedBook != null)
            {
                _viewModel.AddBookToStock(addBookWindow.SelectedBook.Isbn13, addBookWindow.BookCount);
                MessageBox.Show("Added Successfully");
            }

        }
    }
}
