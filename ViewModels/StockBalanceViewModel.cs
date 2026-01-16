using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_for_bokstore.Models;
using WPF_for_bokstore.ViewModels;
    
namespace WPF_for_bokstore.ViewModels
{
    public class StockBalanceViewModel:ViewModelBase, INotifyPropertyChanged
    {
        public ObservableCollection<Store> Stores { get; private set; }
        public Store SelectedStore { get; set; }
        public StockBalanceViewModel()
        {
            LoadData();
        }
        private void LoadData()
        {
            using var db = new BookStoreContext();

            Stores = new ObservableCollection<Store>(
                db.Stores.Distinct().ToList()
                );
        }

    }
}
