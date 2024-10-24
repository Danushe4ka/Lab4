using Lab4.Data;
using Lab4.Middleware;
using Lab4.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IServiceCollection services = builder.Services;

            // внедрение зависимости для доступа к БД с использованием EF
            string connectionString = builder.Configuration.GetConnectionString("RemoteSQLConnection");

            services.AddDbContext<Db8011Context>(options => options.UseSqlServer(connectionString));
            // внедрение зависимости OperationService
            services.AddTransient<IReviewService, ReviewService>();
            // добавление кэширования
            services.AddMemoryCache();
            // добавление поддержки сессии
            services.AddDistributedMemoryCache();
            services.AddSession();

            //Использование MVC
            services.AddControllersWithViews();
            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем поддержку сессий
            app.UseSession();

            // добавляем компонент middleware по инициализации базы данных и производим инициализацию базы
            app.UseDbInitializer();

            // добавляем компонент middleware для реализации кэширования и записывем данные в кэш
            app.UseOperatinCache("Test 20");

            //Маршрутизация
            app.UseRouting();
            // устанавливаем сопоставление маршрутов с контроллерами 
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
