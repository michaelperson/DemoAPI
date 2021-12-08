using BLL.Models;
using DAL.Interface;
using System.Threading.Tasks;

namespace SignalRTool.Interfaces
{
    public interface ISignalRMngt
    {
        string ExtractEmailFromJwt();
        BusinessUser FromConnectionId(string SignalRConnectionId, IUserRepository ur);
        string GetConnectionId(int UserId, IUserRepository ur); 
    }
}