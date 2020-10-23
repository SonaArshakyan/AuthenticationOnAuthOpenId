using AspCoreAuthenticatAuthorizeProjectExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAuthenticatAuthorizeProjectExample.Repositories
{
    public interface IUserRepository
    {
        User GetBYUserNameAndPassword(string  username, string password);
        User GetByGoogleId(string googleId);
    }
}
