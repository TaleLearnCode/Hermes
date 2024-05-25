//#nullable disable
//namespace Hermes.Requests;

//public class CallForSpeakerRequest
//{
//	public string Permalink { get; set; }
//	public string Status { get; set; }
//	public string EventName { get; set; }
//	public string EventUrl { get; set; }
//	public DateOnly EventStartDate { get; set; }
//	public DateOnly EventEndDate { get; set; }
//	public string EventLocation { get; set; }
//	public string EventCity { get; set; }
//	public string EventCountryCode { get; set; }
//	public string EventCountryDivisionCode { get; set; }
//	public string EventTimeZone { get; set; }
//	public string CallForSpeakerUrl { get; set; }
//	public DateOnly? CallForSpeakerStartDate { get; set; } = null;
//	public DateOnly? CallForSpeakerEndDate { get; set; } = null;
//	public bool IncludesSpeakerHonorarium { get; set; }
//	public decimal? SpeakerHonorariumAmount { get; set; } = null;
//	public string SpeakerHonorariumCurrency { get; set; }
//	public string SpeakerHonorariumNotes { get; set; }
//	public bool AreTravelExpenseCovered { get; set; }
//	public string TravelNotes { get; set; }
//	public bool AreAccommodationsCovered { get; set; }
//	public string AccomodationNotes { get; set; }
//	public bool EventFeeCovered { get; set; }
//	public string EventFeeNotes { get; set; }
//	public int? SubmissionLimit { get; set; }
//	public List<SubmissionRequest> Submissions { get; set; } = [];
//}