using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreAuthenticatAuthorizeProjectExample.Models
{
    public class LoginModel
    {
        public int LoginModelId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememeberLogin{ get; set; }
    }
}
