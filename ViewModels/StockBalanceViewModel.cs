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
        private Models.StockBalance? _selectedStockBalance;

        public Store? SelectedStore
        {
            get => _selectedStore; 
            set  {
                _selectedStore = value;
                RaisePropertyChanged();
                LoadStockBalance();
                RaisePropertyChanged(nameof(StockBalanceInStore));
            }
        }

        public Models.StockBalance? SelectedStockBalance
        {
            get => _selectedStockBalance;
            set
            {
                _selectedStockBalance = value;
                RaisePropertyChanged();
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
                     .Include(sb => sb.BookIsbn13Navigation)
                    .Where(sb => sb.StoreId == _selectedStore.Id)
                    .ToList()
                );
            RaisePropertyChanged(nameof(StockBalanceInStore));

        }

        public void AddBookToStock(string bookIsbn13, int count  )
        {
            if (_selectedStore == null) return;
            using var db = new BookStoreContext();

            var existingStock = db.StockBalances
                .FirstOrDefault(sb => sb.StoreId == _selectedStore.Id && sb.BookIsbn13 == bookIsbn13);

            if (existingStock != null)
            {
                existingStock.Count = (existingStock.Count ?? 0) + count;
            }

            else
            {
                var newStockBalance = new Models.StockBalance()
                {
                    StoreId = _selectedStore.Id,
                    BookIsbn13 = bookIsbn13,
                    Count = count
                };
                db.StockBalances.Add(newStockBalance);
            }
            db.SaveChanges();
            LoadStockBalance();

        }

        public bool DeleteSelectedStock()
        {
            if (_selectedStore == null || _selectedStockBalance == null) 
                return false;
            using var db = new BookStoreContext();

            var stockToRemove = db.StockBalances
                .FirstOrDefault(sb => sb.StoreId == _selectedStockBalance.StoreId && sb.BookIsbn13 == _selectedStockBalance.BookIsbn13);

            if (stockToRemove != null)
            {
                db.StockBalances.Remove(stockToRemove);
                db.SaveChanges();
                LoadStockBalance();
                SelectedStockBalance = null;
                return true;
            }
            return false;
        }


    }
}
