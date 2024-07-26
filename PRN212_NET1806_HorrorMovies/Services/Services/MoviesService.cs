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
    public class MoviesService
    {
        private readonly MoviesRepo repo;

        public MoviesService()
        {
            repo = new MoviesRepo();
        }

        public List<MoviesModelShow> getAll()
        {
            var list = repo.GetAll();
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
