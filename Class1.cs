using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass
{
    static class Bduser
    {
        public static Users user { get; set; }
        public static Users SetUser()
        {
            Users _user = user;
            return _user;
        }

    }
}
