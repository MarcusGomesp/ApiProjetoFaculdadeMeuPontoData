using MeuPontoMongoDb.Database;
using MeuPontoMongoDb.Interface;
using MeuPontoMongoDb.Service;
using Microsoft.EntityFrameworkCore;

namespace MeuPontoMongoDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))); // Configurar DbContext



            // Inje��o de depend�ncia do servi�o CadastroService
            builder.Services.AddScoped<ICadastroService, CadastroService>();
            builder.Services.AddScoped<IRegistroService, RegistroService>();
            builder.Services.AddScoped<IPerfilService, PerfilService>();



            // Adicionar controllers
            builder.Services.AddControllers();

            // Configura��o do Swagger para desenvolvimento
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            // Configurar o pipeline de requisi��es
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // For�ar redirecionamento para HTTPS
            app.UseHttpsRedirection();

            // Habilita o CORS
            app.UseCors(corsPolicyName);

            // Habilitar autoriza��o (caso haja autentica��o configurada)
            app.UseAuthorization();

            // Mapear as controllers
            app.MapControllers();

            // Executar migra��es pendentes no banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // Aplica as migra��es pendentes no banco de dados
            }

            // Rodar a aplica��o
            app.Run();
        }
    }
}
