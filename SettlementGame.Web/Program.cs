using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SettlementGame.Domain;
using System.Security.Claims;
using System.Text;

namespace SettlementGame.Web
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var JwtKey = builder.Configuration["Jwt:Key"];
            //if (string.IsNullOrWhiteSpace(JwtKey))
            //{
            //    throw new InvalidOperationException(
            //        "JWT key is not configured.");
            //}

            //making new jwt-configuration for easy runnung in public version:
            //1) look Jwt:Key in user secrets(for dev, real file location: %APPDATA%\Microsoft\UserSecrets)
            //2) look in jwt.key and create a new one, if it does not exists.

            string? JwtKey = builder.Configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(JwtKey))
            {
                string jwtKeyPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "jwt.key");

                if (File.Exists(jwtKeyPath))
                {
                    JwtKey = File.ReadAllText(jwtKeyPath).Trim();
                }
                else
                {
                    byte[] keyBytes = new byte[32];
                    System.Security.Cryptography.RandomNumberGenerator.Fill(keyBytes);

                    JwtKey = Convert.ToHexString(keyBytes).ToLowerInvariant();

                    File.WriteAllText(jwtKeyPath, JwtKey);
                }
            }
            builder.Configuration["Jwt:Key"] = JwtKey;
            builder.WebHost.UseUrls("http://localhost:5126");



            // DATABASE


            // GameDbContext is registered as Scoped.
            //
            // This means that one DbContext instance is Created for each
            // HTTP request and disposed when the request is completed.
            //
            // Scoped is the standard lifetime for Entity Framework Core
            // DbContext because it keeps one unit of work within a request.
            
            //added path to be sure, that the file will be created in the correct folder
            string databasePath = Path.Combine(AppContext.BaseDirectory,"game.db");
            //builder.Services.AddDbContext<GameDbContext>(options => options.UseSqlite("Data Source=game.db"));
            builder.Services.AddDbContext<GameDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));


            // API


            // Registers API controllers.
            builder.Services.AddControllers();

            // Enables API endpoint metadata used by Swagger.
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();


            
            // AUTHENTICATION
            

            // AddAuthentication enables the ASP.NET Core authentication system.
            //
            // Authentication answers the question:
            // "Who is making this request?"
            //
            // In our application the client sends a JWT in the
            // Authorization header:
            //
            // Authorization: Bearer <token>
            //
            // ASP.NET Core uses the configured authentication scheme
            // to validate this token and identify the user.
            builder.Services.AddAuthentication(options =>
            {
                // DefaultAuthenticateScheme specifies which authentication
                // scheme ASP.NET Core uses by default to identify the user.
                //
                // Here we use JWT Bearer authentication.
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                // DefaultChallengeScheme specifies which authentication
                // scheme is used when authentication is required but the
                // request does not contain valid authentication credentials.
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                // DefaultScheme defines the default authentication scheme.
                options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })

            // JwtBearer configures JWT Bearer authentication.
            //
            // The client sends the token using:
            //
            // Authorization: Bearer <JWT>
            //
            // The server then validates the token before allowing access
            // to protected endpoints.
            .AddJwtBearer(options =>
            {
                // TokenValidationParameters defines the rules used
                // to validate incoming JWT tokens.
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        // Validate the issuer of the token.
                        //
                        // The issuer identifies the application or service
                        // that Created the token.
                        ValidateIssuer = true,

                        // Validate the intended audience of the token.
                        //
                        // The audience identifies the application or client
                        // for which the token was issued.
                        ValidateAudience = true,

                        // Validate the token expiration time.
                        //
                        // Expired tokens must not be accepted.
                        ValidateLifetime = true,

                        // Validate the cryptographic signature of the token.
                        //
                        // This prevents a client from modifying the token
                        // contents without knowing the secret signing key.
                        ValidateIssuerSigningKey = true,

                        // Secret key used to validate the JWT signature.
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey)),

                        // Only tokens issued by "game" are accepted.
                        ValidIssuer = "game",

                        // Only tokens intended for "game_client" are accepted.
                        ValidAudience = "game_client",

                        // Tells ASP.NET Core which claim contains
                        // the user's role.
                        //
                        // This allows [Authorize(Roles = "Admin")]
                        // to determine whether the user has the required role.
                        RoleClaimType = ClaimTypes.Role
                    };

                // Handles JWT authentication errors.
                //
                // This is useful during development for diagnosing
                // invalid or rejected tokens.
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("JWT ERROR:");
                        Console.WriteLine(context.Exception.Message);

                        return Task.CompletedTask;
                    }
                };
            });


            
            // AUTHORIZATION
            

            // AddAuthorization enables the ASP.NET Core authorization system.
            //
            // Authentication answers:
            // "Who are you?"
            //
            // Authorization answers:
            // "Are you allowed to perform this action?"
            builder.Services.AddAuthorization();


            
            // APPLICATION SERVICES
            

            // DataWorld is registered as Singleton.
            //
            // The current application contains one shared
            // colony/worker-camp simulation.
            //
            // DataWorld contains the in-memory state of this simulation,
            // so all controllers and services must work with the same
            // DataWorld instance.
            //
            // If the application is later extended to support multiple
            // users with separate colonies, DataWorld should no longer
            // be a global Singleton. Each game/session would need its
            // own isolated game state.
            builder.Services.AddSingleton<DataWorld>();


            // WorldCreator depends on GameDbContext.
            //
            // It is registered as Scoped so one instance is used
            // during each HTTP request.
            //
            // Scoped is also the usual lifetime for services that
            // depend on a Scoped DbContext.
            builder.Services.AddScoped<WorldCreator>();


            // WorkerEmploymentService depends on GameDbContext,
            // so it also uses the Scoped lifetime.
            //
            // During one HTTP request it works with the DbContext
            // instance Created for that request.
            builder.Services.AddScoped<WorkerEmploymentService>();


            // WorldService depends on GameDbContext and therefore
            // also uses the Scoped lifetime.
            builder.Services.AddScoped<WorldService>();


            // WebAuthService works with the database and therefore
            // uses the Scoped lifetime as well.
            builder.Services.AddScoped<WebAuthService>();


            
            // BUILD APPLICATION
            

            var app = builder.Build();


            
            // DATABASE INITIALIZATION
            

            // This code runs during application startup rather than
            // inside an HTTP request.
            //
            // Because GameDbContext is Scoped, we Create a scope manually
            // in order to obtain a properly scoped DbContext instance.
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider
                    .GetRequiredService<GameDbContext>();

                // Creates the database and its tables if they do not exist.

                //Console.WriteLine($"DATABASE: {databasePath}");
                //Console.WriteLine($"DATABASE EXISTS: {File.Exists(databasePath)}");
                //Console.WriteLine($"USERS COUNT BEFORE SEED: {db.Users.Count()}");
                db.Database.EnsureCreated();

                // Seed initial users if the database does not contain
                // any users yet.
                //
                // This can later be moved to a separate database
                // seeding class.
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


            
            // HTTP REQUEST PIPELINE
            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Displays detailed exception information during development.
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            
            // AUTHENTICATION AND AUTHORIZATION MIDDLEWARE
            

            // UseAuthentication processes authentication for each request.
            //
            // In our application JWT Bearer authentication reads the
            // Authorization header, validates the JWT and, if successful,
            // Creates the authenticated user in HttpContext.User.
            //
            // The user's claims and role are also available through User.
            app.UseAuthentication();


            // UseAuthorization checks whether the authenticated user
            // has permission to access the requested endpoint.
            //
            // [Authorize] requires an authenticated user.
            //
            // [Authorize(Roles = "Admin")] additionally requires
            // the user to have the "Admin" role.
            app.UseAuthorization();


            // Maps controller actions to HTTP endpoints.
            //
            // Without MapControllers(), controllers with [ApiController]
            // would not be mapped to routes.
            app.MapControllers();


            // Starts the web application.
            app.Run();
        }
    }
}