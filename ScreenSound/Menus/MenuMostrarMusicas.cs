using ScreenSound.Modelos;
using ScreenSound.db;

namespace ScreenSound.Menus;

public class MenuMostrarMusicas : Menu
{
    public override void Executar(ArtistaDAL artistaDAL, MusicaDAL musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL); // Corrige a chamada para corresponder à nova assinatura
        ExibirTituloDaOpcao("Exibir detalhes do artista");

        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
        string nomeDoArtista = Console.ReadLine()!;

        var artistaRecuperado = artistaDAL.RecuperarPeloNome(nomeDoArtista);

        if (artistaRecuperado != null)
        {
            Console.WriteLine("\nDiscografia:");
            artistaRecuperado.ExibirDiscografia();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
