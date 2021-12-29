using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases
{
    public class SellProductUseCase : ISellProductUseCase
    {
        readonly IProductRepository productRepository;
        private readonly IRecordTransactionUseCase recordTransaction;

        public SellProductUseCase(IProductRepository productRepository, IRecordTransactionUseCase recordTransactionUseCase)
        {
            this.productRepository = productRepository;
            recordTransaction = recordTransactionUseCase;
        }
        public void Execute(string cashierName, int productId, int qtyToSell)
        {
            var product = productRepository.GetProduct(productId);
            if (product is null) return;

            recordTransaction.Execute(cashierName, productId, qtyToSell);

            product.Quantity -= qtyToSell;
            productRepository.UpdateProduct(product);
        }
    }
}
