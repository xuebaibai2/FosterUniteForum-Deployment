namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TopicTrack")]
    public partial class TopicTrack
    {
        public int Id { get; set; }

        public string LastViewed { get; set; }

        public int TopicId { get; set; }

        public int ForumUserId { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
