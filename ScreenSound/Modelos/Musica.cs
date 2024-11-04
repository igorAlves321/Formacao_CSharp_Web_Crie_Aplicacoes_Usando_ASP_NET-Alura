namespace ScreenSound.Modelos;

public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public int? ArtistaId { get; set; }
    public Artista? Artista { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Ano de Lançamento: {AnoLancamento}");
        if (Artista != null)
        {
            Console.WriteLine($"Artista: {Artista.Nome}");
        }
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}
        Ano de Lançamento: {AnoLancamento}";
    }
}
