using CsvHelper.Configuration;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class CriticReviewModel : ClassMap<CriticReview>
    {
        public CriticReviewModel() 
        {
            Map(m => m.CreationDate).Name("creationDate");
            Map(m => m.CriticName).Name("criticName");
            Map(m => m.IsTopCritic).Name("isTopCritic");
            Map(m => m.PublicationName).Name("publicationName");
            Map(m => m.ReviewUrl).Name("reviewUrl");
            Map(m => m.Quote).Name("quote");
            Map(m => m.ReviewId).Name("reviewId");
            Map(m => m.ScoreSentiment).Name("scoreSentiment");
            Map(m => m.MovieId).Name("movieId");
        }
    }
}
