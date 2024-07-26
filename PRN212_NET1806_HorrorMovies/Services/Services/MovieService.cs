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

        public Movie GetById(string Id)
        {
            return _movieRepo.GetMovieById(Id);
        }

        public string GenerateRandomStringWithHyphens()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            int[] segments = { 8, 4, 4, 4, 12 };

            for (int i = 0; i < segments.Length; i++)
            {
                for (int j = 0; j < segments[i]; j++)
                {
                    result.Append(chars[random.Next(chars.Length)]);
                }
                if (i < segments.Length - 1)
                {
                    result.Append('-');
                }
            }

            return result.ToString();
        }

        public string checkIdGenerate()
        {
            bool check = false;

            string Id = "";

            while (!check)
            {
                Id = GenerateRandomStringWithHyphens();
                var item = _movieRepo.GetMovieById(Id);

                if (item == null)
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            return Id;
        }

        public bool IsValidUrl(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
            {
                return (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        public bool ValidateRankAdd(string rank)
        {
            return true;
        }

        public bool ValidateInputMovie(MovieCheckModel model)
        {

            #region Title
            if (string.IsNullOrEmpty(model.MovieTitle))
            {
                throw new Exception("Title is required for input");
            }
            #endregion

            #region URL
            if (string.IsNullOrEmpty(model.MovieUrl))
            {
                throw new Exception("URL is required for input");
            }
            else
            {
                if (!IsValidUrl(model.MovieUrl))
                {
                    throw new Exception("Not Correct Format of URL");
                }
            }
            #endregion

            #region Year
            if (string.IsNullOrEmpty(model.MovieYear))
            {
                throw new Exception("Year is required for input");
            }
            else
            {
                var year = model.MovieYear;

                for (int i = 0; i < year.Length; i++)
                {
                    if (!char.IsDigit(year[i]))
                    {
                        throw new Exception("Wrong Year Format");
                    }
                }

                if (year.Length <= 4 && year.Length > 0)
                {
                    int currentYear = DateTime.UtcNow.Year;

                    if (int.Parse(year) < 1900 || currentYear < int.Parse(year))
                    {
                        throw new Exception("The timeline for year begins in 1900 and must not exceed the current year");
                    }
                }
                else
                {
                    throw new Exception("Wrong Year Format");
                }
            }
            #endregion

            #region Rank
            if (string.IsNullOrEmpty(model.MovieRank))
            {
                throw new Exception("Rank is required for input");
            }
            else
            {
                var rank = model.MovieRank;

                for (int i = 0; i < rank.Length; i++)
                {
                    if (!char.IsDigit(rank[i]))
                    {
                        throw new Exception("Rank Format must be a number");
                    }
                }
            }
            #endregion

            return true;
        }

        public void AddMovie(Movie movie)
        {
            _movieRepo.AddMovie(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            _movieRepo.UpdateMovie(movie);
        }
    }
}
