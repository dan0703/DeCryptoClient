using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCrypto.Domain
{
    internal class Account
    {
        private string email;
        private string password;
        private string nickName;

        public string EmailGet { get { return email; } set {  email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string NickName { get {  return nickName; } set {  nickName = value; } }

        public Account() { }

        public override string ToString()
        {
            return nickName;
        }

    }
}
