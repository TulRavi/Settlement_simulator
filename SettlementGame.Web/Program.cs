using SettlementGame.Domain;
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

            // Add services to the container.

            builder.Services.AddControllers(); //подготовка к подключению контроллеров
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<DataWorld>();
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

            //app.UseAuthorization();


            app.MapControllers();// Ч подключили все классы с [ApiController] к маршрутам
            //≈сли убрать Ч контроллеры не будут вызыватьс€

            

            app.Run();//запускаем сервер,аналог while(true) в консоли
        }
    }
}
