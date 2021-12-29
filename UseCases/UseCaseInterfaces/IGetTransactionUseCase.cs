using CoreBusiness;
using System;
using System.Collections.Generic;

namespace UseCases
{
    public interface IGetTransactionUseCase
    {
        IEnumerable<Transaction> Execute(string cashierName, DateTime from, DateTime to);
    }
}