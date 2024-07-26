using Repositories.Entities;
using Repositories.Repositories;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MovieService
    {
        private readonly MoviesRepo _movieRepo;

        public MovieService()
        {
            _movieRepo = new MoviesRepo();
        }

        public List<Movie> GetAll()
        {
            return _movieRepo.GetAll();
        }

        public void DeleteMovie(MoviesModelShow x)
        {
            Movie y = new Movie()
            {
                MovieId = x.MovieId,
                MovieRank = x.MovieRank,
                MovieTitle = x.MovieTitle,
                MovieUrl = x.MovieUrl,
                MovieYear = x.MovieYear,
            };
            _movieRepo.DeleteMovie(y);
        }

        public List<MoviesModelShow> GetSearchYear(int? start, int? end, string? name)
        {
            var list = _movieRepo.SearchYear(start, end, name);
            return list.Select(x => new MoviesModelShow()
            {
                MovieId = x.MovieId,
                MovieRank = x.MovieRank,
                MovieTitle = x.MovieTitle,
                MovieUrl = x.MovieUrl,
                MovieYear = x.MovieYear,
            }).ToList();
        }

        public List<MoviesModelShow> GetSearchName(string? name)
        {
            var list =  _movieRepo.SearchName(name);
            return list.Select(x => new MoviesModelShow()
            {
                MovieId = x.MovieId,
                MovieRank = x.MovieRank,
                MovieTitle = x.MovieTitle,
                MovieUrl = x.MovieUrl,
                MovieYear = x.MovieYear,
            }).ToList();
        }
    }
}
