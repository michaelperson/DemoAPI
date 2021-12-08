﻿using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IRolesBusiness<TEntity> : IBusiness<TEntity>
        where TEntity : new()
    {
         
        IEnumerable<TEntity> GetByUser(int IdUSer);
       
    }
}
