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

    public virtual DbSet<CallForSpeaker> CallForSpeakers { get; set; }

    public virtual DbSet<CallForSpeakerStatus> CallForSpeakerStatuses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryDivision> CountryDivisions { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LearningObjective> LearningObjectives { get; set; }

    public virtual DbSet<Presentation> Presentations { get; set; }

    public virtual DbSet<PresentationStatus> PresentationStatuses { get; set; }

    public virtual DbSet<PresentationTag> PresentationTags { get; set; }

    public virtual DbSet<PresentationText> PresentationTexts { get; set; }

    public virtual DbSet<PresentationType> PresentationTypes { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionLearningObjective> SubmissionLearningObjectives { get; set; }

    public virtual DbSet<SubmissionStatus> SubmissionStatuses { get; set; }

    public virtual DbSet<SubmissionTag> SubmissionTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    public virtual DbSet<WorldRegion> WorldRegions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CallForSpeaker>(entity =>
        {
            entity.HasKey(e => e.CallForSpeakerId).HasName("pkcCallForSpeaker");

            entity.ToTable("CallForSpeaker");

            entity.Property(e => e.CallForSpeakerId).HasComment("The identifier of the call for speaker.");
            entity.Property(e => e.AccomodationExpensesCovered).HasComment("Indicates if the event will cover the accomodation expenses of the speaker.");
            entity.Property(e => e.AccomodationNotes)
                .HasMaxLength(500)
                .HasComment("Additional notes about the accomodation expenses.");
            entity.Property(e => e.CallForSpeakerEndDate).HasComment("The end date of the call for speaker.");
            entity.Property(e => e.CallForSpeakerStartDate).HasComment("The start date of the call for speaker.");
            entity.Property(e => e.CallForSpeakerStatusId).HasComment("The identifier of the call for speaker status.");
            entity.Property(e => e.CallForSpeakerUrl)
                .HasMaxLength(200)
                .HasComment("The URL of the call for speaker.");
            entity.Property(e => e.EventCity)
                .HasMaxLength(100)
                .HasComment("The city where the event is located.");
            entity.Property(e => e.EventCountryCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The ISO 3166-1 alpha-2 country code where the event is located.");
            entity.Property(e => e.EventCountryDivisionCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The ISO 3166-2 alpha-3 country division code where the event is located.");
            entity.Property(e => e.EventEndDate).HasComment("The end date of the event.");
            entity.Property(e => e.EventFeeCovered)
                .HasDefaultValue(true)
                .HasComment("Indicates if the event will cover the fee of the speaker.");
            entity.Property(e => e.EventFeeNotes)
                .HasMaxLength(500)
                .HasComment("Additional notes about the event fee.");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(300)
                .HasComment("The location of the event.");
            entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("The name of the event.");
            entity.Property(e => e.EventStartDate).HasComment("The start date of the event.");
            entity.Property(e => e.EventTimeZoneId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("The identifier of the time zone where the event is located.");
            entity.Property(e => e.EventUrl)
                .HasMaxLength(200)
                .HasComment("The URL of the event.");
            entity.Property(e => e.ExpectedDecisionDate).HasComment("The expected decision date for the submissions.");
            entity.Property(e => e.Permalink)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SpeakerHonorarium).HasComment("Indicates if the speaker will receive a honorarium.");
            entity.Property(e => e.SpeakerHonorariumAmount)
                .HasComment("The amount of the honorarium.")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SpeakerHonorariumCurrency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The currency of the honorarium.");
            entity.Property(e => e.SpeakerHonorariumNotes)
                .HasMaxLength(500)
                .HasComment("Additional notes about the honorarium.");
            entity.Property(e => e.SubmissionLimit).HasComment("The maximum number of submissions allowed.");
            entity.Property(e => e.TravelExpensesCovered).HasComment("Indicates if the event will cover the travel expenses of the speaker.");
            entity.Property(e => e.TravelNotes)
                .HasMaxLength(500)
                .HasComment("Additional notes about the travel expenses.");

            entity.HasOne(d => d.CallForSpeakerStatus).WithMany(p => p.CallForSpeakers)
                .HasForeignKey(d => d.CallForSpeakerStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCallForSpeaker_CallForSpeakerStatus");

            entity.HasOne(d => d.EventCountryCodeNavigation).WithMany(p => p.CallForSpeakers)
                .HasForeignKey(d => d.EventCountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCallForSpeaker_Country");

            entity.HasOne(d => d.EventTimeZone).WithMany(p => p.CallForSpeakers)
                .HasForeignKey(d => d.EventTimeZoneId)
                .HasConstraintName("fkCallForSpeaker_TimeZone");

            entity.HasOne(d => d.CountryDivision).WithMany(p => p.CallForSpeakers)
                .HasForeignKey(d => new { d.EventCountryCode, d.EventCountryDivisionCode })
                .HasConstraintName("fkCallForSpeaker_CountryDivision");
        });

        modelBuilder.Entity<CallForSpeakerStatus>(entity =>
        {
            entity.HasKey(e => e.CallForSpeakerStatusId).HasName("pkcCallForSpeakerStatus");

            entity.ToTable("CallForSpeakerStatus", tb => tb.HasComment("Represents a status of a call for speakers."));

            entity.Property(e => e.CallForSpeakerStatusId)
                .ValueGeneratedNever()
                .HasComment("The identifier of the call for speakers status record.");
            entity.Property(e => e.CallForSpeakerStatusName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("The name of the call for speakers status.");
            entity.Property(e => e.IsDefault).HasComment("Indicates whether the call for speakers status is the default status.");
            entity.Property(e => e.IsEnabled).HasComment("Indicates whether the call for speakers status is enabled.");
            entity.Property(e => e.SortOrder).HasComment("The order in which the call for speakers status should be displayed.");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("pkcCountry");

            entity.ToTable("Country", tb => tb.HasComment("Lookup table representing the countries as defined by the ISO 3166-1 standard."));

            entity.HasIndex(e => e.WorldRegionCode, "idxCountry_WorldRegionCode");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the country using the ISO 3166-1 Alpha-2 code.");
            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Name of the country using the ISO 3166-1 Country Name.");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("The primary name of the country's divisions.");
            entity.Property(e => e.HasDivisions).HasComment("Flag indicating whether the country has divisions (states, provinces, etc.)");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the country record is enabled.");
            entity.Property(e => e.M49code)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the country using the United Nations M49 standard.")
                .HasColumnName("M49Code");
            entity.Property(e => e.WorldRegionCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the world region where the country is located.");

            entity.HasOne(d => d.WorldRegionCodeNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.WorldRegionCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCountry_WorldRegion");
        });

        modelBuilder.Entity<CountryDivision>(entity =>
        {
            entity.HasKey(e => new { e.CountryCode, e.CountryDivisionCode }).HasName("pkcCountryDivision");

            entity.ToTable("CountryDivision", tb => tb.HasComment("Lookup table representing the world regions as defined by the ISO 3166-2 standard."));

            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountryDivisionCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the country division using the ISO 3166-2 Alpha-2 code.");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("The category name of the country division.");
            entity.Property(e => e.CountryDivisionName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Name of the country using the ISO 3166-2 Subdivision Name.");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the country division record is enabled.");

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.CountryDivisions)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkCountryDivision_Country");
        });

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

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("pkcSubmission");

            entity.ToTable("Submission", tb => tb.HasComment("Represents a session submission to a call for speakers."));

            entity.Property(e => e.SubmissionId).HasComment("Identifier of the Submission record.");
            entity.Property(e => e.AdditionalDetails)
                .HasMaxLength(3000)
                .HasComment("Any additional details about the session submission to the event organizers.");
            entity.Property(e => e.CallForSpeakerId).HasComment("Identifier of the associated call for speakers.");
            entity.Property(e => e.DecisionDate).HasComment("The date the event provided a decision about the submission.");
            entity.Property(e => e.ElevatorPitch)
                .HasMaxLength(300)
                .HasComment("The elevator pitch for the session.");
            entity.Property(e => e.PresentationId).HasComment("Identifier of the associated presentation.");
            entity.Property(e => e.SessionDescription)
                .IsRequired()
                .HasMaxLength(3000)
                .HasComment("The description of the submitted session.");
            entity.Property(e => e.SessionLength).HasComment("The length of the submitted session (in minutes).");
            entity.Property(e => e.SessionLevel)
                .HasMaxLength(100)
                .HasComment("The level of the submitted session.");
            entity.Property(e => e.SessionTitle)
                .IsRequired()
                .HasMaxLength(300)
                .HasComment("The title of the submitted session.");
            entity.Property(e => e.SessionTrack)
                .HasMaxLength(100)
                .HasComment("The track the session was submitted under.");
            entity.Property(e => e.SubmissionDate).HasComment("The date of the submission.");
            entity.Property(e => e.SubmissionLanguageCode)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SubmissionStatusId).HasComment("Identifier of the status for the submission.");

            entity.HasOne(d => d.CallForSpeaker).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.CallForSpeakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmission_CallForSpeaker");

            entity.HasOne(d => d.Presentation).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.PresentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmission_Presentation");

            entity.HasOne(d => d.SubmissionStatus).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.SubmissionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmission_SubmissionStatus");
        });

        modelBuilder.Entity<SubmissionLearningObjective>(entity =>
        {
            entity.HasKey(e => e.SubmissionLearningObjectiveId).HasName("pkcSubmissionLearningObjective");

            entity.ToTable("SubmissionLearningObjective", tb => tb.HasComment("Represents a learning objective that was a part of a submission to a call for speakers.."));

            entity.Property(e => e.SubmissionLearningObjectiveId).HasComment("Identifier of the SubmissionLearningObjective record.");
            entity.Property(e => e.LearningObjectiveText)
                .IsRequired()
                .HasMaxLength(1000)
                .HasComment("The text of the submitted learning objective.");
            entity.Property(e => e.SubmissionId).HasComment("Identifier of the associated submission record.");

            entity.HasOne(d => d.Submission).WithMany(p => p.SubmissionLearningObjectives)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmissionLearningObjective_Submission");
        });

        modelBuilder.Entity<SubmissionStatus>(entity =>
        {
            entity.HasKey(e => e.SubmissionStatusId).HasName("pkcSubmissionStatus");

            entity.ToTable("SubmissionStatus", tb => tb.HasComment("Represents a submission status."));

            entity.Property(e => e.SubmissionStatusId)
                .ValueGeneratedNever()
                .HasComment("The identifier of the submnission status record.");
            entity.Property(e => e.IndicatesAcceptance).HasComment("Flag indicating whether the submission status indicates acceptance.");
            entity.Property(e => e.IsDefault).HasComment("Flag indicating whether the submission status is the default status.");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the submission status is enabled.");
            entity.Property(e => e.SortOrder).HasComment("The sorting order of the submission status.");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(500)
                .HasComment("A description of the submission status.");
            entity.Property(e => e.SubmissionStatusName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("The name of the submission status.");
        });

        modelBuilder.Entity<SubmissionTag>(entity =>
        {
            entity.HasKey(e => e.SubmissionTagId).HasName("pkcSubmissionTag");

            entity.ToTable("SubmissionTag", tb => tb.HasComment("Associated a tag with a call for speaker submission."));

            entity.Property(e => e.SubmissionTagId).HasComment("Identifier of the SubmissionTag record.");
            entity.Property(e => e.SubmissionId).HasComment("Identifier of the associated submission.");
            entity.Property(e => e.TagId).HasComment("Identifier of the associated tag.");

            entity.HasOne(d => d.Submission).WithMany(p => p.SubmissionTags)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmissionTag_Submission");

            entity.HasOne(d => d.Tag).WithMany(p => p.SubmissionTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkSubmissionTag_Tag");
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

        modelBuilder.Entity<TimeZone>(entity =>
        {
            entity.HasKey(e => e.TimeZoneId).HasName("pkcTimeZone");

            entity.ToTable("TimeZone", tb => tb.HasComment("Represents the list of time zones as defined by the IANA."));

            entity.Property(e => e.TimeZoneId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("The identifier of the time zone as defined by the IANA.");
            entity.Property(e => e.DaylightOffset)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The daylight offset for the time zone.");
            entity.Property(e => e.DaylightSavingsAbbreviation)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The daylight savings abbreviation for the time zone.");
            entity.Property(e => e.StandardAbbreviation)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The standard abbreviation for the time zone.");
            entity.Property(e => e.StandardOffset)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("The standard offset for the time zone.");
        });

        modelBuilder.Entity<WorldRegion>(entity =>
        {
            entity.HasKey(e => e.WorldRegionCode).HasName("pkcWorldRegion");

            entity.ToTable("WorldRegion", tb => tb.HasComment("Lookup table representing the world regions as defined by the UN M49 specification."));

            entity.HasIndex(e => e.ParentId, "idxWorldRegion_ParentId");

            entity.Property(e => e.WorldRegionCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the world region.");
            entity.Property(e => e.IsEnabled).HasComment("Flag indicating whether the world region is enabled.");
            entity.Property(e => e.ParentId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("Identifier of the world region parent (for subregions).");
            entity.Property(e => e.WorldRegionName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Name of the world region.");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("fkWorldRegion_WorldRegion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}