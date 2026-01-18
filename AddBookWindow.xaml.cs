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
using WPF_for_bokstore.Models;

namespace WPF_for_bokstore
{
    
    public partial class AddBookWindow : Window
    {
        public Book? SelectedBook { get; private set; }
        public int BookCount { get; private set; }
        public bool IsSaved { get; private set; } = false;
        public AddBookWindow()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            using var db = new BookStoreContext();
            var books = db.Books.ToList();
            BooksDataGrid.ItemsSource = books;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                if (int.TryParse(CountTextBox.Text, out int count) && count > 0)
                {
                    SelectedBook = selectedBook;
                    BookCount = count;
                    IsSaved = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid Input, Enter a valid value greater than 0","Validation Error",MessageBoxButton.OK , MessageBoxImage.Warning);
                }
            }
           

        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e)
        {
            IsSaved = false;
            Close();
        }
    }
}
