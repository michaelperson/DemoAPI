﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IContactRepository: IRepository<Contact, int>
    {
        
    }
}
