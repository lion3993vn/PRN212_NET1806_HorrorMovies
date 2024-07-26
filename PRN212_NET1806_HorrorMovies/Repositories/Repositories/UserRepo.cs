using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepo
    {
        private readonly HorrorMoviesContext _context;

        public UserRepo()
        {
            _context = new HorrorMoviesContext();
        }

        public void AddUser(UserReview user)
        {
            _context.UserReviews.Add(user);
            _context.SaveChanges();
        }

        public void AddUserList(List<UserReview> users)
        {
            foreach (var user in users)
            {
                using (var context = new HorrorMoviesContext())
                {
                    var existingUser = context.UserReviews
                        .AsNoTracking()
                        .FirstOrDefault(u => u.UserId == user.UserId);

                    if (existingUser != null)
                    {
                        // Cập nhật giá trị mới nếu cần
                        context.Entry(existingUser).CurrentValues.SetValues(user);
                    }
                    else
                    {
                        context.UserReviews.Add(user);
                    }

                    context.SaveChanges();
                }
            }
        }


        public UserReview GetUserById(string id)
        {
            var check = _context.UserReviews.SingleOrDefault(x => x.UserId == id);
            return check;
        }

        public List<UserReview> GetAll()
        {
            return _context.UserReviews.Include(x => x.Movie).ToList();
        }

        public void DeleteAll(List<UserReview> user)
        {
            _context.UserReviews.RemoveRange(user);
            _context.SaveChanges();
        }
    }
}
