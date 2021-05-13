﻿using Registry.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        List<T> GetAll();
        void Create(T org);
        void Update(T org);
        void Disable(string id);
    }
}
