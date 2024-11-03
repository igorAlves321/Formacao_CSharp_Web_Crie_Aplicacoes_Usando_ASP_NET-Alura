using ScreenSound.Modelos;
using ScreenSound.db;
using System;

namespace ScreenSound.Menus;

    public class MenuRegistrarMusica : Menu
    {
        public override void Executar(MusicaDAL musicaDAL, ArtistaDAL artistaDAL)
        {
            base.Executar(musicaDAL);
            ExibirTituloDaOpcao("Registro de músicas");

            Console.Write("Digite o título da música que deseja registrar: ");
            string tituloDaMusica = Console.ReadLine()!;

            var musica = new Musica(tituloDaMusica);
            musicaDAL.Adicionar(musica);

            Console.WriteLine($"A música '{tituloDaMusica}' foi registrada com sucesso!");
            Console.WriteLine("\nPressione uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }

