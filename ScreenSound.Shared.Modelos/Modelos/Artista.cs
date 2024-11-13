namespace ScreenSound.Modelos
{
    public class Artista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Inicializando a propriedade
        public string Bio { get; set; } = string.Empty;  // Inicializando a propriedade
        public string? FotoPerfil { get; set; } // Propriedade FotoPerfil, pode ser nula
public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();

        // Construtor que aceita dois argumentos
        public Artista(string nome, string bio)
        {
            Nome = nome;
            Bio = bio;
        }

        // Método para exibir a discografia do artista
        public void ExibirDiscografia()
        {
            Console.WriteLine($"Discografia de {Nome}:");
            if (Musicas.Any())
            {
                foreach (var musica in Musicas)
                {
                    Console.WriteLine($"- {musica}");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma música registrada.");
            }
        }

        public override string ToString()
        {
            return $"Nome: {Nome} - Bio: {Bio}";
        }
    }
}
