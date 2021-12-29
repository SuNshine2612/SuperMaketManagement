using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases
{
    public class GetTransactionUseCase : IGetTransactionUseCase
    {
        private readonly ITransactionRepository transactionRepository;

        public GetTransactionUseCase(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public IEnumerable<Transaction> Execute(string cashierName, DateTime from, DateTime to)
        {
            return transactionRepository.Search(cashierName, from, to);
        }
    }
}
