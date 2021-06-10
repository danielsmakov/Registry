using Registry.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        List<T> List();
        void Insert(T org);
        void Update(T org);
        void Delete(int id);
    }
}
