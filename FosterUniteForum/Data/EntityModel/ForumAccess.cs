namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForumAccess")]
    public partial class ForumAccess
    {
        public int Id { get; set; }

        public int ForumId { get; set; }

        public int GroupId { get; set; }

        public int AccessMaskId { get; set; }

        public virtual AccessMask AccessMask { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual Group Group { get; set; }
    }
}
