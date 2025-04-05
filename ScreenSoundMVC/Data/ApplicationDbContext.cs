using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScreenSoundMVC.Models;
using ScreenSoundMVC.Models.Identity;

namespace ScreenSoundMVC.Data;

public class ApplicationDbContext : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{
    // DbSets representando as tabelas do banco de dados
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Genero> Generos { get; set; }

    // Construtor padrão
    public ApplicationDbContext()
    {
    }

    // Construtor para injeção de dependência
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    // Configuração de relacionamentos e mapeamento
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração para relação muitos-para-muitos entre Musicas e Generos
        modelBuilder.Entity<Musica>()
            .HasMany(m => m.Generos)
            .WithMany(g => g.Musicas);

        // Configurações adicionais (se necessário)
    }
}