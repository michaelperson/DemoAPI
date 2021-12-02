using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Roles> GetAll();
        Roles GetById(int Id); 
        bool Insert(Roles c);
        bool Update(Roles c);
        void Delete(int Id);
        IEnumerable<Roles> GetAllByUser(int IdUser);


    }
}
