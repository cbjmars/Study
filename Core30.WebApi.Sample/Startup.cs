using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Core30.WebApi.Sample.Models;
using Core30.WebApi.Sample.Services;

namespace Core30.WebApi.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // �� �ż���� ��Ÿ������ ȣ�� �˴ϴ�. �� �ż��带 ����Ͽ� �����̳ʿ� ���񽺸� �߰� �� �� �ֽ��ϴ�.
        // ConfigureService()�� Configure()���� ���� ȣ��Ǹ�
        public void ConfigureServices(IServiceCollection services)
        {
            //EF Core�� ���Ӽ� ����(DI) �����̳ʿ� �Բ� DbContext ����� �����Ѵ�.
            //DbContext�� AddDbContext<TContext> ����� ����Ͽ� ���� �����̳ʿ� �߰� �� �� ����
            //appsettings.json�� ConnectionStrings �߰� (ex - EKP_WORKFLOW_Connection)
            services.AddDbContext<TbTestApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EKP_WORKFLOW_Connection")));
            #region Services�� ��ȯ �� �߰�
            // Scoped(������)�� Ŭ���̾�Ʈ ��û(����)�� �ѹ� �����˴ϴ�.
            // Ŭ���̾�Ʈ ��û�� �ѹ� ������ �Ǹ�, �ش� ������ �����Ǹ� ���������� ����մϴ�
            services.AddScoped<ITestApiService, TestApiService>();
            #endregion
            // Controller ������� �߰� (Controllers�� ����ϰڴٰ� ����)
            services.AddControllers();
        }

        // �� �ż���� ��Ÿ������ ȣ�� �˴ϴ�. �� �ż��带 ����Ͽ� HTTP ��û ������������ ������ �� �ֽ��ϴ�.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // launchSettings.json�� ASPNETCORE_ENVIRONMENT ���� ���� "Development"�� ������ ���
            if (env.IsDevelopment())
            {
                // ����ȯ�濡���� ����ó�� �̵���� ����ϰڴ�.
                app.UseDeveloperExceptionPage();
            }

            #region wwwroot���� ���� �� �������� ��� �� �ݵ��� �߰�
            // ������ ������ �������� �ʴ� URL �ۼ����̸�, UseStaticFiles�� �Բ� ��������.
            // UseDefaultFiles�� ����ϸ� default.htm, default.html, index.htm, index.html ���� ���ϵ��� �˻�
            // (�������� : HTML,CSS,�̹���,JavaScript)
            app.UseDefaultFiles();

            // wwwroot�� ����Ű�� ���� �����ڸ� �⺻������ �����ϰ� ���� ������ ����(��ȯ)�ϵ��� ����
            app.UseStaticFiles();
            #endregion

            // https Redirect �̵����� HTTP ��û�� HTTPS�� Redirect��
            app.UseHttpsRedirection();
            // ������ HTTP ��û�� ��ġ���� ���� ���� ���� EndPoint�� ������.
            app.UseRouting();
            // ����ڿ��� ���� ���ҽ��� �׼����� �� �ִ� ������ �ο�
            app.UseAuthorization();
            // ��û ���������ο� MapControllers ��������Ʈ �߰�.
            app.UseEndpoints(endpoints =>
            {
                // ��θ� �������� �ʰ� �۾��� ������ �߰��մϴ�.
                endpoints.MapControllers();
            });
        }
    }
}
