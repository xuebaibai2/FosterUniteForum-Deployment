namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostReport")]
    public partial class PostReport
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Timestamp { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public int ReportedById { get; set; }

        public bool Feedback { get; set; }

        public bool Resolved { get; set; }

        public int? ResolvedById { get; set; }

        public string ResolvedTimestamp { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual ForumUser ForumUser1 { get; set; }

        public virtual Post Post { get; set; }
    }
}
