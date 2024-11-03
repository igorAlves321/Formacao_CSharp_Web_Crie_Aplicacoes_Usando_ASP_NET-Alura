using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.db;

public class ScreenSoundContext : DbContext
{
    // Define a tabela Artistas
    public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Define a string de conexão para o MySQL
        optionsBuilder.UseMySql("server=localhost;database=ScreenSound;user=root;password=;port=3306",
            new MySqlServerVersion(new Version(8, 0, 26))); // Ajuste a versão conforme necessário
    }
}
