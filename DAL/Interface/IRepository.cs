using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRepository<T,TKey>
        where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        bool Insert(T c);
        bool Update(T c);
        void Delete(TKey Id);
         
    }
}
