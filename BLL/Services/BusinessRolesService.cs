using BLL.Interface;
using BLL.Models;
using BLL.Tools;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BusinessRolesService : IRolesBusiness<BusinessRoles>
    {
        private readonly IRoleRepository _repo;
        public BusinessRolesService(IRoleRepository repo)
        {
            _repo = repo;
        }
        public void Delete(int Id)
        {
            if(Id<1)
            {
                throw new ArgumentException("Id must be positive");
            }
            _repo.Delete(Id);
        }

        public IEnumerable<BusinessRoles> GetAll()
        {
            return _repo.GetAll().Select(m => m.ToBll());
        }

        public BusinessRoles GetById(int Id)
        {
            return _repo.GetById(Id).ToBll();
        }

        public IEnumerable<BusinessRoles> GetByUser(int IdUSer)
        {
            return _repo.GetAllByUser(IdUSer).Select(m => m.ToBll());
        }

        public void Insert(BusinessRoles c)
        {
            if(c==null)
            {
                throw new ArgumentNullException("Roles is null");
            }
            if(!_repo.Insert(c.ToDal()))
            {
                throw new InvalidOperationException("Unable to insert the role");
            }
        }

        public void Update(BusinessRoles c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("Roles is null");
            }
            if (!_repo.Update(c.ToDal()))
            {
                throw new InvalidOperationException("Unable to update the role");
            }
        }
    }
}
