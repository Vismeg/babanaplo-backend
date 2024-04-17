
using BabaNaplo.Models;
using Microsoft.EntityFrameworkCore;


namespace BabaNaplo
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin();
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                  });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BabanaploContext>(option =>
            {
                var connectionString = "SERVER=localhost;DATABASE=babanaplo;USER=root;PASSWORD=;";
                option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors(MyAllowSpecificOrigins);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }


    }
}
