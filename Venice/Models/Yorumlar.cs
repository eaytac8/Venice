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
    
    public partial class Yorumlar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Yorumlar()
        {
            this.YorumTablo = new HashSet<YorumTablo>();
        }
    
        public int YorumID { get; set; }
        public string KonuBaslik { get; set; }
        public string KonuIcerik { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YorumTablo> YorumTablo { get; set; }
    }
}