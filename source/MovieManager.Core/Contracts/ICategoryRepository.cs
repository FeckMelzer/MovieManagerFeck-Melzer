using MovieManager.Core.Entities;
using System.Collections.Generic;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        void AddCategories(List<Category> categories);
        Category getCategoryWithMostFilms();
    }
}
