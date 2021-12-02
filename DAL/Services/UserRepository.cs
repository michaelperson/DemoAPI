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
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
       
        public UserRepository(IConfiguration config):base(config)
        {
           
        }


         

        

        public void Delete(int Id)
        {
            string query = "DELETE FROM [User] WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            
            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM [User]";
            Command cmd = new Command(query);
            
            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public User GetById(int Id)
        {
            string query = "SELECT * FROM [User] WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }
        public User GetByEmail(string Email)
        {
            string query = "SELECT * FROM [User] WHERE Email = @Email";
            Command cmd = new Command(query);
            cmd.AddParameter("Email", Email);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }
        public bool Insert(User c)
        {
            string query = "INSERT INTO [User] (FirstName, LastName, Email, Password, Salt, IdRole)" +
                " VALUES(@fn, @ln, @email, @Password, @Salt, @IdRole)";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);
            cmd.AddParameter("Password", c.Password);
            cmd.AddParameter("Salt", c.Salt);
            cmd.AddParameter("IdRole", c.IdRole);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(User c)
        {
            string query = "UPDATE [User] SET FirstName = @fn, LastName = @ln, Email = @email, Password= @Password, Salt=@Salt, IdRole=@IdRole WHERE Id = @id";
            Command cmd = new Command(query);
            cmd.AddParameter("fn", c.FirstName);
            cmd.AddParameter("ln", c.LastName);
            cmd.AddParameter("email", c.Email);
            cmd.AddParameter("Password", c.Password);
            cmd.AddParameter("Salt", c.Salt);
            cmd.AddParameter("id", c.Id);
            cmd.AddParameter("IdRole", c.IdRole);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }
    }
}
