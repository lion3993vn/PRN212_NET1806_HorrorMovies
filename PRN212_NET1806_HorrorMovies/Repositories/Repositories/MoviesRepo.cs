using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MoviesRepo
    {
        private readonly HorrorMoviesContext _context;

        public MoviesRepo()
        {
            _context = new HorrorMoviesContext();
        }


        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void AddMovieList(List<Movie> movie)
        {
            _context.Movies.AddRange(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie x)
        {
            _context.Movies.Update(x);
            _context.SaveChanges();
        }

        public void DeleteMovie(Movie x)
        {
            _context.Movies.Remove(x);
            _context.SaveChanges();
        }

        public Movie GetMovieById(string id)
        {
            var check = _context.Movies.SingleOrDefault(x => x.MovieId == id);

            return check;
        }

        public void DeleteAll(List<Movie> movies)
        {
            _context.Movies.RemoveRange(movies);
            _context.SaveChanges();
        }

        public List<Movie> SearchYear(int start, int end)
        {
            var movies = _context.Movies.Where(x => x.MovieYear.HasValue &&
                                               x.MovieYear.Value >= start &&
                                               x.MovieYear.Value <= end)
                                                .OrderBy(xx => xx.MovieYear.Value).ToList();
            return movies;
        }
    }
}
