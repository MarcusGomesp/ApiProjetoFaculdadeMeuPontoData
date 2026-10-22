using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Service;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MeuPontoMongoDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))); // Configurar DbContext



            // Injeção de dependência do serviço CadastroService
            builder.Services.AddScoped<ICadastroService, CadastroService>();
            builder.Services.AddScoped<IRegistroService, RegistroService>();
            builder.Services.AddScoped<IPerfilService, PerfilService>();
            builder.Services.AddScoped<ISolicitacaoService, SolicitacaoService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Autenticacao JWT: a chave vem de User Secrets (dev) ou variavel de ambiente Jwt__Key (producao)
            var jwtSection = builder.Configuration.GetSection("Jwt");
            builder.Services.Configure<JwtSettings>(jwtSection);
            var jwt = jwtSection.Get<JwtSettings>() ?? new JwtSettings();
            if (string.IsNullOrWhiteSpace(jwt.Key) || Encoding.UTF8.GetByteCount(jwt.Key) < 32)
                throw new InvalidOperationException(
                    "Configure 'Jwt:Key' com pelo menos 32 caracteres: dotnet user-secrets set \"Jwt:Key\" \"<chave>\"");

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                        ClockSkew = TimeSpan.FromMinutes(1)
                    };
                });
            builder.Services.AddAuthorization();



            // Adicionar controllers
            builder.Services.AddControllers();

            // Configuração do Swagger para desenvolvimento
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeuPonto API", Version = "v1" });

                // Botao "Authorize" no Swagger: cole o token retornado pelo /api/Cadastro/login
                var esquema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Informe o token JWT obtido no login.",
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                };
                c.AddSecurityDefinition("Bearer", esquema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { esquema, Array.Empty<string>() } });
            });

            //cors para rodar no navegador 
            var corsPolicyName = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("http://127.0.0.1:3000") // Permite todas as origens
                              .AllowAnyMethod()
                              .AllowAnyHeader();

                    });
            });


            // Configurar o app
            var app = builder.Build();

            // Configurar o pipeline de requisições
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Forçar redirecionamento para HTTPS
            app.UseHttpsRedirection();

            // Habilita o CORS
            app.UseCors(corsPolicyName);

            // Habilitar autorização (caso haja autenticação configurada)
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear as controllers
            app.MapControllers();

            // Executar migrações pendentes no banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); 
            }

            // Rodar a aplicação
            app.Run();
        }
    }
}
