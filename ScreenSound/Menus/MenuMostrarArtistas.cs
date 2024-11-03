using ScreenSound.Modelos;
using ScreenSound.db;

namespace ScreenSound.Menus;

public class MenuMostrarArtistas : Menu
{
    public override void Executar(ArtistaDAL artistaDAL, MusicaDAL musicaDAL)
    {
        base.Executar(artistaDAL, musicaDAL); // Ajusta a chamada base para a nova assinatura
        ExibirTituloDaOpcao("Exibindo todos os artistas registrados na nossa aplicação");

        // Chama o método Listar diretamente e usa var para inferência de tipo
        var artistas = artistaDAL.Listar();

        Console.WriteLine("Artistas registrados:");
        foreach (var artista in artistas)
        {
            Console.WriteLine($"Id: {artista.Id}, Nome: {artista.Nome}, Bio: {artista.Bio}, FotoPerfil: {artista.FotoPerfil}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
