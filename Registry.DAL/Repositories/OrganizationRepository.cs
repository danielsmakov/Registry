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
        private void Connection()
        {
            string connectString = ConfigurationManager.ConnectionStrings["RegistryDBConnection"].ConnectionString;
            _con = new SqlConnection(connectString);
        }
        public Organization Get(int id)
        {
            try
            {
                Connection();
                _con.Open();
                Organization org = _con.QuerySingle<Organization>("Select * From Organizations Where Id = @Id", 
                    new { 
                            id = id
                        }
                    );
                _con.Close();
                return org;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        //public class IQuery
        //{
        //    public DateTime? EndDate { get; set; }
        //}


        public class OrganizationQuery 
        {
            public string Name { get; set; }
            public bool IsDeleted { get; set; }
        }


        public List<Organization> List(OrganizationQuery query)
        {
            try
            {
                Connection();
                _con.Open();

                var where = "WHERE 1=1";
                if (query.IsDeleted) where += where + " AND EndDate is not null ";
                if (!string.IsNullOrEmpty(query.Name)) where += where + " AND Name like '%@Name%'";

                List<Organization> orgs = _con.Query<Organization>($"Select * From Organizations {where}",
                    new
                    {
                        name = query.Name
                    }
                    
                    ).ToList();
                _con.Close();
                return orgs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Create(Organization entity)
        {
            try
            {
                Connection();
                _con.Open();
                _con.Execute("Insert Into Organizations (Id, Name, BIN, PhoneNumber, Status, BeginDate, EndDate) Values (@Id, @Name, @BIN, @PhoneNumber, @Status, @BeginDate, @EndDate)", new { entity});
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
        public void Delete(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@EndDate", DateTime.Now.ToString());
                Connection();
                _con.Open();
                _con.Execute("Update Organizations Set Status = 0, EndDate = GETDATE() Where Id = @Id", param);
                _con.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
