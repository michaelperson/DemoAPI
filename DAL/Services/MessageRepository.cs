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
    public class MessageRepository : BaseRepository<Messages>, IMessageRepository
    {
        public MessageRepository(IConfiguration config): base (config)
        {

        }
        public void Delete(int Id)
        {
            string query = "DELETE FROM Messages WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);

            seConnecter().ExecuteNonQuery(cmd);
        }

        public IEnumerable<Messages> GetAll()
        {
            string query = "SELECT * FROM Messages";
            Command cmd = new Command(query);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Messages> GetByGroupAndSender(int Id, int IdGroup)
        {
            string query = "SELECT * FROM Messages Where Sender=@id and ToGroup=@idGRoup";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            cmd.AddParameter("idGRoup", IdGroup);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Messages> GetByGroups(int Id)
        {
            string query = "SELECT * FROM Messages WHERE ToGroup = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert);
        }

        public Messages GetById(int Id)
        {
            string query = "SELECT * FROM Messages WHERE Id = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert).FirstOrDefault();
        }

        public IEnumerable<Messages> GetByRecipient(int Id)
        {
            string query = "SELECT * FROM Messages WHERE Recipient = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Messages> GetByRecipientAndSender(int Id, int IdRecipient)
        {
            string query = "SELECT * FROM Messages Where Sender=@id and Recipient=@idIdRecipient";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            cmd.AddParameter("Id", IdRecipient);

            return seConnecter().ExecuteReader(cmd, Convert);
        }

        public IEnumerable<Messages> GetBySender(int Id)
        {
            string query = "SELECT * FROM Messages WHERE Sender = @Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Convert);
        }

        public bool Insert(Messages c)
        {
            string query = "INSERT INTO Messages (Message, Sender, Recipient, ToGroup)" +
               " VALUES(@Message, @Sender, @Recipient, @ToGroup)";
            Command cmd = new Command(query);
            cmd.AddParameter("Message", c.Message);
            cmd.AddParameter("Sender", c.Sender);
            cmd.AddParameter("Recipient", c.Recipient);
            cmd.AddParameter("ToGroup", c.ToGroup); 

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }

        public bool Update(Messages c)
        {
            string query = "UPDATE Messages set Message=@Message, Sender=@Sender, Recipient=@Recipient, ToGroup=@ToGroup" +
              " WHERE Id=@Id";
            Command cmd = new Command(query);
            cmd.AddParameter("Message", c.Message);
            cmd.AddParameter("Sender", c.Sender);
            cmd.AddParameter("Recipient", c.Recipient);
            cmd.AddParameter("ToGroup", c.ToGroup);
            cmd.AddParameter("Id", c.ID);

            Connection conn = new Connection(_connectionString);
            return conn.ExecuteNonQuery(cmd) == 1;
        }
    }
}
