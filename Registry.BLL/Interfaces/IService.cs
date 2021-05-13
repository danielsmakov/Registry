using Registry.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        T Get(string id);
        List<T> GetAll();
        void Create(T DTO);
        void Update(T DTO);
        void Disable(string id);
        void Dispose();
    }
}
