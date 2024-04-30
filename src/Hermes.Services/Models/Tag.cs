﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// Represents a label attached to a presentation.
/// </summary>
public partial class Tag
{
    /// <summary>
    /// The identifier of the tag record.
    /// </summary>
    public int TagId { get; set; }

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public string TagName { get; set; }

    /// <summary>
    /// Flag indicating whether the tag is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual ICollection<PresentationTag> PresentationTags { get; set; } = new List<PresentationTag>();
}