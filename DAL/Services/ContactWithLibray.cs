using ADOLibrary;
using DAL.Entities;
using DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class ContactWithLibray : IContactRepoLibrary
    {
        private string _connectionString;
        //= @"Data Source=DESKTOP-RGPQP6I\TFTIC2014;Initial Catalog=TechniContact;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ContactWithLibray(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }


        private Connection seConnecter()
        {
            return new Connection(_connectionString);
        }

        public Contact Convert(SqlDataReader reader)
        {
            return new Contact
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"] is DBNull ? null : reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString()
            };
        }

        public void Delete(int Id)
        {
            string query = "DELETE FROM Contact WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            
            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<Contact> GetAll()
        {
            string query = "SELECT * FROM Contact";
            Command cmd = new Command(query);
            
            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Contact> GetAllByUser(int IdUser)
        {
            string query = "SELECT c.* FROM Contact c inner join UserContact uc on c.Id=uc.IdContact WHERE uc.IdUser=@IdUser ";
            Command cmd = new Command(query);
            cmd.AddParameter("IdUser", IdUser);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public Contact GetById(int Id)
        {
            string query = "SELECT * FROM Contact WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }

        public bool Insert(Contact c)
        {
            string query = "INSERT INTO Contact (FirstName, LastName, Email)" +
                " VALUES(@fn, @ln, @email)";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(Contact c)
        {
            string query = "UPDATE Contact SET FirstName = @fn, LastName = @ln, Email = @email WHERE Id = @id";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);
            cmd.AddParameter("id", c.Id);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }
    }
}
