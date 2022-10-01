using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass
{
    public partial class Ucheniki
    {
        public override string ToString()
        {
            //return base.ToString();
            return F_uch + " " + I_uch + " " + O_uch;
        }

    }
    public partial class KlRuks
    {
        public override string ToString()
        {
            return F_klruk + " " + I_klruk + " " + O_klruk;
        }
    }


  /*  public partial class Klasses
    {
        public override string ToString()
        {
            //return base.ToString();
            return KlRuks.F_klruk;
        }

    } */

}
