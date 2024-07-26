using CsvHelper.Configuration;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserReviewModel : ClassMap<UserReview>
    {
        public UserReviewModel()
        {
            Map(m => m.Rating).Name("rating");
            Map(m => m.IsSuperReviewer).Name("isSuperReviewer");
            Map(m => m.HasSpoilers).Name("hasSpoilers");
            Map(m => m.HasProfanity).Name("hasProfanity");
            Map(m => m.CreationDate).Name("creationDate");
            Map(m => m.UserId).Name("userId");
            Map(m => m.MovieId).Name("movieId");
        }
    }

    public class UserReviewShowModel()
    {
        public string UserId { get; set; } = null!;

        public float? Rating { get; set; }

        public bool? IsSuperReviewer { get; set; }

        public bool? HasSpoilers { get; set; }

        public bool? HasProfanity { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? MovieId { get; set; }

        public string? MovieTitle { get; set; }

    }
}
