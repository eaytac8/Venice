//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Venice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AromaUrun
    {
        public int AromaUrunID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public Nullable<int> AromaID { get; set; }
    
        public virtual Aroma Aroma { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
