using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DAServices.Servicios
{
    public class Services<T> : IDisposable where T : class
    {
        private readonly UsersContext context;

        public Services(UsersContext context)
        {
            this.context = context;
        }

        protected DbSet<T> EntitySet
        {
            get{
                return context.Set<T>();
            }
        }

          public async Task<IEnumerable<T>> GetObjet()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<T> GetObjetByID(int id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<T> InsertObjet(T objeto)
        {
            EntitySet.Add(objeto);
            await Save();
            return objeto;
        }

        public async Task<T> DeleteObjet(int id)
        {
            T objeto = await EntitySet.FindAsync(id);
            EntitySet.Remove(objeto);
            await Save();
            return objeto;
        }

        public async Task<T> UpdateObjet(T objeto)
        {
            context.Entry(objeto).State = EntityState.Modified;
            await Save();
            return objeto;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}