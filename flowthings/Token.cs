using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flowthings
{
    public sealed class Token
    {
        public string account { get; private set; }
        internal string token { get; private set; }


        /// <summary>
        /// Creates a token to use in connecting to the flowthings API
        /// </summary>
        /// <param name="account"></param>
        /// <param name="token"></param>
        public Token(string account, string token)
        {
            this.account = account;
            this.token = token;
        }
    }
}
