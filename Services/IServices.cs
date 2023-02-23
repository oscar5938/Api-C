using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.Services{

    public interface IServices<T> : IDisposable where T : class{
         Task<IEnumerable<T>> GetObjet();
         Task<T> GetObjetByID(int? id);
         Task<T> InsertObjet(T objeto);
          Task<T> DeleteObjet(int id);
         Task<T> UpdateObjet(T objeto);
    }
}