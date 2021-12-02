using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IRolesBusiness<TEntity>
        where TEntity : new()
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int Id);
        IEnumerable<TEntity> GetByUser(int IdUSer);
        void Delete(int Id);
        void Insert(TEntity c);
        void Update(TEntity c);
    }
}
