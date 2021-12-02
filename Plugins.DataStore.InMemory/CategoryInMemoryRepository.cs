using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    // Test Data first ! Use SQL data later
    public class CategoryInMemoryRepository : ICategoryRepository
    {
        readonly List<Category> categories;
        public CategoryInMemoryRepository()
        {
            // add some default categories
            categories = new()
            {
                new Category { CategoryId = 1, Name = "Milk", Description = "Milk is a nutrient-rich liquid food produced by the mammary glands of mammals. It is the primary source of nutrition for young mammals, including breastfed" },
                new Category { CategoryId = 2, Name = "Meal", Description = "Meat is animal flesh that is eaten as food.[1] Humans have hunted and killed animals for meat since prehistoric times. The advent of civilization allowed the domestication of animals such as chickens, sheep, rabbits, pigs and cattle" },
                new Category { CategoryId = 3, Name = "Vegetable", Description = "Vegetables are parts of plants that are consumed by humans or other animals as food. The original meaning is still commonly used and is applied to plants collectively to refer to all edible plant matter, including the flowers, fruits, stems, leaves, roots, and seeds" }
            };
        }

        public void AddCategory(Category category)
        {
            if (categories.Any(x => x.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase))) return;

            if (categories != null && categories.Count > 0)
            {
                var maxID = categories.Max(x => x.CategoryId);
                category.CategoryId = maxID++;
            }
            else
            {
                category.CategoryId = 1;
            }
            categories.Add(category);
        }

        public void DeleteCategory(int categoryID)
        {
            categories?.Remove(GetCategory(categoryID));
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories;
        }

        public Category GetCategory(int categoryID)
        {
            return categories?.FirstOrDefault(x => x.CategoryId == categoryID);
        }

        public void UpdateCategory(Category category)
        {
            Category update = GetCategory(category.CategoryId);
            if (update is not null)
            {
                update.Name = category.Name;
                update.Description = category.Description;
            }
        }
    }
}
