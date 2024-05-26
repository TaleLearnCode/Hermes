namespace Hermes.Responses;

public class EngagementCallForSpeakerResponse
{
	public string Status { get; set; } = null!;
	public string? StatusDescription { get; set; }
	public string Url { get; set; } = null!;
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public DateOnly? ExpectedDecisionDate { get; set; }
	public DateOnly? ActualDecisionDate { get; set; }
	public bool IncludesSpeakerHonorarium { get; set; } = false;
	public decimal? SpeakerHonorariumAmount { get; set; }
	public string? SpeakerHonorariumCurrencyCode { get; set; }
	public string? SpeakerHonorariumNotes { get; set; }
	public bool IncludesTravelExpenses { get; set; } = false;
	public string? TravelExpensesNotes { get; set; }
	public bool IncludesAccommodation { get; set; } = false;
	public string? AccommodationNotes { get; set; }
	public bool EventFeeCovered { get; set; } = true;
	public string? EventFeeNotes { get; set; }
	public int? SubmissionLimit { get; set; }
}