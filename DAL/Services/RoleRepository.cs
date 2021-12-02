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
    public class RoleRepository : IRoleRepository
    {
        private string _connectionString;
        //= @"Data Source=DESKTOP-RGPQP6I\TFTIC2014;Initial Catalog=TechniContact;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public RoleRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }


        private Connection seConnecter()
        {
            return new Connection(_connectionString);
        }

        public Roles Convert(SqlDataReader reader)
        {
            return new Roles
            {
                Id = (int)reader["Id"],
                 Nom = reader["Nom"].ToString()
            };
        }

        public void Delete(int Id)
        {
            string query = "DELETE FROM Roles WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);

            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<Roles> GetAll()
        {
            string query = "SELECT * FROM Roles";
            Command cmd = new Command(query);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Roles> GetAllByUser(int IdUser)
        {
            string query = "SELECT c.* FROM Roles c inner join User uc on c.Id=uc.IdRoles WHERE c.IdUser=@IdUser ";
            Command cmd = new Command(query);
            cmd.AddParameter("IdUser", IdUser);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public Roles GetById(int Id)
        {
            string query = "SELECT * FROM Roles WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }

        public bool Insert(Roles c)
        {
            string query = "INSERT INTO Roles (Nom)" +
                " VALUES(@Nom)";
            Command cmd = new Command(query);
            cmd.AddParameter("Nom", c.Nom); 

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(Roles c)
        {
            string query = "UPDATE Roles SET Nom = @Nom WHERE Id = @id";
            Command cmd = new Command(query);
            cmd.AddParameter("Nom", c.Nom);
            cmd.AddParameter("id", c.Id);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }
    }
}
