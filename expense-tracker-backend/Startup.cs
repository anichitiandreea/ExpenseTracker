using expense_tracker_backend.Database;
using expense_tracker_backend.Services;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace expense_tracker_backend
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=ExpenseTracker;User Id=postgres;Password=parola;"));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
