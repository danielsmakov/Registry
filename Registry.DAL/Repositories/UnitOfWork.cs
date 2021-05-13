using Registry.DAL.Entities;
using Registry.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["RegistryDBConnection"].ConnectionString;
        private IDbConnection db = new SqlConnection(connectionString);
        private OrganizationRepository orgRepository;
        private ServiceRepository servRepository;
        public IRepository<Organization> Organizations
        {
            get
            {
                if (orgRepository == null)
                    orgRepository = new OrganizationRepository();
                return orgRepository;
            }
        }
        public IRepository<Service> Services
        {
            get
            {
                if (servRepository == null)
                    servRepository = new ServiceRepository();
                return servRepository;
            }
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
