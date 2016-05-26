namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Forum")]
    public partial class Forum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Forum()
        {
            FollowForum = new HashSet<FollowForum>();
            Forum1 = new HashSet<Forum>();
            ForumAccess = new HashSet<ForumAccess>();
            ForumTrack = new HashSet<ForumTrack>();
            Topic1 = new HashSet<Topic>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Forum name is required")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sort order number is required")]
        [RegularExpression(@"0|^[1-9][0-9]*", ErrorMessage = "Only number is allowed")]
        public int SortOrder { get; set; }

        public int? ParentForumId { get; set; }

        public string Description { get; set; }

        public string LastPosted { get; set; }

        public int? LastTopicId { get; set; }

        public int? LastPostId { get; set; }

        public int? LastPostUserId { get; set; }

        [StringLength(256)]
        public string LastPostUsername { get; set; }

        public int TopicCount { get; set; }

        public int PostCount { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowForum> FollowForum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forum> Forum1 { get; set; }

        public virtual Forum Forum2 { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual Post Post { get; set; }

        public virtual Topic Topic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumAccess> ForumAccess { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumTrack> ForumTrack { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic1 { get; set; }
    }
}
