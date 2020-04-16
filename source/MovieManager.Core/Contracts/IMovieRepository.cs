using MovieManager.Core.Entities;

namespace MovieManager.Core.Contracts
{
    public interface IMovieRepository
    {
        void AddMovies(Movie[] movies);
        Movie getLongestFilm();
        int getYearWithMostActionFilms();
    }
}
