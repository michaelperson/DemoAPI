using ADOLibrary;
using DAL.Entities;
using DAL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{ 
    public class GroupesRepository : BaseRepository<Groupes>, IGroupesRepository
    {
        public GroupesRepository(IConfiguration config) : base(config)
        {

        }






        public void Delete(int Id)
        {
            string query = "DELETE FROM [User] WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);

            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<Groupes> GetAll()
        {
            string query = "SELECT * FROM [Groupes]";
            Command cmd = new Command(query);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public Groupes GetById(int Id)
        {
            string query = "SELECT * FROM [Groupes] WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }
      
        public bool Insert(Groupes c)
        {
            string query = "INSERT INTO [Groupes] (Nom)" +
                " VALUES(@Nom)";
            Command cmd = new Command(query);
            cmd.AddParameter("Nom", c.Nom); 

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(Groupes c)
        {
            string query = "UPDATE [Groupes] SET Nom = @Nom WHERE Id = @id";
            Command cmd = new Command(query);
            cmd.AddParameter("Nom", c.Nom); 
            cmd.AddParameter("id", c.Id); 

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public IEnumerable<Groupes> GetByUser(int UserId)
        {
            string query = "SELECT Groupes.* FROM [Groupes] inner join [GroupeUser] on Id=IdGroupe WHERE GroupeUser.IdUser = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", UserId);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert);
        }
    }
}