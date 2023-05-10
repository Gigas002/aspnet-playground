using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efpostgres;

public partial class Test1Context : DbContext
{
    public Test1Context()
    {
    }

    public Test1Context(DbContextOptions<Test1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<CharacterRelation> CharacterRelations { get; set; }

    public virtual DbSet<Circle> Circles { get; set; }

    public virtual DbSet<Creation> Creations { get; set; }

    public virtual DbSet<CreationRelation> CreationRelations { get; set; }

    public virtual DbSet<Doujinshi> Doujinshis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test1;Username=gigas");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_author_id");

            entity.ToTable("authors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalDetails).HasColumnName("additional_details");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AlternativeAuthorNames).HasColumnName("alternative_author_names");
            entity.Property(e => e.AlternativeNames).HasColumnName("alternative_names");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(256)
                .HasColumnName("author_name");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ExternalLinks).HasColumnName("external_links");
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("first_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("last_name");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Species)
                .HasMaxLength(128)
                .HasColumnName("species");

            entity.HasMany(d => d.Circles).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsCircle",
                    r => r.HasOne<Circle>().WithMany()
                        .HasForeignKey("CircleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_circle_id"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_author_id"),
                    j =>
                    {
                        j.HasKey("AuthorId", "CircleId").HasName("pk_author_circle_id");
                        j.ToTable("authors_circles");
                        j.IndexerProperty<long>("AuthorId").HasColumnName("author_id");
                        j.IndexerProperty<long>("CircleId").HasColumnName("circle_id");
                    });

            entity.HasMany(d => d.Creations).WithMany(p => p.Authors)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsCreation",
                    r => r.HasOne<Creation>().WithMany()
                        .HasForeignKey("CreationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_creation_id"),
                    l => l.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_author_id"),
                    j =>
                    {
                        j.HasKey("AuthorId", "CreationId").HasName("pk_author_creation_id");
                        j.ToTable("authors_creations");
                        j.IndexerProperty<long>("AuthorId").HasColumnName("author_id");
                        j.IndexerProperty<long>("CreationId").HasColumnName("creation_id");
                    });
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_character_id");

            entity.ToTable("characters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalDetails).HasColumnName("additional_details");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AlternativeNames).HasColumnName("alternative_names");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FirstName)
                .HasMaxLength(64)
                .HasColumnName("first_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(128)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Genitails).HasColumnName("genitails");
            entity.Property(e => e.IsCosplay).HasColumnName("is_cosplay");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("last_name");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Species)
                .HasMaxLength(128)
                .HasColumnName("species");
        });

        modelBuilder.Entity<CharacterRelation>(entity =>
        {
            entity.HasKey(e => new { e.OriginCharacterId, e.RelatedCharacterId }).HasName("pk_character_relation_id");

            entity.ToTable("character_relations");

            entity.Property(e => e.OriginCharacterId).HasColumnName("origin_character_id");
            entity.Property(e => e.RelatedCharacterId).HasColumnName("related_character_id");
            entity.Property(e => e.RelationType).HasColumnName("relation_type");

            entity.HasOne(d => d.OriginCharacter).WithMany(p => p.CharacterRelationOriginCharacters)
                .HasForeignKey(d => d.OriginCharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_relations_origin_character_id_fkey");

            entity.HasOne(d => d.RelatedCharacter).WithMany(p => p.CharacterRelationRelatedCharacters)
                .HasForeignKey(d => d.RelatedCharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("character_relations_related_character_id_fkey");
        });

        modelBuilder.Entity<Circle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_circle_id");

            entity.ToTable("circles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlternativeTitles).HasColumnName("alternative_titles");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Creation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_creation_id");

            entity.ToTable("creations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationType).HasColumnName("creation_type");

            entity.HasMany(d => d.Characters).WithMany(p => p.Creations)
                .UsingEntity<Dictionary<string, object>>(
                    "CreationsCharacter",
                    r => r.HasOne<Character>().WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_character_id"),
                    l => l.HasOne<Creation>().WithMany()
                        .HasForeignKey("CreationId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_creation_id"),
                    j =>
                    {
                        j.HasKey("CreationId", "CharacterId").HasName("pk_creation_character_id");
                        j.ToTable("creations_characters");
                        j.IndexerProperty<long>("CreationId").HasColumnName("creation_id");
                        j.IndexerProperty<long>("CharacterId").HasColumnName("character_id");
                    });
        });

        modelBuilder.Entity<CreationRelation>(entity =>
        {
            entity.HasKey(e => new { e.OriginCreationId, e.RelatedCreationId }).HasName("pk_creation_relation_id");

            entity.ToTable("creation_relations");

            entity.Property(e => e.OriginCreationId).HasColumnName("origin_creation_id");
            entity.Property(e => e.RelatedCreationId).HasColumnName("related_creation_id");
            entity.Property(e => e.RelationType).HasColumnName("relation_type");

            entity.HasOne(d => d.OriginCreation).WithMany(p => p.CreationRelationOriginCreations)
                .HasForeignKey(d => d.OriginCreationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("creation_relations_origin_creation_id_fkey");

            entity.HasOne(d => d.RelatedCreation).WithMany(p => p.CreationRelationRelatedCreations)
                .HasForeignKey(d => d.RelatedCreationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("creation_relations_related_creation_id_fkey");
        });

        modelBuilder.Entity<Doujinshi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_doujinshi_id");

            entity.ToTable("doujinshi");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeRating)
                .HasMaxLength(5)
                .HasColumnName("age_rating");
            entity.Property(e => e.AlternativeTitles).HasColumnName("alternative_titles");
            entity.Property(e => e.AvailableAt).HasColumnName("available_at");
            entity.Property(e => e.Censorship)
                .HasMaxLength(64)
                .HasColumnName("censorship");
            entity.Property(e => e.Chapters).HasColumnName("chapters");
            entity.Property(e => e.CreationId).HasColumnName("creation_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Genres)
                .HasColumnType("character varying(64)[]")
                .HasColumnName("genres");
            entity.Property(e => e.HasImages).HasColumnName("has_images");
            entity.Property(e => e.IsColored).HasColumnName("is_colored");
            entity.Property(e => e.Languages)
                .HasColumnType("character varying(64)[]")
                .HasColumnName("languages");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.RelatedTo).HasColumnName("related_to");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Status)
                .HasMaxLength(64)
                .HasColumnName("status");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Volumes).HasColumnName("volumes");

            entity.HasOne(d => d.Creation).WithMany(p => p.Doujinshis)
                .HasForeignKey(d => d.CreationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_creation_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
