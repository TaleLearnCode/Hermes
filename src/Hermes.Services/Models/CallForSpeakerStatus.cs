﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hermes.Models;

/// <summary>
/// Represents a status of a call for speakers.
/// </summary>
public partial class CallForSpeakerStatus
{
    /// <summary>
    /// The identifier of the call for speakers status record.
    /// </summary>
    public int CallForSpeakerStatusId { get; set; }

    /// <summary>
    /// The name of the call for speakers status.
    /// </summary>
    public string CallForSpeakerStatusName { get; set; }

    /// <summary>
    /// The order in which the call for speakers status should be displayed.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// Indicates whether the call for speakers status is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual ICollection<CallForSpeaker> CallForSpeakers { get; set; } = new List<CallForSpeaker>();
}