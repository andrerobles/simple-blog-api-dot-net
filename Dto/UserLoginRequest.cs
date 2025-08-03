using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_blog_api_dot_net.Dto
{
    public class UserLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}