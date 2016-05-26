namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Attachment = new HashSet<Attachment>();
            Forum = new HashSet<Forum>();
            Post1 = new HashSet<Post>();
            PostReport = new HashSet<PostReport>();
            Topic1 = new HashSet<Topic>();
        }

        public int Id { get; set; }

        public int Position { get; set; }

        public int Indent { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(50)]
        public string IP { get; set; }

        public string Edited { get; set; }

        public int Flag { get; set; }

        [StringLength(500)]
        public string EditReason { get; set; }

        public bool ModeratorChanged { get; set; }

        [StringLength(500)]
        public string DeleteReason { get; set; }

        public int TopicId { get; set; }

        public int? ReplyToPostId { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(256)]
        public string AuthorName { get; set; }

        public string Posted { get; set; }

        public int SpamScore { get; set; }

        public int SpamReporters { get; set; }

        public string CustomProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forum> Forum { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post1 { get; set; }

        public virtual Post Post2 { get; set; }

        public virtual Topic Topic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostReport> PostReport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic1 { get; set; }
    }
}
