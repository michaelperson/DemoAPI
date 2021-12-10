using BLL.Interface;
using BLL.Models;
using DAL.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRTool.Interfaces
{
    public interface ISignalRMngt
    {
        string ExtractEmailFromJwt();
        BusinessUser FromConnectionId(string SignalRConnectionId, IUserRepository ur);
        string GetConnectionId(int UserId, IUserRepository ur);
        IEnumerable<string> GetGroups(int userId, IGroupesBusiness<BusinessGroupes> gr);
    }
}