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
    
    public partial class StatusMer
    {
        public StatusMer()
        {
            this.Meropriyatia = new HashSet<Meropriyatia>();
        }
    
        public int ID_status { get; set; }
        public string N_status { get; set; }
    
        public virtual ICollection<Meropriyatia> Meropriyatia { get; set; }
    }
}