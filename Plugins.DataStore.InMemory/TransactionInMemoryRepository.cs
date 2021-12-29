using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class TransactionInMemoryRepository : ITransactionRepository
    {
        readonly List<Transaction> transactions;
        public TransactionInMemoryRepository()
        {
            transactions = new List<Transaction>();
        }

        public IEnumerable<Transaction> Get(string cashierName)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return transactions;
            else
                return transactions.Where(x => string.Equals(x.CashierName, cashierName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Transaction> GetByDay(string cashierName, DateTime dateTime)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return transactions.Where(x => x.TimeStamp.Date == dateTime.Date);
            else
                return transactions.Where(x => 
                    string.Equals(x.CashierName, cashierName, StringComparison.OrdinalIgnoreCase)
                    && (x.TimeStamp.Date == dateTime.Date)
                );
        }

        public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
        {
            int transactionId = 0;

            if (transactions is not null && transactions.Count > 0)
            {
                int maxId = transactions.Max(x => x.TransactionId);
                transactionId = maxId++;
            }
            else
            {
                transactionId = 1;
            }

            transactions.Add(new Transaction
            {
                TransactionId = transactionId,
                ProductId = productId,
                ProductName = productName,
                Price = price,
                SoldQty = soldQty,
                BeforeQty = beforeQty,
                TimeStamp = DateTime.Now,
                CashierName = cashierName
            });
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime fromDate, DateTime toDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return transactions.Where(x => x.TimeStamp.Date >= fromDate.Date && x.TimeStamp.Date <= toDate.Date.AddDays(1).Date);
            else
                return transactions.Where(x =>
                    string.Equals(x.CashierName, cashierName, StringComparison.OrdinalIgnoreCase)
                    && (x.TimeStamp.Date >= fromDate.Date ) 
                    && (x.TimeStamp.Date <= toDate.Date.AddDays(1).Date)
                );
        }
    }
}
