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
    
    public partial class Konkurs
    {
        public int ID_konk { get; set; }
        public string N_konk { get; set; }
        public string Result_konk { get; set; }
        public int Uch_id { get; set; }
        public int Level_id { get; set; }
        public int Klass_id { get; set; }
        public System.DateTime Data_konk { get; set; }
    
        public virtual Klasses Klasses { get; set; }
        public virtual LevelKonkurs LevelKonkurs { get; set; }
        public virtual Ucheniki Ucheniki { get; set; }
    }
}