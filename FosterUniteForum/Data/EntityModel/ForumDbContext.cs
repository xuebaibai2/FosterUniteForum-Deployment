namespace FosterUniteForum.Data.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ForumDbContext : DbContext
    {
        public ForumDbContext()
            : base("name=ForumDb")
        {
        }
        
        public virtual DbSet<AccessMask> AccessMask { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Board> Board { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<FollowForum> FollowForum { get; set; }
        public virtual DbSet<FollowTopic> FollowTopic { get; set; }
        public virtual DbSet<Forum> Forum { get; set; }
        public virtual DbSet<ForumAccess> ForumAccess { get; set; }
        public virtual DbSet<ForumTrack> ForumTrack { get; set; }
        public virtual DbSet<ForumUser> ForumUser { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupMember> GroupMember { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostReport> PostReport { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<TopicTrack> TopicTrack { get; set; }
        public virtual DbSet<DonateReceipt> DonateReceipt { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessMask>()
                .HasMany(e => e.ForumAccess)
                .WithRequired(e => e.AccessMask)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Board>()
                .HasMany(e => e.AccessMask)
                .WithRequired(e => e.Board)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Board>()
                .HasMany(e => e.Category)
                .WithRequired(e => e.Board)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Forum)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.FollowForum)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Forum1)
                .WithOptional(e => e.Forum2)
                .HasForeignKey(e => e.ParentForumId);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.ForumAccess)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.ForumTrack)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Topic1)
                .WithRequired(e => e.Forum1)
                .HasForeignKey(e => e.ForumId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.Attachment)
                .WithRequired(e => e.ForumUser)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.FollowForum)
                .WithRequired(e => e.ForumUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.FollowTopic)
                .WithRequired(e => e.ForumUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.Forum)
                .WithOptional(e => e.ForumUser)
                .HasForeignKey(e => e.LastPostUserId);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.ForumTrack)
                .WithRequired(e => e.ForumUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.GroupMember)
                .WithRequired(e => e.ForumUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.ForumUser)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.PostReport)
                .WithRequired(e => e.ForumUser)
                .HasForeignKey(e => e.ReportedById)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.PostReport1)
                .WithOptional(e => e.ForumUser1)
                .HasForeignKey(e => e.ResolvedById);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.Topic)
                .WithRequired(e => e.ForumUser)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.Topic1)
                .WithOptional(e => e.ForumUser1)
                .HasForeignKey(e => e.LastPostAuthorId);

            modelBuilder.Entity<ForumUser>()
                .HasMany(e => e.TopicTrack)
                .WithRequired(e => e.ForumUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.ForumAccess)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.GroupMember)
                .WithRequired(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Attachment)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Forum)
                .WithOptional(e => e.Post)
                .HasForeignKey(e => e.LastPostId);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Post1)
                .WithOptional(e => e.Post2)
                .HasForeignKey(e => e.ReplyToPostId);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostReport)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Topic1)
                .WithOptional(e => e.Post1)
                .HasForeignKey(e => e.LastPostId);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.FollowTopic)
                .WithRequired(e => e.Topic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Forum)
                .WithOptional(e => e.Topic)
                .HasForeignKey(e => e.LastTopicId);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Post)
                .WithRequired(e => e.Topic)
                .HasForeignKey(e => e.TopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Topic1)
                .WithOptional(e => e.Topic2)
                .HasForeignKey(e => e.OriginalTopicId);

            modelBuilder.Entity<Topic>()
                .HasMany(e => e.TopicTrack)
                .WithRequired(e => e.Topic)
                .WillCascadeOnDelete(false);
        }
    }
}
