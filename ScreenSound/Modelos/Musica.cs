namespace ScreenSound.Modelos;

public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int AnoLancamento { get; set; } // Nova propriedade para o ano de lançamento

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Ano de Lançamento: {AnoLancamento}"); // Exibe o ano de lançamento
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}
        Ano de Lançamento: {AnoLancamento}"; // Adiciona o ano de lançamento na exibição
    }
}
