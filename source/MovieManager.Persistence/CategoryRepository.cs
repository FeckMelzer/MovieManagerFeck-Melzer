using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using MovieManager.Core.DataTransferObjects;


namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCategories(List<Category> categories)
        {
            if (categories is null)
            {
                throw new System.ArgumentNullException(nameof(categories));
            }

            foreach (var category in categories)
            {
                _dbContext.Categories.Add(category);
            }
        }

        public Category getCategoryWithMostFilms()
        {
            return _dbContext.Categories
                .Select(c => new
                {
                    Category = c
                })
                .AsEnumerable()
                .OrderByDescending(c => c.Category.Movies.Count())
                .Select(c => c.Category)
                .First();
        }

    }
}