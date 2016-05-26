namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Topic")]
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            FollowTopic = new HashSet<FollowTopic>();
            Forum = new HashSet<Forum>();
            Post = new HashSet<Post>();
            Topic1 = new HashSet<Topic>();
            TopicTrack = new HashSet<TopicTrack>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int ViewCount { get; set; }

        public int PostCount { get; set; }

        public int Flag { get; set; }

        public int Type { get; set; }

        public string LastPosted { get; set; }

        public int? LastPostId { get; set; }

        public int? LastPostAuthorId { get; set; }

        [StringLength(256)]
        public string LastPostUsername { get; set; }

        public int ForumId { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(256)]
        public string AuthorName { get; set; }

        public string Posted { get; set; }

        public int SpamScore { get; set; }

        public int SpamReporters { get; set; }

        public int? OriginalTopicId { get; set; }

        public string CustomProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowTopic> FollowTopic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forum> Forum { get; set; }

        public virtual Forum Forum1 { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual ForumUser ForumUser1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }

        public virtual Post Post1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic1 { get; set; }

        public virtual Topic Topic2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TopicTrack> TopicTrack { get; set; }
    }
}
