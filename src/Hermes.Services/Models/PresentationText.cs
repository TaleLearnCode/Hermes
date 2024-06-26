﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// The text for a presentation details in a specific language.
/// </summary>
public partial class PresentationText
{
    /// <summary>
    /// The identifier of the presentation text record.
    /// </summary>
    public int PresentationTextId { get; set; }

    /// <summary>
    /// The identifier of the associated presentation.
    /// </summary>
    public string PresentationId { get; set; }

    public string LanguageCode { get; set; }

    /// <summary>
    /// The full title of the presentation.
    /// </summary>
    public string PresentationTitle { get; set; }

    /// <summary>
    /// The short title of the presentation.
    /// </summary>
    public string PresentationShortTitle { get; set; }

    /// <summary>
    /// The full abstract for the presentation.
    /// </summary>
    public string Abstract { get; set; }

    /// <summary>
    /// The short abstract for the presentation.
    /// </summary>
    public string ShortAbstract { get; set; }

    /// <summary>
    /// The summary for the presentation.
    /// </summary>
    public string ElevatorPitch { get; set; }

    public string AdditionalDetails { get; set; }

    public virtual Language LanguageCodeNavigation { get; set; }

    public virtual ICollection<LearningObjective> LearningObjectives { get; set; } = new List<LearningObjective>();

    public virtual Presentation Presentation { get; set; }
}