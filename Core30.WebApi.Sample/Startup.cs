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

        // 이 매서드는 런타임으로 호출 됩니다. 이 매서드를 사용하여 컨테이너에 서비스를 추가 할 수 있습니다.
        // ConfigureService()는 Configure()보다 먼저 호출되며
        public void ConfigureServices(IServiceCollection services)
        {
            //EF Core는 종속성 주입(DI) 컨테이너와 함께 DbContext 사용을 지원한다.
            //DbContext는 AddDbContext<TContext> 방법을 사용하여 서비스 컨테이너에 추가 할 수 있음
            //appsettings.json에 ConnectionStrings 추가 (ex - EKP_WORKFLOW_Connection)
            services.AddDbContext<TbTestApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EKP_WORKFLOW_Connection")));
            #region Services로 전환 시 추가
            // Scoped(수명서비스)는 클라이언트 요청(연결)당 한번 생성됩니다.
            // 클라이언트 요청당 한번 생성이 되며, 해당 연결이 유지되면 지속적으로 사용합니다
            services.AddScoped<ITestApiService, TestApiService>();
            #endregion
            // Controller 사용방식을 추가 (Controllers를 사용하겠다고 지정)
            services.AddControllers();
        }

        // 이 매서드는 런타임으로 호출 됩니다. 이 매서드를 사용하여 HTTP 요청 파이프라인을 구성할 수 있습니다.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // launchSettings.json의 ASPNETCORE_ENVIRONMENT 세팅 값이 "Development"로 설정된 경우
            if (env.IsDevelopment())
            {
                // 개발환경에서만 예외처리 미들웨어 사용하겠다.
                app.UseDeveloperExceptionPage();
            }

            #region wwwroot폴더 생성 후 웹페이지 사용 시 반듯이 추가
            // 실제로 파일을 제공하지 않는 URL 작성기이며, UseStaticFiles와 함께 쓰여야함.
            // UseDefaultFiles를 사용하면 default.htm, default.html, index.htm, index.html 등의 파일들을 검색
            // (정적파일 : HTML,CSS,이미지,JavaScript)
            app.UseDefaultFiles();

            // wwwroot를 가리키는 파일 공급자를 기본값으로 설정하고 정적 파일을 제공(반환)하도록 설정
            app.UseStaticFiles();
            #endregion

            // https Redirect 미들웨어는 HTTP 요청을 HTTPS로 Redirect함
            app.UseHttpsRedirection();
            // 들어오는 HTTP 요청을 일치시켜 앱의 실행 가능 EndPoint로 보낸다.
            app.UseRouting();
            // 사용자에게 보안 리소스에 액세스할 수 있는 권한을 부여
            app.UseAuthorization();
            // 요청 파이프라인에 MapControllers 엔드포인트 추가.
            app.UseEndpoints(endpoints =>
            {
                // 경로를 지정하지 않고 작업의 끝점을 추가합니다.
                endpoints.MapControllers();
            });
        }
    }
}
