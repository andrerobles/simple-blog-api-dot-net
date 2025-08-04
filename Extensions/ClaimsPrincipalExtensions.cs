
using System.Security.Claims;

namespace simple_blog_api_dot_net.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("User Id claim not found");

            return int.Parse(userIdClaim.Value);
        }
    }
}