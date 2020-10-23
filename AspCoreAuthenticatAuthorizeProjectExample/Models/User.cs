using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAuthenticatAuthorizeProjectExample.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FavColor { get; set; }
        public string Role { get; set; }
        public string GoogleId { get; set; }
    }
}
