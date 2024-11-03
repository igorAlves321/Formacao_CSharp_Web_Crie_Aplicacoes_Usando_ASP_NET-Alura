using ScreenSound.Modelos;
using ScreenSound.db;
using System.Threading;

namespace ScreenSound.Menus;

public class MenuRegistrarArtista : Menu
{
    public override void Executar(ArtistaDAL artistaDAL, MusicaDAL musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL); // Chama base com os dois parâmetros
        ExibirTituloDaOpcao("Registro dos Artistas");

        Console.Write("Digite o nome do artista que deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;

        Console.Write("Digite a bio do artista que deseja registrar: ");
        string bioDoArtista = Console.ReadLine()!;

        // Cria uma nova instância do artista
        var artista = new Artista(nomeDoArtista, bioDoArtista);

        // Adiciona o artista ao banco de dados usando ArtistaDAL
        artistaDAL.Adicionar(artista);

        Console.WriteLine($"O artista {nomeDoArtista} foi registrado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
