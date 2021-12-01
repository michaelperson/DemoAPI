using BLL.Interface;
using BLL.Models;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Tools;

namespace BLL.Services
{
    public class BusinessUserService : IUserBusiness<BusinessUser>
    {
        private IUserRepository _userRepo;

        public BusinessUserService(IUserRepository cr)
        {
            _userRepo = cr;
        }

        public void Delete(int Id)
        {
            if(Id == 1)
            {
                throw new ArgumentException("On ne peut supprimer l'utilisateur ID 1");
            }
            _userRepo.Delete(Id);
        }

        public IEnumerable<BusinessUser> GetAll()
        {
            return _userRepo.GetAll().Select(c => c.ToBll());
        }

        public BusinessUser GetById(int Id)
        {
            return _userRepo.GetById(Id).ToBll();
        }
        public BusinessUser GetByEmail(string Email)
        {
            return _userRepo.GetByEmail(Email).ToBll();
        }
        public void Insert(BusinessUser c)
        {
            _userRepo.Insert(c.ToDal());
        }

        public void Update(BusinessUser c)
        {
            _userRepo.Update(c.ToDal());
        }
    }
}
