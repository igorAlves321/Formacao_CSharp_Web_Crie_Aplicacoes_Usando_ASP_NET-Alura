using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System;
using System.Collections.Generic;

namespace ScreenSound.Banco;

    public class ScreenSoundContext : DbContext
    {
        // DbSets representando as tabelas do banco de dados
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        // String de conexão para o banco MySQL
        private string connectionString = "Server=localhost;Database=ScreenSound;User=root;Password=;";

        // Construtores
        public ScreenSoundContext()
        {
        }

        public ScreenSoundContext(DbContextOptions options) : base(options)
        {
        }

        // Configuração do banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29))) // Substitua pela versão do seu MySQL
                    .UseLazyLoadingProxies();
            }
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
            // Exemplo: Configuração de constraints ou índices
        }
    }

