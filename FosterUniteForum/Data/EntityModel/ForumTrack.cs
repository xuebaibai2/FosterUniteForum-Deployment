namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForumTrack")]
    public partial class ForumTrack
    {
        public int Id { get; set; }

        public string LastViewed { get; set; }

        public int ForumId { get; set; }

        public int ForumUserId { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual ForumUser ForumUser { get; set; }
    }
}
