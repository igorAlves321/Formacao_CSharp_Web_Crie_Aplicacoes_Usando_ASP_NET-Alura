using System.ComponentModel.DataAnnotations;

namespace ScreenSoundMVC.Models;

public class Musica
{
    public Musica()
    {
        
    }
    
    public Musica(string nome)
    {
        Nome = nome;
    }

    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome da música é obrigatório")]
    public string Nome { get; set; }
    
    [Display(Name = "Ano de Lançamento")]
    public int? AnoLancamento { get; set; }
    
    public int? ArtistaId { get; set; }
    
    public virtual Artista? Artista { get; set; }
    
    public virtual ICollection<Genero> Generos { get; set; }
    
}