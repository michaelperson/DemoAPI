using BLL.Models;
using DemoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Tools
{
    public static class Mappers
    {
        public static ContactForm ToApi(this BusinessContact bc)
        {
            if (bc == null) return null;
            return new ContactForm
            {
                Id = bc.Id,
                Email = bc.Email,
                LastName = bc.LastName,
                FirstName= bc.FirstName
            };
        }

        public static BusinessContact ToBLL(this ContactForm c)
        {
            if (c == null) return null;
            return new BusinessContact
            {
                Id = c.Id,
                Email = c.Email,
                LastName = c.LastName,
                FirstName = c.FirstName
            };
        }

        public static UserModel  ToApi(this BusinessUser bu)
        {
            if (bu == null) return null;
            return new UserModel
            {
                Id = bu.Id,
                Email = bu.Email,
                LastName = bu.LastName,
                FirstName = bu.FirstName,
                 Password= bu.Password, 
                 Salt= bu.Salt,

                IdRole = bu.IdRole,
                SignalRConnectionId=bu.SignalRConnectionId
            };
        }

        public static BusinessUser ToBLL(this UserModel u)
        {
            if (u == null) return null;
            return new BusinessUser
            {
                Id = u.Id,
                Email = u.Email,
                LastName = u.LastName,
                FirstName = u.FirstName,
                 Password = u.Password,
                Salt = u.Salt,
                IdRole= u.IdRole,
                SignalRConnectionId=u.SignalRConnectionId
            };
        }

        public static RolesModel ToApi(this BusinessRoles bc)
        {
            if (bc == null) return null;
            return new RolesModel
            {
                Id = bc.Id,
                Nom = bc.Nom, 
            };
        }

        public static BusinessRoles ToBLL(this RolesModel c)
        {
            if (c == null) return null;
            return new BusinessRoles
            {
                Id = c.Id,
                Nom = c.Nom,
            };
        }
    }
}
