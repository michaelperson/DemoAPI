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
    public class BusinessGroupeService : IGroupesBusiness<BusinessGroupes>
    {
        private readonly IGroupesRepository _repo;
        public BusinessGroupeService(IGroupesRepository repo)
        {
            _repo = repo;
        }
        public void Delete(int Id)
        {
            if (Id < 1)
            {
                throw new ArgumentException("Id must be positive");
            }
            _repo.Delete(Id);
        }

        public IEnumerable<BusinessGroupes> GetAll()
        {
            return _repo.GetAll().Select(m => m.ToBll());
        }

        public BusinessGroupes GetById(int Id)
        {
            return _repo.GetById(Id).ToBll();
        }

        public IEnumerable<BusinessGroupes> GetByUser(int IdUSer)
        {
            return _repo.GetByUser(IdUSer).Select(m => m.ToBll());
        }

        public void Insert(BusinessGroupes c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("Groupe is null");
            }
            if (!_repo.Insert(c.ToDal()))
            {
                throw new InvalidOperationException("Unable to insert the Groupe");
            }
        }

        public void Update(BusinessGroupes c)
        {
            if (c == null)
            {
                throw new ArgumentNullException("Groupe is null");
            }
            if (!_repo.Update(c.ToDal()))
            {
                throw new InvalidOperationException("Unable to update the Groupe");
            }
        }
    }
}
