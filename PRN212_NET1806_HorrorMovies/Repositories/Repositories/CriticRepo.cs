using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CriticRepo
    {
        private readonly HorrorMoviesContext _context;

        public CriticRepo()
        {
            _context = new HorrorMoviesContext();
        }

        public void AddCritic(CriticReview review)
        {
            _context.CriticReviews.Add(review);
            _context.SaveChanges();
        }

        public void AddCriticList(List<CriticReview> reviews)
        {
            foreach (var review in reviews)
            {
                var existingReview = _context.CriticReviews.SingleOrDefault(u => u.ReviewId == review.ReviewId);
                if (existingReview != null)
                {
                    // Cập nhật giá trị mới nếu cần
                    _context.Entry(existingReview).CurrentValues.SetValues(review);
                }
                else
                {
                    _context.CriticReviews.Add(review);
                }
            }
            _context.SaveChanges();
        }

        public CriticReview GetCriticById(int id)
        {
            var check = _context.CriticReviews.SingleOrDefault(x => x.ReviewId == id);
            return check;
        }

        public List<CriticReview> GetAll()
        {
            return _context.CriticReviews.Include(x => x.Movie).ToList();
        }

        public void DeleteAll(List<CriticReview> reviews)
        {
            _context.CriticReviews.RemoveRange(reviews);
            _context.SaveChanges();
        }
    }
}
