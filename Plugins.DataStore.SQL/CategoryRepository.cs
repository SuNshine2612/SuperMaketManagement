using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MarketContext marketContext;

        public CategoryRepository(MarketContext marketContext)
        {
            this.marketContext = marketContext;
        }
        public void AddCategory(Category category)
        {
            marketContext.Categories.Add(category);
            marketContext.SaveChanges();
        }

        public void DeleteCategory(int categoryID)
        {
            var cat = GetCategory(categoryID);
            if (cat is null) return;

            marketContext.Categories.Remove(cat);
            marketContext.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return marketContext.Categories.ToList();
        }

        public Category GetCategory(int categoryID)
        {
            return marketContext.Categories.Find(categoryID);
        }

        public void UpdateCategory(Category category)
        {
            var temp = GetCategory(category.CategoryId);

            if (temp is null) return;

            temp.Name = category.Name;
            temp.Description = category.Description;

            marketContext.SaveChanges();
        }
    }
}
