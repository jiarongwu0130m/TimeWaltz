using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
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

            builder.Services.AddTransient<VacationTypeService>();
            builder.Services.AddScoped<ShiftScheduleService>();
            builder.Services.AddScoped<FlextimeService>();
            builder.Services.AddScoped<GradeTableService>();
            builder.Services.AddScoped<SpecialHolidayService>();
            builder.Services.AddScoped<PersonalDataService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<CompRequestService>();
            builder.Services.AddScoped<OvertimeRequestService>();
            builder.Services.AddTransient<ClockService>();
            builder.Services.AddScoped<PublicHolidayService>();
            builder.Services.AddScoped<LeaveService>();
            builder.Services.AddScoped<RequestStatusService>();
            builder.Services.AddScoped<SpecialHolidayDaysService>();
            builder.Services.AddScoped<SpecialVacationService>();
            builder.Services.AddScoped<ApprovalService>();
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("TimeWaltz")));
            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<ShiftService>();

            builder.Services.AddScoped<BillboardService>();
            builder.Services.AddScoped<AccessService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<DropDownBasicSettingService>();
            builder.Services.AddHttpContextAccessor();
 

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("forWeb", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                })
                .AddCookie("EmployeeAuthScheme", options =>
                {
                    options.LoginPath = "/Employee/Account/Login";
                }); ;

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
