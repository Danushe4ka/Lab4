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

            // ��������� ����������� ��� ������� � �� � �������������� EF
            string connectionString = builder.Configuration.GetConnectionString("RemoteSQLConnection");

            services.AddDbContext<Db8011Context>(options => options.UseSqlServer(connectionString));
            // ��������� ����������� OperationService
            services.AddTransient<IReviewService, ReviewService>();
            // ���������� �����������
            services.AddMemoryCache();
            // ���������� ��������� ������
            services.AddDistributedMemoryCache();
            services.AddSession();

            //������������� MVC
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

            // ��������� ��������� ����������� ������
            app.UseStaticFiles();

            // ��������� ��������� ������
            app.UseSession();

            // ��������� ��������� middleware �� ������������� ���� ������ � ���������� ������������� ����
            app.UseDbInitializer();

            // ��������� ��������� middleware ��� ���������� ����������� � ��������� ������ � ���
            app.UseOperatinCache("Test 20");

            //�������������
            app.UseRouting();
            // ������������� ������������� ��������� � ������������� 
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
