﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// Lookup table representing the world regions as defined by the ISO 3166-2 standard.
/// </summary>
public partial class CountryDivision
{
    /// <summary>
    /// Identifier of the country division using the ISO 3166-2 Alpha-2 code.
    /// </summary>
    public string CountryDivisionCode { get; set; }

    public string CountryCode { get; set; }

    /// <summary>
    /// Name of the country using the ISO 3166-2 Subdivision Name.
    /// </summary>
    public string CountryDivisionName { get; set; }

    /// <summary>
    /// The category name of the country division.
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// Flag indicating whether the country division record is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual Country CountryCodeNavigation { get; set; }

    public virtual ICollection<Engagement> Engagements { get; set; } = new List<Engagement>();
}