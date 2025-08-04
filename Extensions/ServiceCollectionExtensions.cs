using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simple_blog_api_dot_net.Services;
using simple_blog_api_dot_net.Services.Contracts;
using simple_blog_api_dot_net.WebSockets;

namespace simple_blog_api_dot_net.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddSingleton<PostWebSocketHandler>();

            return services;
        }
    }
}