namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForumUser")]
    public partial class ForumUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForumUser()
        {
            Attachment = new HashSet<Attachment>();
            FollowForum = new HashSet<FollowForum>();
            FollowTopic = new HashSet<FollowTopic>();
            Forum = new HashSet<Forum>();
            ForumTrack = new HashSet<ForumTrack>();
            GroupMember = new HashSet<GroupMember>();
            Post = new HashSet<Post>();
            PostReport = new HashSet<PostReport>();
            PostReport1 = new HashSet<PostReport>();
            Topic = new HashSet<Topic>();
            Topic1 = new HashSet<Topic>();
            TopicTrack = new HashSet<TopicTrack>();
        }

        public int Id { get; set; }

        
        [StringLength(50)]
        public string ProviderId { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        public string FirstVisit { get; set; }

        public string LastVisit { get; set; }

        [StringLength(50)]
        public string LastIP { get; set; }

        public bool Deleted { get; set; }

        public bool Active { get; set; }
        
        [StringLength(100)]
        public string Timezone { get; set; }
        
        [StringLength(10)]
        public string Culture { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        public int UserFlag { get; set; }

        public bool UseFullName { get; set; }

        public bool ExternalAccount { get; set; }

        [StringLength(50)]
        public string ExternalProvider { get; set; }

        [StringLength(200)]
        public string ExternalProviderId { get; set; }

        [StringLength(200)]
        public string Theme { get; set; }

        [StringLength(200)]
        public string City { get; set; }

        [StringLength(200)]
        public string State { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Birthday { get; set; }

        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(200)]
        public string CompanyAddress { get; set; }

        [StringLength(200)]
        public string Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowForum> FollowForum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowTopic> FollowTopic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Forum> Forum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumTrack> ForumTrack { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupMember> GroupMember { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostReport> PostReport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostReport> PostReport1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topic1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TopicTrack> TopicTrack { get; set; }
    }
}
