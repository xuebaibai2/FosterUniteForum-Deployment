namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attachment")]
    public partial class Attachment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Filename { get; set; }

        public int PostId { get; set; }

        public int Size { get; set; }

        public int DownloadCount { get; set; }

        [Required]
        [StringLength(500)]
        public string Path { get; set; }

        public int AuthorId { get; set; }

        public string Created { get; set; }

        public virtual ForumUser ForumUser { get; set; }

        public virtual Post Post { get; set; }
    }
}
