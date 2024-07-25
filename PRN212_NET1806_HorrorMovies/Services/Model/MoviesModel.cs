using CsvHelper.Configuration;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class MoviesModel : ClassMap<Movie>
    {
        public MoviesModel()
        {
            Map(m => m.MovieId).Name("movieId");
            Map(m => m.MovieTitle).Name("movieTitle");
            Map(m => m.MovieYear).Name("movieYear");
            Map(m => m.MovieUrl).Name("movieURL");
            Map(m => m.MovieRank).Name("movieRank");
        }
    }
}
