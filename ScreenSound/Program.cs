using Microsoft.EntityFrameworkCore;
using ScreenSound.db;
using ScreenSound.Menus;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;

var optionsBuilder = new DbContextOptionsBuilder<ScreenSoundContext>();
optionsBuilder.UseMySql("server=localhost;database=ScreenSound;user=root;password=;port=3306", 
    new MySqlServerVersion(new Version(8, 0, 26)));

var context = new ScreenSoundContext(optionsBuilder.Options);

// Funções para criar novos objetos de Artista e Musica
Func<Artista> criarArtista = () => 
{
    Console.Write("Digite o nome do artista: ");
    var nome = Console.ReadLine() ?? "";
    Console.Write("Digite a bio do artista: ");
    var bio = Console.ReadLine() ?? "";
    return new Artista(nome, bio);
};

Func<Musica> criarMusica = () => 
{
    Console.Write("Digite o título da música: ");
    var titulo = Console.ReadLine() ?? "";
    return new Musica(titulo);
};

// Instancia os menus genéricos para Artista e Musica
var menuArtistas = new MenuBase<Artista>(new DAL<Artista>(context), criarArtista);
var menuMusicas = new MenuBase<Musica>(new DAL<Musica>(context), criarMusica);

// Configuração do menu com as opções
Dictionary<int, Action> opcoes = new()
{
    { 1, menuArtistas.Registrar },
    { 2, menuMusicas.Registrar },
    { 3, menuArtistas.ListarTodos },
    { 4, menuMusicas.ListarTodos },
    { -1, Sair }
};

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para registrar um artista");
    Console.WriteLine("Digite 2 para registrar uma música");
    Console.WriteLine("Digite 3 para mostrar todos os artistas");
    Console.WriteLine("Digite 4 para exibir todas as músicas");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    if (int.TryParse(Console.ReadLine(), out int opcaoEscolhidaNumerica) && opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        opcoes[opcaoEscolhidaNumerica].Invoke();
        if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    }
    else
    {
        Console.WriteLine("Opção inválida");
    }
}

void ExibirLogo()
{
    Console.WriteLine(@"
░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
    Console.WriteLine("Boas vindas ao Screen Sound 3.0!");
}

void Sair()
{
    Console.WriteLine("Saindo da aplicação. Até mais!");
    Environment.Exit(0);
}

ExibirOpcoesDoMenu();
