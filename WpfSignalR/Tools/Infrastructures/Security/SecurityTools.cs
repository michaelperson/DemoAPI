using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WpfSignalR.Tools.Infrastructures.Security
{
    public static class SecurityTools
    {
        public static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static SecureString ConvertToSecureString(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var securePassword = new SecureString();

            foreach (char c in value)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }
    
        public static bool ExtractInfoFromJwt(SecureString token)
        {
            try
            {
                if (token == null) return false;
                //Extract Id from Jwt
                string jwt = SecureStringToString(token);
                JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
                JwtSecurityToken Infos = Handler.ReadToken(jwt.Replace("\"", "")) as JwtSecurityToken;

                Global.UserInfos.Id = int.Parse(Infos.Id);
                Global.UserInfos.Role = Infos.Claims.FirstOrDefault(m => m.Subject.Name == "Role").Value;
                Global.UserInfos.Name = Infos.Claims.FirstOrDefault(m => m.Subject.Name == "Name").Value;
                Global.UserInfos.Email = Infos.Claims.FirstOrDefault(m => m.Subject.Name == "Email").Value;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
