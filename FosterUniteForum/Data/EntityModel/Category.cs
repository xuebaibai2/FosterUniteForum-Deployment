namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Forum = new HashSet<Forum>();
        }

        public int Id { get; set; }

        public int BoardId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Sort order number is required")]
        [RegularExpression(@"0|^[1-9][0-9]*",ErrorMessage = "Only number is allowed")]     
        public int SortOrder { get; set; }

        public virtual Board Board { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forum> Forum { get; set; }
    }
}
