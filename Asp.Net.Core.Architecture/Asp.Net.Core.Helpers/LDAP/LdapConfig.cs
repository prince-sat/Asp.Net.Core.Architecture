using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Helpers.LDAP
{
    public class LdapConfig
    {
        public bool Active { get; set; }
        public string DefaultDomain { get; set; }
        public string LDAPServer { get; set; }
        public int LDAPPort { get; set; }
        public string CookieName { get; set; }
        public string searchBase { get; set; }
        public string searchFilter { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
