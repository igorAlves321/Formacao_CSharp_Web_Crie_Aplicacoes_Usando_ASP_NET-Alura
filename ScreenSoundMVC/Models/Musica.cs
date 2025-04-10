using System.ComponentModel.DataAnnotations;
namespace ScreenSoundMVC.Models;
public class Musica
{
    public Musica()
    {
        Nome = string.Empty;
        Generos = new List<Genero>();
    }
    
    public Musica(string nome)
    {
        Nome = nome;
        Generos = new List<Genero>();
    }
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome da música é obrigatório")]
    public string Nome { get; set; }
    
    [Display(Name = "Ano de Lançamento")]
    public int? AnoLancamento { get; set; }
    
    public int? ArtistaId { get; set; }
    
    public virtual Artista? Artista { get; set; }
    
    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();
}