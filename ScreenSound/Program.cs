using ScreenSound.db;
using ScreenSound.Menus;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;

// Instancia o contexto e os repositórios para Artista e Musica
var context = new ScreenSoundContext();
var artistaDAL = new ArtistaDAL(context);
var musicaDAL = new MusicaDAL(context); // Nova instância para MusicaDAL

// Configuração do menu com as opções
Dictionary<int, Menu> opcoes = new()
{
    { 1, new MenuRegistrarArtista() },
    { 2, new MenuRegistrarMusica() },
    { 3, new MenuMostrarArtistas() },
    { 4, new MenuMostrarMusicas() },
    { -1, new MenuSair() }
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
        // Passa tanto artistaDAL quanto musicaDAL para cada execução de menu
        opcoes[opcaoEscolhidaNumerica].Executar(artistaDAL, musicaDAL);
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

ExibirOpcoesDoMenu();
