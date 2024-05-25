﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// Represents an engagement.
/// </summary>
public partial class Engagement
{
    /// <summary>
    /// The unique identifier for the engagement.
    /// </summary>
    public string Permalink { get; set; }

    /// <summary>
    /// The identifier of the engagement type.
    /// </summary>
    public int EngagementTypeId { get; set; }

    /// <summary>
    /// The identifier of the engagement status.
    /// </summary>
    public int EngagementStatusId { get; set; }

    /// <summary>
    /// The name of the engagement.
    /// </summary>
    public string EngagementName { get; set; }

    /// <summary>
    /// The country code for the engagement.
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// The country division code for the engagement.
    /// </summary>
    public string CountryDivisionCode { get; set; }

    /// <summary>
    /// The city where the engagement is located.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// The venue where the engagement is located.
    /// </summary>
    public string Venue { get; set; }

    /// <summary>
    /// The location of the engagement.
    /// </summary>
    public string OverviewLocation { get; set; }

    /// <summary>
    /// The location of the engagement as it should be listed.
    /// </summary>
    public string ListingLocation { get; set; }

    /// <summary>
    /// The start date of the engagement.
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// The end date of the engagement.
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// The time zone of the engagement.
    /// </summary>
    public string TimeZoneId { get; set; }

    /// <summary>
    /// The starting cost of the engagement.
    /// </summary>
    public string StartingCost { get; set; }

    /// <summary>
    /// The ending cost of the engagement.
    /// </summary>
    public string EndingCost { get; set; }

    /// <summary>
    /// The description of the engagement.
    /// </summary>
    public string EngagementDescription { get; set; }

    /// <summary>
    /// The summary of the engagement.
    /// </summary>
    public string EngagementSummary { get; set; }

    /// <summary>
    /// The URL of the engagement.
    /// </summary>
    public string EngagementUrl { get; set; }

    /// <summary>
    /// Flag indicating whether the engagement should be included in the public profile.
    /// </summary>
    public bool IncludeInPublicProfile { get; set; }

    /// <summary>
    /// Flag indicating whether the engagement is virtual.
    /// </summary>
    public bool IsVirtual { get; set; }

    /// <summary>
    /// Flag indicating whether the engagement is public.
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Flag indicating whether the engagement is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual Country CountryCodeNavigation { get; set; }

    public virtual CountryDivision CountryDivision { get; set; }

    public virtual EngagementCallForSpeker EngagementCallForSpeker { get; set; }

    public virtual ICollection<EngagementPresentation> EngagementPresentations { get; set; } = new List<EngagementPresentation>();

    public virtual EngagementStatus EngagementStatus { get; set; }

    public virtual EngagementType EngagementType { get; set; }

    public virtual TimeZone TimeZone { get; set; }
}