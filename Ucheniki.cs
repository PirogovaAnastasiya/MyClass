//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClass
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ucheniki
    {
        public Ucheniki()
        {
            this.Konkurs = new HashSet<Konkurs>();
            this.RodUch = new HashSet<RodUch>();
        }
    
        public int ID_uch { get; set; }
        public string F_uch { get; set; }
        public string I_uch { get; set; }
        public string O_uch { get; set; }
        public System.DateTime Datarozh_uch { get; set; }
        public string Adres_uch { get; set; }
        public string Phone_uch { get; set; }
        public string Image_uch { get; set; }
        public string Zach_uch { get; set; }
        public string Otch_uch { get; set; }
        public string Nation_uch { get; set; }
        public int Pol_id { get; set; }
        public int Language_id { get; set; }
        public int Klass_id { get; set; }
        public int Dock_id { get; set; }
        public int Fam_id { get; set; }
    
        public virtual Dock Dock { get; set; }
        public virtual Family Family { get; set; }
        public virtual Klasses Klasses { get; set; }
        public virtual ICollection<Konkurs> Konkurs { get; set; }
        public virtual Languages Languages { get; set; }
        public virtual Pol Pol { get; set; }
        public virtual ICollection<RodUch> RodUch { get; set; }
    }
}
