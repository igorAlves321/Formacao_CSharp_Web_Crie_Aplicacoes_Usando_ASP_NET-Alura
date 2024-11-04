using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.db;

    public class ScreenSoundContext : DbContext
    {
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }

        // Construtor que recebe as opções, usado principalmente no tempo de execução
        public ScreenSoundContext(DbContextOptions<ScreenSoundContext> options) : base(options) { }

        // Construtor padrão sem parâmetros para permitir criação de migrations
        public ScreenSoundContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure o banco de dados apenas se as opções ainda não estiverem configuradas
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=ScreenSound;user=root;password=;port=3306", 
                    new MySqlServerVersion(new Version(8, 0, 26)));
            }
        }
    }

