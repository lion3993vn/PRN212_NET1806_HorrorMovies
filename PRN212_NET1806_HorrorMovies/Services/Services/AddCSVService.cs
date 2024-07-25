using CsvHelper.Configuration;
using CsvHelper;
using Repositories.Entities;
using Repositories.Repositories;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AddCSVService
    {
        private readonly MoviesRepo _movieRepo;
        private readonly UserRepo _userRepo;
        private readonly CriticRepo _criticRepo;

        public AddCSVService()
        {
            _movieRepo = new MoviesRepo();
            _userRepo = new UserRepo();
            _criticRepo = new CriticRepo();
        }

        public List<Movie> ReadMovieCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null
            }))
            {
                csv.Context.RegisterClassMap<MoviesModel>();
                var records = csv.GetRecords<Movie>().ToList();
                return records;
            }
        }

        public List<UserReview> ReadUserCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null
            }))
            {
                csv.Context.RegisterClassMap<UserReviewModel>();
                var records = csv.GetRecords<UserReview>().ToList();
                return records;
            }
        }

        public List<CriticReview> ReadCriticCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                BadDataFound = null
            }))
            {
                csv.Context.RegisterClassMap<CriticReviewModel>();
                var records = csv.GetRecords<CriticReview>().ToList();
                return records;
            }
        }

        public void AddCSV(string fileName)
        {

            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("Choose a file");
            }
            else
            {
                if (fileName.Contains("user_reviews"))
                {
                    _userRepo.DeleteAll(_userRepo.GetAll());
                    AddUserReview(fileName);
                }
                else if (fileName.Contains("movies"))
                {
                    _userRepo.DeleteAll(_userRepo.GetAll());
                    _criticRepo.DeleteAll(_criticRepo.GetAll());
                    _movieRepo.DeleteAll(_movieRepo.GetAll());
                    AddMovie(fileName);
                }
                else
                {
                    _criticRepo.DeleteAll(_criticRepo.GetAll());
                    AddCriticReview(fileName);
                }
            }
        }

        public void AddMovie(string filePath)
        {

            var listMovies = ReadMovieCsv(filePath);

            _movieRepo.AddMovieList(listMovies);

            throw new Exception("Add Moive Thành Công");
        }

        public void AddUserReview(string filePath)
        {
            var list = ReadUserCsv(filePath);

            _userRepo.AddUserList(list);

            throw new Exception("Add User_Review Thành Công");
        }

        public void AddCriticReview(string filePath)
        {

            var listCritic = ReadCriticCsv(filePath);

            _criticRepo.AddCriticList(listCritic);

            throw new Exception("Add Critic_Review Thành Công");
        }
    }

}
