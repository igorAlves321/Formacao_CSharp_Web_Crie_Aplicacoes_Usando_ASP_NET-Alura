using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ScreenSound.db;

    public class DAL<T> where T : class
    {
        protected readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> Listar(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            // Adiciona as entidades relacionadas especificadas em 'includes'
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            // Adiciona as entidades relacionadas especificadas em 'includes'
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(condicao);
        }
    }
