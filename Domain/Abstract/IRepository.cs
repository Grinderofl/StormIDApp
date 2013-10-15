using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        List<T> All();
        IQueryable<T> Get(Func<T, bool> conditional); 
        void Add(T t);
        void Delete(T t);
        int Count(Func<T, bool> conditional = null); 
        void Commit();
    }
}
