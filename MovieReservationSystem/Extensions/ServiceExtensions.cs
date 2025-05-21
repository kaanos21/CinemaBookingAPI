using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieReservationSystem.Concrete;
using MovieReservationSystem.Services.Interfaces;
using MovieReservationSystem.Services.Repository;
using MovieReservationSystem.Services.Token;

namespace MovieReservationSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(connectionString));

            services.AddIdentity<MovieReservationSystem.Entities.ApplicationUser, MovieReservationSystem.Entities.ApplicationUserRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IScreeningService, ScreeningService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ISeatsService, SeatsService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}