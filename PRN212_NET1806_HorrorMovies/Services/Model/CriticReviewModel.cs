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

    public class CriticReviewShowModel
    {
        public int ReviewId { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? CriticName { get; set; }

        public bool? IsTopCritic { get; set; }

        public string? PublicationName { get; set; }

        public string? ReviewUrl { get; set; }

        public string? Quote { get; set; }

        public string? ScoreSentiment { get; set; }

        public string? MovieId { get; set; }

        public string? MovieTitle { get; set; }

    }

}
