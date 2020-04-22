using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using MovieManager.Core.DataTransferObjects;
using System;

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
        public IEnumerable<CategoryDTO1> CategoryStatistics1()
        {
            return _dbContext.Categories
                .Select(c => new CategoryDTO1
                {
                    Category = c,
                    MoviesCount = c.Movies.Count(),
                    CompleteMinutes = c.Movies.Sum(c => c.Duration)
                })
                .OrderBy(c => c.Category.CategoryName)
                .ToArray();

        }

        public IEnumerable<CategoryDTO2> CategoryStatistics2()
        {
            return _dbContext.Categories
                .Select(c => new CategoryDTO2
                {
                    category = c,
                    averageMovieDuration = c.Movies.Average(c => c.Duration)
                })
                .OrderBy(c => c.category.CategoryName)
                .ToArray();
        }

    }
}