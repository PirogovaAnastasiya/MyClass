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
    
    public partial class Roditeli
    {
        public Roditeli()
        {
            this.RodUch = new HashSet<RodUch>();
        }
    
        public int ID_rod { get; set; }
        public string F_rod { get; set; }
        public string I_rod { get; set; }
        public string O_rod { get; set; }
        public string Phone_rod { get; set; }
        public string Mestorab_rod { get; set; }
        public int Obr_id { get; set; }
        public int Vidrod_id { get; set; }
    
        public virtual Obrazovanie Obrazovanie { get; set; }
        public virtual Vidrods Vidrods { get; set; }
        public virtual ICollection<RodUch> RodUch { get; set; }
    }
}
