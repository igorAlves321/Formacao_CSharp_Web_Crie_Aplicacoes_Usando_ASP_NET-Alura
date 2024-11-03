using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace ScreenSound.db;

public class ArtistaDAL
{
    // Método para adicionar um novo artista ao banco de dados
    public void Adicionar(Artista artista)
    {
        using var context = new ScreenSoundContext();

        context.Artistas.Add(artista);
        context.SaveChanges();
        Console.WriteLine("Artista adicionado com sucesso!");
    }

    // Método para listar todos os artistas
    public IEnumerable<Artista> Listar()
    {
        using var context = new ScreenSoundContext();

        return context.Artistas.ToList(); // Usa EF Core para listar todos os artistas
    }

    // Método para atualizar um artista existente
    public void Atualizar(Artista artista)
    {
        using var context = new ScreenSoundContext();

        var artistaExistente = context.Artistas.Find(artista.Id); // Busca o artista pelo Id
        if (artistaExistente != null)
        {
            artistaExistente.Nome = artista.Nome;
            artistaExistente.Bio = artista.Bio;
            artistaExistente.FotoPerfil = artista.FotoPerfil;

            context.SaveChanges(); // Salva as alterações no banco de dados
            Console.WriteLine("Artista atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Artista não encontrado.");
        }
    }

    // Método para deletar um artista
    public void Deletar(int id)
    {
        using var context = new ScreenSoundContext();

        var artista = context.Artistas.Find(id); // Busca o artista pelo Id
        if (artista != null)
        {
            context.Artistas.Remove(artista); // Remove o artista do contexto
            context.SaveChanges(); // Salva as alterações no banco de dados
            Console.WriteLine("Artista deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Artista não encontrado.");
        }
    }
}
