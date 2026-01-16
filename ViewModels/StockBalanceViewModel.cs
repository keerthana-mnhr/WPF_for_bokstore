using Microsoft.EntityFrameworkCore;
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

        public ObservableCollection<Models.StockBalance> StockBalanceInStore { get; set; }

        private Store? _selectedStore;

        public Store? SelectedStore
        {
            get => _selectedStore; 
            set  {
                _selectedStore = value;
                RaisePropertyChanged();
                LoadStockBalance();
                RaisePropertyChanged("StockBalanceInStore");
            }
        }

        public StockBalanceViewModel()
        {
            LoadData();
            LoadStockBalance();
        }
        private void LoadData()
        {
            using var db = new BookStoreContext();

            Stores = new ObservableCollection<Store>(
                db.Stores.Distinct().ToList()
                );
            SelectedStore = Stores.FirstOrDefault(); 
        }

        private void LoadStockBalance()
        {
            using var db = new BookStoreContext();

         

            StockBalanceInStore = new ObservableCollection<Models.StockBalance>
                (
              db.StockBalances
                    .Where(sb => sb.StoreId == _selectedStore.Id)
                    .ToList()
                );

        }

    }
}
