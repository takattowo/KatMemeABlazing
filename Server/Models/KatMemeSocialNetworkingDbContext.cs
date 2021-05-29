using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KatMemeABlazing.Server.Models
{
    public partial class KatMemeSocialNetworkingDbContext : DbContext
    {
        public KatMemeSocialNetworkingDbContext()
        {
        }

        public KatMemeSocialNetworkingDbContext(DbContextOptions<KatMemeSocialNetworkingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<KatComment> KatComments { get; set; }
        public virtual DbSet<KatPost> KatPosts { get; set; }
        public virtual DbSet<KatSection> KatSections { get; set; }
        public virtual DbSet<KatUser> KatUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=TAKATTOWO;Initial Catalog=KatMemeSocialNetworkingDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<KatComment>(entity =>
            {
                entity.ToTable("KatComment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentAuthor).HasColumnName("comment_author");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("comment_content");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.NegativeVoteCount).HasColumnName("negative_vote_count");

                entity.Property(e => e.PositiveVoteCount).HasColumnName("positive_vote_count");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.CommentAuthorNavigation)
                    .WithMany(p => p.KatComments)
                    .HasForeignKey(d => d.CommentAuthor)
                    .HasConstraintName("FK__KatCommen__comme__3B75D760");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.KatComments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__KatCommen__post___3A81B327");
            });

            modelBuilder.Entity<KatPost>(entity =>
            {
                entity.ToTable("KatPost");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.IsPromoted).HasColumnName("is_promoted");

                entity.Property(e => e.NegativeVoteCount).HasColumnName("negative_vote_count");

                entity.Property(e => e.PositiveVoteCount).HasColumnName("positive_vote_count");

                entity.Property(e => e.PostAuthor).HasColumnName("post_author");

                entity.Property(e => e.PostContent)
                    .HasMaxLength(2083)
                    .IsUnicode(false)
                    .HasColumnName("post_content");

                entity.Property(e => e.PostImage)
                    .HasMaxLength(2083)
                    .IsUnicode(false)
                    .HasColumnName("post_image");

                entity.Property(e => e.PostSectionId).HasColumnName("post_section_id");

                entity.Property(e => e.PostTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("post_title");

                entity.HasOne(d => d.PostAuthorNavigation)
                    .WithMany(p => p.KatPosts)
                    .HasForeignKey(d => d.PostAuthor)
                    .HasConstraintName("FK__KatPost__post_au__37A5467C");

                entity.HasOne(d => d.PostSection)
                    .WithMany(p => p.KatPosts)
                    .HasForeignKey(d => d.PostSectionId)
                    .HasConstraintName("FK__KatPost__post_se__36B12243");
            });

            modelBuilder.Entity<KatSection>(entity =>
            {
                entity.ToTable("KatSection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.SectionDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("section_description");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("section_name");

                entity.Property(e => e.SectionPicture)
                    .HasMaxLength(2083)
                    .IsUnicode(false)
                    .HasColumnName("section_picture");
            });

            modelBuilder.Entity<KatUser>(entity =>
            {
                entity.ToTable("KatUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_state");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.CustomStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("custom_status");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("display_name");

                entity.Property(e => e.DisplayPicture)
                    .HasMaxLength(2083)
                    .IsUnicode(false)
                    .HasColumnName("display_picture");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.JoinedDate)
                    .HasColumnType("date")
                    .HasColumnName("joined_date");

                entity.Property(e => e.JoinedSource)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("joined_source");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
