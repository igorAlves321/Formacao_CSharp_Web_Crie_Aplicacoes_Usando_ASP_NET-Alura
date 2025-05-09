﻿using System.ComponentModel.DataAnnotations;
namespace ScreenSoundMVC.Models;
public class Artista 
{
    public Artista()
    {
        Nome = string.Empty;
        Bio = string.Empty;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        Musicas = new List<Musica>();
    }
    
    public Artista(string nome, string bio)
    {
        Nome = nome;
        Bio = bio;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        Musicas = new List<Musica>();
    }
    
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome do artista é obrigatório")]
    public string Nome { get; set; }
    
    [Display(Name = "Foto de Perfil")]
    public string FotoPerfil { get; set; }
    
    [Required(ErrorMessage = "A biografia é obrigatória")]
    [Display(Name = "Biografia")]
    public string Bio { get; set; }
    
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
    
    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
    }
}