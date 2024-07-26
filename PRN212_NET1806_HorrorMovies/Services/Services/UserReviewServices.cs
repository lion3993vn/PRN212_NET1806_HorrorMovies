using Repositories.Repositories;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserReviewServices
    {
        private readonly UserRepo _userRepo;

        public UserReviewServices() 
        {
            _userRepo = new UserRepo();
        }

        public List<UserReviewShowModel> getAll()
        {
            var list = _userRepo.GetAll();
            return list.Select(x => new UserReviewShowModel()
            {
                CreationDate = x.CreationDate,
                HasProfanity = x.HasProfanity,
                HasSpoilers = x.HasSpoilers,
                IsSuperReviewer = x.IsSuperReviewer,
                MovieId = x.MovieId,
                MovieTitle = x.Movie.MovieTitle,
                Rating = x.Rating,
                UserId = x.UserId,
            }).ToList();
        }
    }
}
