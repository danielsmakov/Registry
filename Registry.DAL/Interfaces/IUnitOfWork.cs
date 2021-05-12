﻿using Registry.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Organization> Organizations { get; }
    }
}
