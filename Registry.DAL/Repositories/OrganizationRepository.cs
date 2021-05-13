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
    public class OrganizationRepository : IRepository<Organization>
    {
        private SqlConnection _con;
        /*public OrganizationRepository()
        {
            List<Organization> orgs = GetAll();
            if (!orgs.Any())
            {
                Organization org = new Organization { Id = Guid.NewGuid().ToString(), Name = "Google", BIN = "749583740162", PhoneNumber = "+18239433437", Status = 1, BeginDate = DateTime.Now.ToString() };
                Create(org);
            }
        }*/


        // Constructor below is to DELETE ALL records in the table
        /*public OrganizationRepository()
        {
            Connection();
            _con.Open();
            _con.Execute("Delete From Organizations");
            _con.Close();
        }*/
        private void Connection()
        {
            string connectString = ConfigurationManager.ConnectionStrings["RegistryDBConnection"].ConnectionString;
            _con = new SqlConnection(connectString);
        }
        public Organization Get(string id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                Connection();
                _con.Open();
                Organization org = _con.QuerySingle<Organization>("Select * From Organizations Where Id = @Id", param);
                _con.Close();
                return org;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<Organization> GetAll()
        {
            try
            {
                Connection();
                _con.Open();
                List<Organization> orgs = _con.Query<Organization>("Select * From Organizations Where Status = 1").ToList();
                _con.Close();
                return orgs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Create(Organization org)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", org.Id);
                param.Add("@Name", org.Name);
                param.Add("@BIN", org.BIN);
                param.Add("@PhoneNumber", org.PhoneNumber);
                param.Add("@Status", org.Status);
                param.Add("@BeginDate", org.BeginDate);
                param.Add("@EndDate", org.EndDate);
                Connection();
                _con.Open();
                _con.Execute("Insert Into Organizations (Id, Name, BIN, PhoneNumber, Status, BeginDate, EndDate) Values (@Id, @Name, @BIN, @PhoneNumber, @Status, @BeginDate, @EndDate)", param);
                _con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Organization org)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", org.Id);
                param.Add("@Name", org.Name);
                param.Add("@BIN", org.BIN);
                param.Add("@PhoneNumber", org.PhoneNumber);
                /*param.Add("@Status", 1);*/
                param.Add("@BeginDate", org.BeginDate);
                /*param.Add("@EndDate", org.EndDate);*/
                Connection();
                _con.Open();
                _con.Execute("Update Organizations Set Name = @Name, BIN = @BIN, PhoneNumber = @PhoneNumber, BeginDate = @BeginDate Where Id = @Id", param);
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
                _con.Execute("Update Organizations Set Status = 0, EndDate = @EndDate Where Id = @Id", param);
                _con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
