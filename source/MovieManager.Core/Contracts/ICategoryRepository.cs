using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;
using System.Collections.Generic;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        void AddCategories(List<Category> categories);
        Category getCategoryWithMostFilms();

        public IEnumerable<CategoryDTO1> CategoryStatistics1();
        public IEnumerable<CategoryDTO2> CategoryStatistics2();
        
    }
}
