using ScreenSound.Modelos;
using ScreenSound.db;

namespace ScreenSound.Menus;

public class Menu
{
    public void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }

    // Alterado para aceitar tanto ArtistaDAL quanto MusicaDAL como parâmetros
    public virtual void Executar(ArtistaDAL artistaDAL, MusicaDAL musicaDAL)
    {
        Console.Clear();
        // Aqui você pode chamar métodos de ArtistaDAL ou MusicaDAL, se necessário
    }
}
