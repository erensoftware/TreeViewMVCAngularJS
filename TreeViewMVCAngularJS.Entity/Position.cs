//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TreeViewMVCAngularJS.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Position()
        {
            this.Position1 = new HashSet<Position>();
        }
    
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> SuperiorId { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Position> Position1 { get; set; }
        public virtual Position Position2 { get; set; }
    }
}
