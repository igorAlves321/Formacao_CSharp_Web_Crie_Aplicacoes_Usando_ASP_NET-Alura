using ScreenSound.Modelos;
using ScreenSound.db;

namespace ScreenSound.Menus;

public class MenuMostrarArtistas : Menu
{
    public override void Executar(ArtistaDAL artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Exibindo todos os artistas registrados na nossa aplicação");

        // Chama o método Listar diretamente e usa var para inferência de tipo
        var artistas = artistaDAL.Listar();

        Console.WriteLine("Artistas registrados:");
        Console.WriteLine(artistas);

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
