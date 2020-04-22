using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using MovieManager.Core.DataTransferObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMovies(Movie[] movies)
        {
            if (movies is null)
            {
                throw new System.ArgumentNullException(nameof(movies));
            }

            foreach (var movie in movies)
            {
                _dbContext.Movies.Add(movie);
            }
        }

        public Movie getLongestFilm()
        {
            return _dbContext.Movies
                .Select(m => new
                {
                    Movie = m
                })
                .AsEnumerable()
                .OrderByDescending(m => m.Movie.Duration)
                .ThenBy(m => m.Movie.Title)
                .Select(m => m.Movie)
                .First();
        }

        public int getYearWithMostActionFilms()
        {
            
            List<int> years = _dbContext.Movies
                .Select(m => new
                {
                    Movie = m
                })
                .AsEnumerable()
                .OrderBy(m => m.Movie.Year)
                .Select(m => m.Movie.Year)
                .ToList();

            List<Movie> actionFilms = _dbContext.Movies
                .Select(m => new
                {
                    Movie = m
                })
                .Where(m => m.Movie.Category.CategoryName.Equals("Action"))
                .Select(m => m.Movie)
                .ToList();

            int count = 0;
            int countTemp = 0;
            int retYear = 0;
            foreach (var year in years)
            {
                countTemp = 0;

                foreach (var actionFilm in actionFilms)
                {
                    if(actionFilm.Year.Equals(year))
                    {
                        countTemp++;
                    }
                }
                if(countTemp > count)
                {
                    retYear = year;
                    count = countTemp;
                }

            }
            return retYear;
                

        }

        

    }
}