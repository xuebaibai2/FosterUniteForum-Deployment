namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupMember")]
    public partial class GroupMember
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int ForumUserId { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual Group Group { get; set; }
    }
}
