
using Microsoft.EntityFrameworkCore;
using NZWaks.API.Data;
using NZWaks.API.Mappings;
using NZWaks.API.Repositories;

namespace NZWaks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Injecting our DbContext Class below
            builder.Services.AddDbContext<NZWalksDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
            //Injecting IRegionRepository with implementation for SQL database
            builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
            //Injecting Automapper
            builder.Services.AddAutoMapper(typeof(AutomapperProfiles));

            var app = builder.Build();

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

            Console.WriteLine(Guid.NewGuid().ToString());
        }
    }
}
