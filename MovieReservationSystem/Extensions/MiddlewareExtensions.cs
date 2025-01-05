using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MovieReservationSystem.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}