using MeuPontoMongoDb.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuPontoMongoDb.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }



        public DbSet<BancoHoras> BancoHoras { get; set; } = null!;
        public DbSet<Cadastro> Cadastros { get; set; } = null!;
        public DbSet<Perfil> Perfis { get; set; } = null!;
        public DbSet<Registro> Registros { get; set; } = null!;
        public DbSet<Solicitacao> Solicitacoes { get; set; } = null!;




        //funciona SQLServer Local
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Cadastro 1:1 Perfil
            modelBuilder.Entity<Cadastro>()
                .HasOne(c => c.Perfil)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Perfil>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Perfil_Cadastro");

            // Cadastro 1:1 BancoHoras
            modelBuilder.Entity<Cadastro>()
                .HasOne(c => c.BancoHoras)
                .WithOne(b => b.Usuario)
                .HasForeignKey<BancoHoras>(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_BancoHoras_Cadastro");

            // Cadastro 1:N Registro
            modelBuilder.Entity<Registro>()
                .HasOne(r => r.Usuario)
                .WithMany(c => c.Registros)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Registro_Cadastro");

            // Cadastro 1:N Solicitacao
            modelBuilder.Entity<Solicitacao>()
                .HasOne(s => s.Usuario)
                .WithMany(c => c.Solicitacoes)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Solicitacao_Cadastro");

            // Registro 1:N Solicitacao
            modelBuilder.Entity<Solicitacao>()
                .HasOne(s => s.Registro)
                .WithMany(r => r.Solicitacoes)
                .HasForeignKey(s => s.IdRegistro)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Solicitacao_Registro");

        }
    }
}
