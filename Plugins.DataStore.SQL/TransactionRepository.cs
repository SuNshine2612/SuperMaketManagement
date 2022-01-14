using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MarketContext marketContext;

        public TransactionRepository(MarketContext marketContext)
        {
            this.marketContext = marketContext;
        }
        public IEnumerable<Transaction> Get(string cashierName)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return marketContext.Transactions.ToList();
            else
                return marketContext.Transactions.Where(x => string.Equals(x.CashierName, cashierName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Transaction> GetByDay(string cashierName, DateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return marketContext.Transactions.Where(x => x.TimeStamp.Date == dateTime.Date);
            else
                return marketContext.Transactions.Where(x =>
                    //(x.CashierName.ToLower() == cashierName.ToLower())
                    EF.Functions.Like(x.CashierName, $"%{cashierName}%")
                    && (x.TimeStamp.Date == dateTime.Date)
                );
        }

        public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            Transaction transaction = new()
            {
                ProductId = productId,
                CashierName = cashierName,
                ProductName = productName,
                Price = price,
                BeforeQty = beforeQty,
                SoldQty = soldQty,
                TimeStamp = DateTime.Now
            };

            marketContext.Transactions.Add(transaction);
            marketContext.SaveChanges();
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return marketContext.Transactions.Where(x => x.TimeStamp.Date >= fromDate.Date && x.TimeStamp.Date <= toDate.Date.AddDays(1).Date);
            else
                return marketContext.Transactions.Where(x =>
                    //(x.CashierName.ToLower() == cashierName.ToLower())
                    EF.Functions.Like(x.CashierName, $"%{cashierName}%")
                    && (x.TimeStamp.Date >= fromDate.Date)
                    && (x.TimeStamp.Date <= toDate.Date.AddDays(1).Date)
                );
        }
    }
}
