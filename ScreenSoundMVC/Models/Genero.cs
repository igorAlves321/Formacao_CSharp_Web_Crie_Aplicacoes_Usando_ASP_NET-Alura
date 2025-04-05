using System.ComponentModel.DataAnnotations;

namespace ScreenSoundMVC.Models;

public class Genero
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome do gênero é obrigatório")]
    public string? Nome { get; set; } = string.Empty;
    
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; } = string.Empty;

    public virtual ICollection<Musica> Musicas { get; set; }
}