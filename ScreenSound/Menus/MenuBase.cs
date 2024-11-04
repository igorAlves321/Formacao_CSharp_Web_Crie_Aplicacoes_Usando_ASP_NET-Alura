using ScreenSound.db;
using System;

namespace ScreenSound.Menus
{
    public class MenuBase<T> where T : class
    {
        protected readonly DAL<T> dal;
        private readonly Func<T> objetoFactory;

        public MenuBase(DAL<T> dal, Func<T> objetoFactory)
        {
            this.dal = dal;
            this.objetoFactory = objetoFactory;
        }

        public void ExibirTituloDaOpcao(string titulo)
        {
            int quantidadeDeLetras = titulo.Length;
            string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
            Console.WriteLine(asteriscos);
            Console.WriteLine(titulo);
            Console.WriteLine(asteriscos + "\n");
        }

        public virtual void Executar()
        {
            Console.Clear();
            ExibirTituloDaOpcao($"Menu para {typeof(T).Name}s");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Listar Todos");
            Console.WriteLine("2. Registrar Novo");
            Console.WriteLine("3. Atualizar Existente");
            Console.WriteLine("4. Deletar Existente");
            Console.WriteLine("5. Sair");
            
            Console.Write("Digite a opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarTodos();
                    break;
                case "2":
                    Registrar();
                    break;
                case "3":
                    Atualizar();
                    break;
                case "4":
                    Deletar();
                    break;
                case "5":
                    Sair();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        public void ListarTodos()
        {
            ExibirTituloDaOpcao($"Lista de {typeof(T).Name}s");
            var itens = dal.Listar();
            if (itens == null || !itens.Any())
            {
                Console.WriteLine("Nenhum item encontrado.");
                return;
            }
            foreach (var item in itens)
            {
                Console.WriteLine(item);
            }
        }

        public void Registrar()
        {
            ExibirTituloDaOpcao($"Registrar Novo {typeof(T).Name}");
            var novoObjeto = objetoFactory(); // Usa objetoFactory para criar uma nova instância
            dal.Adicionar(novoObjeto);
            Console.WriteLine($"{typeof(T).Name} registrado com sucesso!");
        }

        public void Atualizar()
        {
            ExibirTituloDaOpcao($"Atualizar {typeof(T).Name}");
            Console.Write("Digite o ID do item para atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var objetoExistente = dal.RecuperarPor(obj => (obj as dynamic).Id == id); // Assume que T possui um Id
                if (objetoExistente != null)
                {
                    dal.Atualizar(objetoExistente);
                    Console.WriteLine($"{typeof(T).Name} atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine($"{typeof(T).Name} com ID {id} não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        public void Deletar()
        {
            ExibirTituloDaOpcao($"Deletar {typeof(T).Name}");
            Console.Write("Digite o ID do item para deletar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var objetoExistente = dal.RecuperarPor(obj => (obj as dynamic).Id == id);
                if (objetoExistente != null)
                {
                    dal.Deletar(objetoExistente);
                    Console.WriteLine($"{typeof(T).Name} deletado com sucesso!");
                }
                else
                {
                    Console.WriteLine($"{typeof(T).Name} com ID {id} não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        public void Sair()
        {
            Console.WriteLine("Saindo da aplicação. Até mais!");
            Environment.Exit(0);
        }
    }
}
