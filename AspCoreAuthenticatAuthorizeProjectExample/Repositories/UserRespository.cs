using AspCoreAuthenticatAuthorizeProjectExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAuthenticatAuthorizeProjectExample.Repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRespository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User GetByGoogleId(string googleId)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.GoogleId == googleId);
            return user;
        }

        public User GetBYUserNameAndPassword(string username, string password)
        {
            var user = _appDbContext.Users.SingleOrDefault(u => u.UserName == username && u.Password == password);
            return user;
        }
    }
}
