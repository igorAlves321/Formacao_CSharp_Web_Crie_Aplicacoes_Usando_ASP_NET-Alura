using ScreenSound.Menus;
using ScreenSound.Modelos;
using ScreenSound.db; // Referência à classe de conexão
using System;
using System.Collections.Generic;

        Artista ira = new Artista("Ira!", "Banda Ira!");
        Artista beatles = new("The Beatles", "Banda The Beatles");

        Dictionary<string, Artista> artistasRegistrados = new();
        artistasRegistrados.Add(ira.Nome, ira);
        artistasRegistrados.Add(beatles.Nome, beatles);

        Dictionary<int, Action> opcoes = new();
        opcoes.Add(1, RegistrarArtista);
        opcoes.Add(2, RegistrarMusica);
        opcoes.Add(3, MostrarArtistas);
        opcoes.Add(4, MostrarMusicas);
        opcoes.Add(5, TestarConexaoBanco); // Nova opção para testar a conexão
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
            Console.WriteLine("Digite 5 para testar a conexão com o banco de dados"); // Nova opção
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
            // Lógica para registrar artista
        }

        void RegistrarMusica()
        {
            Console.WriteLine("Registrar Música selecionado.");
            // Lógica para registrar música
        }

        void MostrarArtistas()
        {
            Console.WriteLine("Mostrar Artistas selecionado.");
            // Lógica para mostrar artistas
        }

        void MostrarMusicas()
        {
            Console.WriteLine("Mostrar Músicas selecionado.");
            // Lógica para mostrar músicas
        }

        void TestarConexaoBanco()
        {
            Console.WriteLine("Testando conexão com o banco de dados...");
            var dbConnection = new Connection(); // Instancia a classe de conexão
            var connection = dbConnection.GetConnection(); // Tenta abrir a conexão

            if (connection != null)
            {
                Console.WriteLine("Conexão com o banco de dados foi realizada com sucesso!");
                dbConnection.CloseConnection(connection); // Fecha a conexão
            }
            else
            {
                Console.WriteLine("Falha ao conectar ao banco de dados.");
            }
        }

        void Sair()
        {
            Console.WriteLine("Saindo...");
            Environment.Exit(0);
        }

        ExibirOpcoesDoMenu();
    