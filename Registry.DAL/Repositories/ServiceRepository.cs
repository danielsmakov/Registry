using Dapper;
using Registry.DAL.Entities;
using Registry.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Repositories
{
    class ServiceRepository : IRepository<Service>
    {
        private SqlConnection _con;
        private void Connection()
        {
            string connectString = ConfigurationManager.ConnectionStrings["RegistryDBConnection"].ConnectionString;
            _con = new SqlConnection(connectString);
        }
        public Service Get(string id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                Connection();
                _con.Open();
                Service serv = _con.QuerySingle<Service>("Select * From Services Where Id = @Id", param);
                _con.Close();
                return serv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Service> GetAll()
        {
            try
            {
                Connection();
                _con.Open();
                List<Service> servs = _con.Query<Service>("Select * From Services Where Status = 1").ToList();
                _con.Close();
                return servs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Create(Service serv)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", serv.Id);
                param.Add("@Name", serv.Name);
                param.Add("@Code", serv.Code);
                param.Add("@Price", serv.Price);
                param.Add("@Status", serv.Status);
                param.Add("@BeginDate", serv.BeginDate);
                param.Add("@EndDate", serv.EndDate);
                Connection();
                _con.Open();
                _con.Execute("Insert Into Services (Id, Name, Code, Price, Status, BeginDate, EndDate) Values (@Id, @Name, @Code, @Price, @Status, @BeginDate, @EndDate)", param);
                _con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Service serv)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", serv.Id);
                param.Add("@Name", serv.Name);
                param.Add("@Code", serv.Code);
                param.Add("@Price", serv.Price);
                param.Add("@Status", 1);
                param.Add("@BeginDate", serv.BeginDate);
                param.Add("@EndDate", serv.EndDate);
                Connection();
                _con.Open();
                _con.Execute("Update Services Set Name = @Name, Code = @Code, Price = @Price, BeginDate = @BeginDate Where Id = @Id", param);
                _con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Disable(string id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@EndDate", DateTime.Now.ToString());
                Connection();
                _con.Open();
                _con.Execute("Update Services Set Status = 0, EndDate = @EndDate Where Id = @Id", param);
                _con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
