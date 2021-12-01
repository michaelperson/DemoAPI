using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int Id);
        User GetByEmail(string Email);
        bool Insert(User c);
        bool Update(User c);
        void Delete(int Id);
    }
}
