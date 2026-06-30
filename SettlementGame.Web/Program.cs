using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.IdentityModel.Tokens;
using SettlementGame.Domain;
using System.Security.Claims;
using System.Text;

namespace SettlementGame.Web
{//временно вырезал <DockerDefaultTargetOS>Windows</DockerDefaultTargetOS> из проекта 
 //Создать BuildingsController

    //Сделать GET /api/buildings

    //Убедиться, что Swagger показывает оба контроллера
    public class Program
    {
        public const string JwtKey =
    "here_is_my_secret_key_12345!!!!!!!";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var key = JwtKey;
            builder.WebHost.UseUrls(
            "http://localhost:5126"
            //"https://localhost:7107"
            );
            builder.Services.AddDbContext<GameDbContext>(
            options => options.UseSqlite("Data Source=game.db"));
            // Add services to the container.

            builder.Services.AddControllers(); //подготовка к подключению контроллеров
            builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddAuthentication("Basic").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null); ;//включает механизм входа типа Basic
            builder.Services.AddAuthorization();//включает роли для различного уровня досутупа
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(key)),
                        ValidIssuer = "game",
                        ValidAudience = "game_client",

                        RoleClaimType = ClaimTypes.Role
                    };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("JWT ERROR:");
                        Console.WriteLine(context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            }); ;

            builder.Services.AddSwaggerGen();
            

            //builder.Services.AddOpenApi();
            builder.Services.AddSingleton<DataWorld>();
            builder.Services.AddScoped<WorldCreator>();
            builder.Services.AddScoped<WorkerEmploymentService>();
            builder.Services.AddScoped<WorldService>();

            var app = builder.Build();//создали сервер
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<GameDbContext>();
                db.Database.EnsureCreated();
                //пока оставим создание юезров в коде, после надо бы отд.утилиту по добавлению в БД сделать
                if (!db.Users.Any())
                {
                    db.Users.Add(new UserEntity
                    {
                        Email = "Admin",
                        PasswordHash =
                            BCrypt.Net.BCrypt.HashPassword("321"),
                        Role = "Admin"
                    });

                    db.Users.Add(new UserEntity
                    {
                        Email = "User",
                        PasswordHash =
                            BCrypt.Net.BCrypt.HashPassword("123"),
                        Role = "User"
                    });

                    db.SaveChanges();
                }
            }
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
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.Use(async (ctx, next) =>
            //{
            //    Console.WriteLine("MIDDLEWARE HIT: " + ctx.Request.Path);
            //    await next();
            //});

            app.UseDeveloperExceptionPage();//чтобы понять причину 500 ошибки добавим аока что
            app.MapControllers();// — подключили все классы с [ApiController] к маршрутам
            //Если убрать — контроллеры не будут вызываться

            

            app.Run();//запускаем сервер,аналог while(true) в консоли
        }
    }
}
