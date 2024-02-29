using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entity;
using WebApplication1.Services;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
      {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TimeWaltzContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("TimeWaltz")));
            //TODO: 將Transient統一改成Scoped
            builder.Services.AddTransient<VacationTypeService>();
            builder.Services.AddScoped<ShiftScheduleService>();
            builder.Services.AddScoped<FlextimeService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddTransient<ClockService>();
            builder.Services.AddScoped<PublicHolidayService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
