using ADOLibrary;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public abstract class BaseRepository<T>
        where T:class
    {
        protected string _connectionString;
        //= @"Data Source=DESKTOP-RGPQP6I\TFTIC2014;Initial Catalog=TechniContact;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        protected Connection seConnecter()
        {
            return new Connection(_connectionString);
        }

        protected T Convert(SqlDataReader reader)
        {
            T retour =(T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo item in typeof(T).GetProperties())
            {
                item.SetValue(retour, reader[item.Name]!=DBNull.Value?reader[item.Name]:null);
            }
            return retour;
        }

    }
}
