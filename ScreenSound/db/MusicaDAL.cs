using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace ScreenSound.db;

    public class MusicaDAL
    {
        private readonly ScreenSoundContext _context;

        public MusicaDAL(ScreenSoundContext context)
        {
            _context = context;
        }

        // Método para adicionar uma música
        public void Adicionar(Musica musica)
        {
            _context.Musicas.Add(musica);
            _context.SaveChanges();
        }

        // Método para listar todas as músicas
        public IEnumerable<Musica> Listar()
        {
            return _context.Musicas.ToList();
        }

        // Método para buscar uma música pelo nome
        public Musica? RecuperarPeloNome(string nome)
        {
            return _context.Musicas.FirstOrDefault(m => m.Nome == nome);
        }

        // Método para deletar uma música pelo Id
        public void Deletar(int id)
        {
            var musica = _context.Musicas.Find(id);
            if (musica != null)
            {
                _context.Musicas.Remove(musica);
                _context.SaveChanges();
            }
        }
    }

