using Microsoft.EntityFrameworkCore;
using simple_blog_api_dot_net.Data;
using simple_blog_api_dot_net.Extensions;
using simple_blog_api_dot_net.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDependencies();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
            });

            return new BadRequestObjectResult(new { Message = "Validation Failed", Errors = errors });
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseWebSockets();

app.UseAuthentication();
app.UseAuthorization();

app.UsePostWebSocketEndpoint();

app.MapControllers();
app.Run();