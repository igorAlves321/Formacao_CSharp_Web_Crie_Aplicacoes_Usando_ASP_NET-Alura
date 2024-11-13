using System.Collections.Generic;

namespace ScreenSound.Modelos
{
    public class Musica
    {
        public Musica(string nome)
        {
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int? AnoLancamento { get; set; }
        public int ArtistaId { get; set; } // Adicionando a propriedade ArtistaId
        public virtual Artista? Artista { get; set; } // Relacionamento com Artista

        public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();

        public override string ToString()
        {
            return $"Nome: {Nome} - Ano de Lançamento: {AnoLancamento}";
        }
    }
}
