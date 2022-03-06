using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WeSharper.Models
{
    public partial class WeSharperContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly string _connectionString;
        public WeSharperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public WeSharperContext(DbContextOptions<WeSharperContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupPost> GroupPosts { get; set; } = null!;
        public virtual DbSet<GroupPostComment> GroupPostComments { get; set; } = null!;
        public virtual DbSet<GroupPostCommentReact> GroupPostCommentReacts { get; set; } = null!;
        public virtual DbSet<GroupPostReact> GroupPostReacts { get; set; } = null!;
        public virtual DbSet<GroupUser> GroupUsers { get; set; } = null!;
        public virtual DbSet<Hobby> Hobbies { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PostCommentReact> PostCommentReacts { get; set; } = null!;
        public virtual DbSet<PostReact> PostReacts { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<Userhobby> Userhobbies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.RelationshipId)
                    .HasName("PK__Friend__31FEB861C0AB61AA");

                entity.ToTable("Friend");

                entity.Property(e => e.RelationshipId).HasColumnName("RelationshipID");

                entity.Property(e => e.AcceptedUserId)
                    .HasMaxLength(450)
                    .HasColumnName("AcceptedUserID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.Relationship)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestedUserId)
                    .HasMaxLength(450)
                    .HasColumnName("RequestedUserID");

                entity.HasOne(d => d.AcceptedUser)
                    .WithMany(p => p.FriendAcceptedUsers)
                    .HasForeignKey(d => d.AcceptedUserId)
                    .HasConstraintName("FK__Friend__Accepted__56B3DD81");

                entity.HasOne(d => d.RequestedUser)
                    .WithMany(p => p.FriendRequestedUsers)
                    .HasForeignKey(d => d.RequestedUserId)
                    .HasConstraintName("FK__Friend__Requeste__55BFB948");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.GroupManagerId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupManagerID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GroupPicture).IsUnicode(false);

                entity.HasOne(d => d.GroupManager)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.GroupManagerId)
                    .HasConstraintName("FK__Groups__GroupMan__6F7F8B4B");
            });

            modelBuilder.Entity<GroupPost>(entity =>
            {
                entity.ToTable("GroupPost");

                entity.Property(e => e.GroupPostId).HasColumnName("GroupPostID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupID");

                entity.Property(e => e.PostContent).IsUnicode(false);

                entity.Property(e => e.PostPhoto).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupPost__Group__753864A1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupPosts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupPost__UserI__762C88DA");
            });

            modelBuilder.Entity<GroupPostComment>(entity =>
            {
                entity.ToTable("GroupPostComment");

                entity.Property(e => e.GroupPostCommentId).HasColumnName("GroupPostCommentID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupPostComment1)
                    .IsUnicode(false)
                    .HasColumnName("GroupPostComment");

                entity.Property(e => e.GroupPostId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupPostID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPostComments)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupPost__Group__1975C517");

                entity.HasOne(d => d.GroupPost)
                    .WithMany(p => p.GroupPostComments)
                    .HasForeignKey(d => d.GroupPostId)
                    .HasConstraintName("FK__GroupPost__Group__1A69E950");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupPostComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupPost__UserI__1B5E0D89");
            });

            modelBuilder.Entity<GroupPostCommentReact>(entity =>
            {
                entity.ToTable("GroupPostCommentReact");

                entity.Property(e => e.GroupPostCommentReactId).HasColumnName("GroupPostCommentReactID");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupPostCommentId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupPostCommentID");

                entity.Property(e => e.ReactId)
                    .HasMaxLength(450)
                    .HasColumnName("ReactID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPostCommentReacts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupPost__Group__1E3A7A34");

                entity.HasOne(d => d.GroupPostComment)
                    .WithMany(p => p.GroupPostCommentReacts)
                    .HasForeignKey(d => d.GroupPostCommentId)
                    .HasConstraintName("FK__GroupPost__Group__1F2E9E6D");

                entity.HasOne(d => d.React)
                    .WithMany(p => p.GroupPostCommentReacts)
                    .HasForeignKey(d => d.ReactId)
                    .HasConstraintName("FK__GroupPost__React__2116E6DF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupPostCommentReacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupPost__UserI__2022C2A6");
            });

            modelBuilder.Entity<GroupPostReact>(entity =>
            {
                entity.ToTable("GroupPostReact");

                entity.Property(e => e.GroupPostReactId).HasColumnName("GroupPostReactID");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupID");

                entity.Property(e => e.GroupPostId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupPostID");

                entity.Property(e => e.ReactId)
                    .HasMaxLength(450)
                    .HasColumnName("ReactID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPostReacts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupPost__Group__7908F585");

                entity.HasOne(d => d.GroupPost)
                    .WithMany(p => p.GroupPostReacts)
                    .HasForeignKey(d => d.GroupPostId)
                    .HasConstraintName("FK__GroupPost__Group__7AF13DF7");

                entity.HasOne(d => d.React)
                    .WithMany(p => p.GroupPostReacts)
                    .HasForeignKey(d => d.ReactId)
                    .HasConstraintName("FK__GroupPost__React__7BE56230");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupPostReacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupPost__UserI__79FD19BE");
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("GroupUser");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(450)
                    .HasColumnName("GroupID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__GroupUser__Group__7167D3BD");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupUser__UserI__725BF7F6");
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.ToTable("Hobby");

                entity.HasIndex(e => e.HobbyName, "UQ__Hobby__C392860017568D0F")
                    .IsUnique();

                entity.Property(e => e.HobbyId).HasColumnName("HobbyID");

                entity.Property(e => e.HobbyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Content).IsUnicode(false);

                entity.Property(e => e.DateRead).HasColumnType("smalldatetime");

                entity.Property(e => e.MessageSent).HasColumnType("smalldatetime");

                entity.Property(e => e.RecipientUserId)
                    .HasMaxLength(450)
                    .HasColumnName("RecipientUserID");

                entity.Property(e => e.SenderUserId)
                    .HasMaxLength(450)
                    .HasColumnName("SenderUserID");

                entity.HasOne(d => d.RecipientUser)
                    .WithMany(p => p.MessageRecipientUsers)
                    .HasForeignKey(d => d.RecipientUserId)
                    .HasConstraintName("FK__Message__Recipie__24E777C3");

                entity.HasOne(d => d.SenderUser)
                    .WithMany(p => p.MessageSenderUsers)
                    .HasForeignKey(d => d.SenderUserId)
                    .HasConstraintName("FK__Message__SenderU__23F3538A");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.PostContent).IsUnicode(false);

                entity.Property(e => e.PostPhoto).IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__UserID__5D60DB10");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__PostComm__C3B4DFAAE4A3070A");

                entity.ToTable("PostComment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.PostComment1)
                    .IsUnicode(false)
                    .HasColumnName("PostComment");

                entity.Property(e => e.PostId)
                    .HasMaxLength(450)
                    .HasColumnName("PostID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostComme__PostI__66EA454A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostComme__UserI__67DE6983");
            });

            modelBuilder.Entity<PostCommentReact>(entity =>
            {
                entity.ToTable("PostCommentReact");

                entity.Property(e => e.PostCommentReactId).HasColumnName("PostCommentReactID");

                entity.Property(e => e.CommentId)
                    .HasMaxLength(450)
                    .HasColumnName("CommentID");

                entity.Property(e => e.ReactId)
                    .HasMaxLength(450)
                    .HasColumnName("ReactID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.PostCommentReacts)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__PostComme__Comme__6ABAD62E");

                entity.HasOne(d => d.React)
                    .WithMany(p => p.PostCommentReacts)
                    .HasForeignKey(d => d.ReactId)
                    .HasConstraintName("FK__PostComme__React__6CA31EA0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostCommentReacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostComme__UserI__6BAEFA67");
            });

            modelBuilder.Entity<PostReact>(entity =>
            {
                entity.ToTable("PostReact");

                entity.Property(e => e.PostReactId).HasColumnName("PostReactID");

                entity.Property(e => e.PostId)
                    .HasMaxLength(450)
                    .HasColumnName("PostID");

                entity.Property(e => e.ReactId)
                    .HasMaxLength(450)
                    .HasColumnName("ReactID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostReacts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostReact__PostI__6225902D");

                entity.HasOne(d => d.React)
                    .WithMany(p => p.PostReacts)
                    .HasForeignKey(d => d.ReactId)
                    .HasConstraintName("FK__PostReact__React__640DD89F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostReacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PostReact__UserI__6319B466");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Bio).IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("smalldatetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePictureUrl)
                    .IsUnicode(false)
                    .HasColumnName("ProfilePictureURL");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Profile__UserID__4D2A7347");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.HasKey(e => e.ReactId)
                    .HasName("PK__Reaction__7661ACCFA61E2A48");

                entity.ToTable("Reaction");

                entity.Property(e => e.ReactId).HasColumnName("ReactID");

                entity.Property(e => e.ReactIcon).IsUnicode(false);

                entity.Property(e => e.ReactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userhobby>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.HobbyId)
                    .HasMaxLength(450)
                    .HasColumnName("HobbyID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Hobby)
                    .WithMany()
                    .HasForeignKey(d => d.HobbyId)
                    .HasConstraintName("FK__Userhobbi__Hobby__52E34C9D");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Userhobbi__UserI__51EF2864");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
