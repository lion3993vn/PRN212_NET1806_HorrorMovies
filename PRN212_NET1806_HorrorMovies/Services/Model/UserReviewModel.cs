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
}
