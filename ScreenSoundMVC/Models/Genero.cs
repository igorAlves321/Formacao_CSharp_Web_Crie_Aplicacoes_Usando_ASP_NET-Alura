using System.ComponentModel.DataAnnotations;
namespace ScreenSoundMVC.Models;
public class Genero
{
    public Genero()
    {
        Nome = string.Empty;
        Descricao = string.Empty;
        Musicas = new List<Musica>();
    }
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome do gênero é obrigatório")]
    public string Nome { get; set; }
    
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
}