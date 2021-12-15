using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Repositories.Interface
{
    public interface IRepository<T, X>
       where T : class
    {
        Task<List<T>> Get();
        Task<T> Get(X id);
        HttpStatusCode Post(T entity);
        HttpStatusCode Put(X id, T entity);
        HttpStatusCode Delete(X id);
    }
}
