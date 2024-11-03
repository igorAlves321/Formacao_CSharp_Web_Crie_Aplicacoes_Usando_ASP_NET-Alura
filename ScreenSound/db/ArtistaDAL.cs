using ScreenSound.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace ScreenSound.db
{
    public class ArtistaDAL
    {
        private readonly ScreenSoundContext _context;

        // Construtor que aceita uma instância de ScreenSoundContext
        public ArtistaDAL(ScreenSoundContext context)
        {
            _context = context;
        }

        // Método para adicionar um novo artista
        public void Adicionar(Artista artista)
        {
            _context.Artistas.Add(artista);
            _context.SaveChanges();
            Console.WriteLine("Artista adicionado com sucesso!");
        }

        // Método para listar todos os artistas
        public IEnumerable<Artista> Listar()
        {
            return _context.Artistas.ToList();
        }

        // Método para atualizar um artista existente
        public void Atualizar(Artista artista)
        {
            var artistaExistente = _context.Artistas.Find(artista.Id);
            if (artistaExistente != null)
            {
                artistaExistente.Nome = artista.Nome;
                artistaExistente.Bio = artista.Bio;
                artistaExistente.FotoPerfil = artista.FotoPerfil;
                _context.SaveChanges();
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
            var artista = _context.Artistas.Find(id);
            if (artista != null)
            {
                _context.Artistas.Remove(artista);
                _context.SaveChanges();
                Console.WriteLine("Artista deletado com sucesso!");
            }
            else
            {
                Console.WriteLine("Artista não encontrado.");
            }
        }

        // Método para recuperar um artista pelo nome
        public Artista? RecuperarPeloNome(string nome)
        {
            return _context.Artistas.FirstOrDefault(a => a.Nome == nome);
        }
    }
}
