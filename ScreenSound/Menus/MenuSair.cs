using ScreenSound.db;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(ArtistaDAL artistaDAL, MusicaDAL musicaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
        Environment.Exit(0);
    }
}
