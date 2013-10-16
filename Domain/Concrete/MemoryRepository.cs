using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class MemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _collection;
        private readonly object _locker;

        public MemoryRepository()
        {
            _collection = new List<T>();
            _locker = new object();
        }

        public List<T> All()
        {
            return _collection;
        }

        public IQueryable<T> Get(Func<T, bool> conditional)
        {
            return _collection.Where(conditional).AsQueryable();
        }

        public void Add(T t)
        {
            _collection.Add(t);
        }

        public void Delete(T t)
        {
            lock (_locker)
            {
                _collection.Remove(t);
            }
        }

        public int Count(Func<T, bool> conditional = null)
        {
            return conditional != null ? _collection.Count(conditional) : _collection.Count();
        }

        public void Commit()
        {
            
        }
    }
}
