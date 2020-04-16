using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
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
                .OrderBy(m => m.Movie.Duration)
                .ThenBy(m => m.Movie.Title)
                .Select(m => m.Movie)
                .First();
        }

        public int getYearWithMostActionFilms()
        {
            List<Movie> movies = _dbContext.Movies
                .OrderBy(m => m.Year)
                .ToList();
            int yearWithMostActionFilms = movies.ElementAt(0).Year;
            int count = 0;
            
            for(int i = 0; i < _dbContext.Movies.Count(); i++)
            {
                int yearcount = 0;
                for(int k = 0; movies.ElementAt(k).Year == movies.ElementAt(k+1).Year; k++)
                {
                    yearcount++;
                }
                i += yearcount;
                if (yearcount > count)
                {
                    yearWithMostActionFilms = movies.ElementAt(i).Year;
                    count = yearcount;
                }
            }

            return yearWithMostActionFilms;
        }
    }
}