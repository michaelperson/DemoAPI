using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IGroupesBusiness<TEntity> : IBusiness<TEntity>
        where TEntity : new()
    {
        public IEnumerable<TEntity> GetByUser(int IdUser);
    }
}
