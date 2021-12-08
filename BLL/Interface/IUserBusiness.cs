using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserBusiness<TEntity> :IBusiness<TEntity>
        where TEntity : new()
    {
         
        TEntity GetByEmail(string Email);
         
    }
}
