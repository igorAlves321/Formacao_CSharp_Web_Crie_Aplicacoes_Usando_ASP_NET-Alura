using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    public class ScreenSoundContext : DbContext
    {
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }

        public ScreenSoundContext(DbContextOptions<ScreenSoundContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=ScreenSound;user=root;password=;port=3306", 
                new MySqlServerVersion(new Version(8, 0, 26)));
        }
    }
}
