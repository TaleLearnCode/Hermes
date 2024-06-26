﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// Represents a type of a presentation.
/// </summary>
public partial class PresentationType
{
    /// <summary>
    /// The identifier of the presentation type record.
    /// </summary>
    public int PresentationTypeId { get; set; }

    /// <summary>
    /// The name of the presentation type.
    /// </summary>
    public string PresentationTypeName { get; set; }

    /// <summary>
    /// The sorting order of the presentation type.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// A description of the presentation type.
    /// </summary>
    public string TypeDescription { get; set; }

    /// <summary>
    /// Flag indicating whether the presentation type is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();
}