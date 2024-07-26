using Repositories.Repositories;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CriticReviewServices
    {
        private readonly CriticRepo _criticRepo;

        public CriticReviewServices() 
        {
            _criticRepo = new CriticRepo();
        }

        public List<CriticReviewShowModel> getAll()
        {
            var list = _criticRepo.GetAll();
            return list.Select(x => new CriticReviewShowModel()
            {
                CreationDate = x.CreationDate,
                CriticName = x.CriticName,
                IsTopCritic = x.IsTopCritic,
                MovieId = x.MovieId,
                MovieTitle = x.Movie.MovieTitle,
                PublicationName = x.PublicationName,
                Quote = x.Quote,
                ReviewId = x.ReviewId,
                ReviewUrl = x.ReviewUrl,
                ScoreSentiment = x.ScoreSentiment
            }).ToList();
        }
    }
}
