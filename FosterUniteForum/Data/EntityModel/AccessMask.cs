namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccessMask")]
    public partial class AccessMask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccessMask()
        {
            ForumAccess = new HashSet<ForumAccess>();
        }

        public int Id { get; set; }

        public int BoardId { get; set; }

        [Required(ErrorMessage = "Mask name is required")]
        [StringLength(200)]
        public string Name { get; set; }

        public int AccessFlag { get; set; }

        public virtual Board Board { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumAccess> ForumAccess { get; set; }
    }
}
