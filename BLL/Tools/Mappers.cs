using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tools
{
    public static class Mappers
    {
        public static BusinessContact ToBll(this Contact contact)
        {
            return new BusinessContact
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName, 
                Email = contact.Email
            };
        }

        public static Contact ToDal(this BusinessContact contact)
        {
            return new Contact
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                Email = contact.Email
            };
        }

        public static BusinessUser ToBll(this User user)
        {
            return new BusinessUser
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Password=user.Password,
                Salt = user.Salt,
                IdRole= user.IdRole
            };
        }

        public static User ToDal(this BusinessUser user)
        {
            return new User
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Password= user.Password,
                Salt = user.Salt,
                IdRole = user.IdRole
            };
        }

        public static BusinessRoles ToBll(this Roles role)
        {
            return new BusinessRoles
            {
                Id = role.Id,
                Nom=role.Nom
            };
        }

        public static Roles ToDal(this BusinessRoles role)
        {
            return new Roles
            {
                Id = role.Id,
                Nom = role.Nom
            };
        }

    }
}
