using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UserService
{
    public class UserService: IUserService
    {
        private readonly IApplicationDbContext _dbContext;

        public UserService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetAll()
        {
            var users = _dbContext.Users.ToList();
            var userListResult = new List<User>();

            foreach(var tempUser in users)
            {
                userListResult.Add(new User
                {
                    Login = tempUser.Login, Password = tempUser.Password
                });
            }

            return userListResult;
        }

    }
}
