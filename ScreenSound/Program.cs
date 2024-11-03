using ScreenSound.db;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;

Dictionary<int, Action> opcoes = new();
opcoes.Add(1, RegistrarArtista);
opcoes.Add(2, RegistrarMusica);
opcoes.Add(3, MostrarArtistas);
opcoes.Add(4, MostrarMusicas);
opcoes.Add(5, TestarConexaoBanco);
opcoes.Add(6, AtualizarArtista);
opcoes.Add(7, ExcluirArtista);
opcoes.Add(8, RecuperarArtistaPeloNome); // Nova opção para buscar pelo nome
opcoes.Add(-1, Sair);

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

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para registrar um artista");
    Console.WriteLine("Digite 2 para registrar a música de um artista");
    Console.WriteLine("Digite 3 para mostrar todos os artistas");
    Console.WriteLine("Digite 4 para exibir todas as músicas de um artista");
    Console.WriteLine("Digite 5 para testar a conexão com o banco de dados");
    Console.WriteLine("Digite 6 para atualizar um artista");
    Console.WriteLine("Digite 7 para excluir um artista");
    Console.WriteLine("Digite 8 para buscar um artista pelo nome"); // Nova opção
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        opcoes[opcaoEscolhidaNumerica].Invoke();
        if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    }
    else
    {
        Console.WriteLine("Opção inválida");
    }
}

void RegistrarArtista()
{
    Console.WriteLine("Registrar Artista selecionado.");

    Console.Write("Digite o nome do artista: ");
    string nome = Console.ReadLine()!;

    Console.Write("Digite a bio do artista: ");
    string bio = Console.ReadLine()!;

    Console.Write("Digite a URL da foto de perfil do artista: ");
    string fotoPerfil = Console.ReadLine()!;

    var artista = new Artista(nome, bio) { FotoPerfil = fotoPerfil };

    var artistaDAL = new ArtistaDAL();
    artistaDAL.Adicionar(artista);
    Console.WriteLine("Artista registrado com sucesso!");
}

void AtualizarArtista()
{
    Console.WriteLine("Atualizar Artista selecionado.");

    Console.Write("Digite o ID do artista: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.Write("Digite o novo nome do artista: ");
    string nome = Console.ReadLine()!;

    Console.Write("Digite a nova bio do artista: ");
    string bio = Console.ReadLine()!;

    Console.Write("Digite a nova URL da foto de perfil do artista: ");
    string fotoPerfil = Console.ReadLine()!;

    var artista = new Artista(nome, bio) { Id = id, FotoPerfil = fotoPerfil };

    var artistaDAL = new ArtistaDAL();
    artistaDAL.Atualizar(artista);
    Console.WriteLine("Artista atualizado com sucesso!");
}

void ExcluirArtista()
{
    Console.WriteLine("Excluir Artista selecionado.");

    Console.Write("Digite o ID do artista a ser excluído: ");
    int id = int.Parse(Console.ReadLine()!);

    var artistaDAL = new ArtistaDAL();
    artistaDAL.Deletar(id);
    Console.WriteLine("Artista excluído com sucesso!");
}

void RegistrarMusica()
{
    Console.WriteLine("Registrar Música selecionado.");
    // Lógica para registrar música
}

void MostrarArtistas()
{
    Console.WriteLine("Mostrar Artistas selecionado.");

    var artistaDAL = new ArtistaDAL();
    IEnumerable<Artista> artistas = artistaDAL.Listar();

    foreach (var artista in artistas)
    {
        Console.WriteLine($"Id: {artista.Id}, Nome: {artista.Nome}, Bio: {artista.Bio}, FotoPerfil: {artista.FotoPerfil}");
    }
}

void MostrarMusicas()
{
    Console.WriteLine("Mostrar Músicas selecionado.");
    // Lógica para mostrar músicas
}

void TestarConexaoBanco()
{
    Console.WriteLine("Testando conexão com o banco de dados...");
    using var context = new ScreenSoundContext();
    if (context.Database.CanConnect())
    {
        Console.WriteLine("Conexão com o banco de dados foi realizada com sucesso!");
    }
    else
    {
        Console.WriteLine("Falha ao conectar ao banco de dados.");
    }
}

void RecuperarArtistaPeloNome()
{
    Console.WriteLine("Buscar Artista pelo Nome selecionado.");

    Console.Write("Digite o nome do artista: ");
    string nome = Console.ReadLine()!;

    var artistaDAL = new ArtistaDAL();
    var artista = artistaDAL.RecuperarPeloNome(nome);

    if (artista != null)
    {
        Console.WriteLine($"Artista encontrado: {artista.Nome}, Bio: {artista.Bio}, Foto de Perfil: {artista.FotoPerfil}");
    }
    else
    {
        Console.WriteLine("Artista não encontrado.");
    }
}

void Sair()
{
    Console.WriteLine("Saindo...");
    Environment.Exit(0);
}

ExibirOpcoesDoMenu();
