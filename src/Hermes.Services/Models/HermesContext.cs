﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hermes.Models;

public partial class HermesContext : DbContext
{
    public HermesContext(DbContextOptions<HermesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LearningObjective> LearningObjectives { get; set; }

    public virtual DbSet<Presentation> Presentations { get; set; }

    public virtual DbSet<PresentationStatus> PresentationStatuses { get; set; }

    public virtual DbSet<PresentationTag> PresentationTags { get; set; }

    public virtual DbSet<PresentationText> PresentationTexts { get; set; }

    public virtual DbSet<PresentationType> PresentationTypes { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageCode).HasName("pkcLangauge");

            entity.ToTable("Language", tb => tb.HasComment("Represents a spoken/written language."));

            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the language.");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the language is enabled.");
            entity.Property(e => e.LanguageName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Name of the language.");
            entity.Property(e => e.NativeName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Native name of the language.");
        });

        modelBuilder.Entity<LearningObjective>(entity =>
        {
            entity.HasKey(e => e.LearningObjectiveId).HasName("pkcLearningObjective");

            entity.ToTable("LearningObjective", tb => tb.HasComment("Represents a learning objective of a presentation."));

            entity.HasIndex(e => new { e.PresentationTextId, e.SortOrder }, "ucLearningObjective").IsUnique();

            entity.Property(e => e.LearningObjectiveId).HasComment("The identifier of the learning objective record.");
            entity.Property(e => e.LearningObjectiveText)
                .IsRequired()
                .HasMaxLength(1000)
                .HasComment("The text of the learning objective.");
            entity.Property(e => e.PresentationTextId).HasComment("The identifier of the associated presentation (text) record.");
            entity.Property(e => e.SortOrder).HasComment("The sorting order of the learning objective.");

            entity.HasOne(d => d.PresentationText).WithMany(p => p.LearningObjectives)
                .HasForeignKey(d => d.PresentationTextId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkLearningObjective_PresentationText");
        });

        modelBuilder.Entity<Presentation>(entity =>
        {
            entity.HasKey(e => e.PresentationId).HasName("pkcPresentation");

            entity.ToTable("Presentation", tb => tb.HasComment("Represents the speaker's presentations."));

            entity.HasIndex(e => e.Permalink, "unqPresentation_Permalink").IsUnique();

            entity.Property(e => e.PresentationId).HasComment("The identifier of the presentation record.");
            entity.Property(e => e.DefaultLanguageCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("en")
                .IsFixedLength()
                .HasComment("The default language to use for the presentation.");
            entity.Property(e => e.IncludeInPublicProfile)
                .HasDefaultValue(true)
                .HasComment("Flag indicating whether the presentation is to be include in the public profile.");
            entity.Property(e => e.IsArchived).HasComment("Flag indicating whether the presentation has been archived.");
            entity.Property(e => e.OriginalThumbnailUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("The original thumbnail image for the presentation.");
            entity.Property(e => e.Permalink)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("The permament link for the presentation.");
            entity.Property(e => e.PresentationStatusId)
                .HasDefaultValue(1)
                .HasComment("Identifier of the status of the presentation is represented.");
            entity.Property(e => e.PresentationTypeId).HasComment("Identifier of the type of presentation is represented.");
            entity.Property(e => e.PrivateRepoLink)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("The link to the private repository for the presentation.");
            entity.Property(e => e.PublicRepoLink)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("The link to the public repository for the presentation.");
            entity.Property(e => e.Resources)
                .HasMaxLength(3000)
                .HasComment("The resources for the presentation.");

            entity.HasOne(d => d.DefaultLanguageCodeNavigation).WithMany(p => p.Presentations)
                .HasForeignKey(d => d.DefaultLanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentation_Language");

            entity.HasOne(d => d.PresentationStatus).WithMany(p => p.Presentations)
                .HasForeignKey(d => d.PresentationStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentation_PresentationStatus");

            entity.HasOne(d => d.PresentationType).WithMany(p => p.Presentations)
                .HasForeignKey(d => d.PresentationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentation_PresentationType");
        });

        modelBuilder.Entity<PresentationStatus>(entity =>
        {
            entity.HasKey(e => e.PresentationStatusId).HasName("pkcPresentationStatus");

            entity.ToTable("PresentationStatus", tb => tb.HasComment("Represents the status of a speaker's presentation."));

            entity.Property(e => e.PresentationStatusId)
                .ValueGeneratedNever()
                .HasComment("The identifier of the presentation status record.");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the presentation status is enabled.");
            entity.Property(e => e.PresentationIsArchived).HasComment("Flag indicating whether the presentation status has been archived.");
            entity.Property(e => e.PresentationStatusName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("The name of the presentation status.");
            entity.Property(e => e.SortOrder).HasComment("The order in which the presentation status should be displayed.");
        });

        modelBuilder.Entity<PresentationTag>(entity =>
        {
            entity.HasKey(e => e.PresentationTagId).HasName("pkcPresentationTag");

            entity.ToTable("PresentationTag", tb => tb.HasComment("Represents the association between a presentation and a tag."));

            entity.HasIndex(e => new { e.PresentationId, e.TagId }, "unqPresentationTag_PresentationId_TagId").IsUnique();

            entity.Property(e => e.PresentationTagId).HasComment("The identifier of the presentation/tag record.");
            entity.Property(e => e.PresentationId).HasComment("Identifier of the associated presentation.");
            entity.Property(e => e.TagId).HasComment("Identifier of the associated tag.");

            entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationTags)
                .HasForeignKey(d => d.PresentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentationTag_Presentation");

            entity.HasOne(d => d.Tag).WithMany(p => p.PresentationTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentationTag_Tag");
        });

        modelBuilder.Entity<PresentationText>(entity =>
        {
            entity.HasKey(e => e.PresentationTextId).HasName("pkcPresentationText");

            entity.ToTable("PresentationText", tb => tb.HasComment("The text for a presentation details in a specific language."));

            entity.HasIndex(e => new { e.PresentationId, e.LanguageCode }, "unqPresentationText_PresentationId_LanguageCode").IsUnique();

            entity.Property(e => e.PresentationTextId).HasComment("The identifier of the presentation text record.");
            entity.Property(e => e.Abstract)
                .HasMaxLength(3000)
                .HasComment("The full abstract for the presentation.");
            entity.Property(e => e.AdditionalDetails).HasMaxLength(3000);
            entity.Property(e => e.ElevatorPitch)
                .HasMaxLength(160)
                .HasComment("The summary for the presentation.");
            entity.Property(e => e.LanguageCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PresentationId).HasComment("The identifier of the associated presentation.");
            entity.Property(e => e.PresentationShortTitle)
                .HasMaxLength(60)
                .HasComment("The short title of the presentation.");
            entity.Property(e => e.PresentationTitle)
                .IsRequired()
                .HasMaxLength(300)
                .HasComment("The full title of the presentation.");
            entity.Property(e => e.ShortAbstract)
                .HasMaxLength(2000)
                .HasComment("The short abstract for the presentation.");

            entity.HasOne(d => d.LanguageCodeNavigation).WithMany(p => p.PresentationTexts)
                .HasForeignKey(d => d.LanguageCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentationText_Language");

            entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationTexts)
                .HasForeignKey(d => d.PresentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPresentationText_Presentation");
        });

        modelBuilder.Entity<PresentationType>(entity =>
        {
            entity.HasKey(e => e.PresentationTypeId).HasName("pkcPresentationType");

            entity.ToTable("PresentationType", tb => tb.HasComment("Represents a type of a presentation."));

            entity.Property(e => e.PresentationTypeId).HasComment("The identifier of the presentation type record.");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasComment("Flag indicating whether the presentation type is enabled.");
            entity.Property(e => e.PresentationTypeName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("The name of the presentation type.");
            entity.Property(e => e.SortOrder).HasComment("The sorting order of the presentation type.");
            entity.Property(e => e.TypeDescription)
                .HasMaxLength(500)
                .HasComment("A description of the presentation type.");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("pkcTag");

            entity.ToTable("Tag", tb => tb.HasComment("Represents a label attached to a presentation."));

            entity.Property(e => e.TagId).HasComment("The identifier of the tag record.");
            entity.Property(e => e.IsEnabled)
                .HasDefaultValue(true)
                .HasComment("Flag indicating whether the tag is enabled.");
            entity.Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("The name of the tag.");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}