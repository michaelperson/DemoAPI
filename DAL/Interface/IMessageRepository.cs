using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IMessageRepository : IRepository<Messages, int>
    {

        IEnumerable<Messages> GetBySender(int Id);
        IEnumerable<Messages> GetByGroups(int Id);
        IEnumerable<Messages> GetByRecipient(int Id);
        IEnumerable<Messages> GetByRecipientAndSender(int Id, int IdRecipient);
        IEnumerable<Messages> GetByGroupAndSender(int Id, int IdGroupe);
    }
}
