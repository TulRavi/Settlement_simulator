using Microsoft.AspNetCore.Authentication;
using SettlementGame.Domain;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SettlementGame.Web
{//временно вырезал <DockerDefaultTargetOS>Windows</DockerDefaultTargetOS> из проекта 
 //—оздать BuildingsController

    //—делать GET /api/buildings

    //”бедитьс€, что Swagger показывает оба контроллера
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls(
            "http://localhost:5126",
            "https://localhost:7107"
            );
            builder.Services.AddDbContext<GameDbContext>(
            options => options.UseSqlite("Data Source=game.db"));
            // Add services to the container.

            builder.Services.AddControllers(); //подготовка к подключению контроллеров
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthentication("Basic").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null); ;//включает механизм входа типа Basic
            builder.Services.AddAuthorization();//включает роли дл€ различного уровн€ досутупа


            builder.Services.AddSwaggerGen();
            

            //builder.Services.AddOpenApi();
            builder.Services.AddSingleton<DataWorld>();
            builder.Services.AddScoped<WorldCreator>();
            builder.Services.AddScoped<WorkerEmploymentService>();
            builder.Services.AddScoped<WorldService>();
            var app = builder.Build();//создали сервер
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();// Ч подключили все классы с [ApiController] к маршрутам
            //≈сли убрать Ч контроллеры не будут вызыватьс€

            

            app.Run();//запускаем сервер,аналог while(true) в консоли
        }
    }
}
