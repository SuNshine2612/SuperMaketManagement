﻿using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.DataStorePluginInterfaces
{
    public interface ITransactionRepository
    {
        public IEnumerable<Transaction> GetByDay(string cashierName, DateTime dateTime);
        public IEnumerable<Transaction> Search(string cashierName, DateTime fromDate, DateTime toDate);
        public IEnumerable<Transaction> Get(string cashierName);
        public void Save(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
    }
}
