using Repositories.Entities;
using Repositories.Repositories;
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

        public void DeleteMovie(Movie x)
        {
            _movieRepo.DeleteMovie(x);
        }

        public List<Movie> GetSearchYear(int start, int end)
        {
            return _movieRepo.SearchYear(start, end);
        }

    }
}
