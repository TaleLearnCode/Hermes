//using Hermes.Types;
//using System.Text;

//namespace Hermes.Extensions;

//internal static class CallForSpeakerRequestExtensions
//{
//	private const string _permalinkInstructions = "[OPTIONAL] The unique permalink for the call for speakers";
//	private const string _statusInstructions = "[OPTIONAL] The status of the call for speakers.";
//	private const string _callForSpeakersUrlInstructions = "[OPTIONAL] The URL for the call for speakers webpage.";
//	private const string _callForSpeakersStartDateInstructions = "[OPTIONAL] The start date of the call for speakers.";
//	private const string _callForSpeakersEndDateInstructions = "[OPTIONAL] The end date of the call for speakers.";
//	private const string _includesHonorariumInstructions = "[OPTIONAL] Indication whether the event pays speakers an honorarium. Default is false.";
//	private const string _honorariumAmountInstructions = "[OPTIONAL] If the event pays an honorarium, specify the amount here.";
//	private const string _honorariumCurrencyInstructions = "[OPTIONAL] The currency for the honorarium amount.";
//	private const string _honorariumNotesInstructions = "[OPTIONAL] Any notes regarding the speaker honorarium.";
//	private const string _areTravelExpensesCoveredInstructions = "[OPTIONAL] Indication whether the event pays for travel costs. Default is false.";
//	private const string _travelNotesInstructions = "[OPTIONAL] Any notes about the event's coverage of travel costs.";
//	private const string _areAccommodationsCoveredInstructions = "[OPTIONAL] Indication whether the event pays for the accommodations during the event. Default is false.";
//	private const string _accomodationNotesInstructions = "OPTIONAL] Any notes about the event's accommodation coverage.";
//	private const string _eventFeeCoveredInstructions = "[OPTIONAL] Indicating whether the event fee is covered for speakers. Default is true.";
//	private const string _eventFeeNotesInstructions = "[OPTIONAL] Any notes about the event fee being covered or note.";
//	private const string _submissionLimitInstructions = "[OPTIONAL] The number of submissions allowed per speaker. Either do not specify a value or specify 0 if there is no limit.";
//	private const string _eventNameInstructions = "[REQUIRED] The name of the event.";
//	private const string _eventUrlInstructions = "[OPTIONAL] The URL for the event.";
//	private const string _eventStartDateInstructions = "[REQUIRED] The event start date.";
//	private const string _eventEndDateInstructions = "[REQUIRED] The event end date.";
//	private const string _eventLocationInstructions = "[OPTIONAL] The location of the event.";
//	private const string _eventCityInstructions = "[OPTIONAL] The city where the event is held.";
//	private const string _eventCountryCodeInstructions = "[REQUIRED] The 2-character code for the country where the event is held.";
//	private const string _eventCountryDivisionCodeInstructions = "[OPTIONAL] The 2-character code for the country division where the event is held.";
//	private const string _eventTimeZoneInstructions = "[OPTIONAL] The IANA time zone for the event's location.";

//	private const string _submissionStatusInstructions = "[OPTIONAL] The status of the submission. Default is 'Under Review'.";
//	private const string _submissionDateInstructions = "[OPTIONAL] The date the submission was made. Default is the current date.";
//	private const string _expectedDecisionDateInstructions = "[OPTIONAL] The expected date for a decision on the submission. If not specified, will default to six weeks after the call for speakers end date (if available) or 4 weeks prior to the event start date.";
//	private const string _decisionDateInstructions = "[OPTIONAL] The date the submission was accepted or rejected.";
//	private const string _languageCodeInstructions = "[OPTIONAL] The language code for the submission. Default is 'en'.";
//	private const string _presentationPermalinkInstructions = "[REQUIRED] The unique permalink for the presentation.";
//	private const string _sessionTitleInstructions = "[OPTIONAL] The title of the session. If not specified, the saved title for the presentation will be used.";
//	private const string _sessionDescriptionInstructions = "[OPTIONAL] The description of the session. If not specified, the saved abstract for the presentation will be used.";
//	private const string _sessionLengthInstructions = "[OPTIONAL] The length of the session in minutes.";
//	private const string _sessionTrackInstructions = "[OPTIONAL] The track for the session.";
//	private const string _sessionLevelInstructions = "[OPTIONAL] The level of the session.";
//	private const string _elevatorPitchInstructions = "[OPTIONAL] A brief elevator pitch for the session.";
//	private const string _additionalDetailsInstructions = "[OPTIONAL] Any additional details for the session.";
//	private const string _learningObjectivesInstructions = "[OPTIONAL] A list of learning objectives for the session.";
//	private const string _tagsInstructions = "[OPTIONAL] A list of tags for the session.";

//	internal static string ToMarkdown(this CallForSpeakerRequest callForSpeakerRequest, bool populateWithInstructions = false)
//	{
//		StringBuilder response = new();
//		response.AppendLine($"## {MarkdownCallForSpeakersHeadings.CallForSpeakerDetails}");
//		response.AppendLine("| Attribute | Value |");
//		response.AppendLine("|---|---|");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.Permalink} | {((!populateWithInstructions) ? callForSpeakerRequest.Permalink : _permalinkInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.Status} | {((!populateWithInstructions) ? callForSpeakerRequest.Status : _statusInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakerUrl} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerUrl : _callForSpeakersUrlInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakersStartDate} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerStartDate : CallForSpeakerRequestExtensions._callForSpeakersStartDateInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakersEndDate} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerEndDate : CallForSpeakerRequestExtensions._callForSpeakersEndDateInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.IncludesSpeakerHonorarium} | {((!populateWithInstructions) ? callForSpeakerRequest.IncludesSpeakerHonorarium : CallForSpeakerRequestExtensions._includesHonorariumInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumAmount} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumAmount : CallForSpeakerRequestExtensions._honorariumAmountInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumCurrency} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumCurrency : _honorariumCurrencyInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumNotes : _honorariumNotesInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AreTravelExpensesCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.AreTravelExpenseCovered : CallForSpeakerRequestExtensions._areTravelExpensesCoveredInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.TravelNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.TravelNotes : _travelNotesInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AreAccommodationsCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.AreAccommodationsCovered : CallForSpeakerRequestExtensions._areAccommodationsCoveredInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AccomodationNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.AccomodationNotes : _accomodationNotesInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.IsEventFeeCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.EventFeeCovered : CallForSpeakerRequestExtensions._eventFeeCoveredInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventFeeNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.EventFeeNotes : _eventFeeNotesInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SubmissionLimit} | {((!populateWithInstructions) ? callForSpeakerRequest.SubmissionLimit : CallForSpeakerRequestExtensions._submissionLimitInstructions)} | ");
//		response.AppendLine();
//		response.AppendLine($"## {MarkdownCallForSpeakersHeadings.EventDetails}");
//		response.AppendLine("| Attribute | Value |");
//		response.AppendLine("|---|---|");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventName} | {((!populateWithInstructions) ? callForSpeakerRequest.EventName : _eventNameInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventUrl} | {((!populateWithInstructions) ? callForSpeakerRequest.EventUrl : _eventUrlInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventStartDate} | {((!populateWithInstructions) ? callForSpeakerRequest.EventStartDate : CallForSpeakerRequestExtensions._eventStartDateInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventEndDate} | {((!populateWithInstructions) ? callForSpeakerRequest.EventEndDate : CallForSpeakerRequestExtensions._eventEndDateInstructions)} | ");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventLocation} | {((!populateWithInstructions) ? callForSpeakerRequest.EventLocation : _eventLocationInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCity} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCity : _eventCityInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCountryCode} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCountryCode : _eventCountryCodeInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCountryDivisionCode} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCountryDivisionCode : _eventCountryDivisionCodeInstructions)} |");
//		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventTimeZone} | {((!populateWithInstructions) ? callForSpeakerRequest.EventTimeZone : _eventTimeZoneInstructions)} |");
//		response.AppendLine();
//		response.AppendLine($"## {MarkdownCallForSpeakersHeadings.Submissions}");
//		response.AppendLine();
//		response.AppendLine($"### {MarkdownSubmissionHeadings.Submission} - {{presentation-permalink}}");
//		response.AppendLine();
//		response.AppendLine("| Attribute | Value |");
//		response.AppendLine("|---|---|");
//		response.AppendLine($"{MarkdownSubmissionAttributes.SessionTitle} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].SessionTitle : _sessionTitleInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.Status} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].Status : _submissionStatusInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.SubmissionDate} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].SubmissionDate : _submissionDateInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.ExpectedDecisionDate} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].ExpectedDecisionDate : _expectedDecisionDateInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.DecisionDate} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].DecisionDate : _decisionDateInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.LanguageCode} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].LanguageCode : _languageCodeInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.SessionLength} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].SessionLength : _sessionLengthInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.SessionTrack} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].SessionTrack : _sessionTrackInstructions)} |");
//		response.AppendLine($"{MarkdownSubmissionAttributes.SessionLevel} | {((!populateWithInstructions) ? callForSpeakerRequest.Submissions[0].SessionLevel : _sessionLevelInstructions)} |");
//		response.AppendLine();
//		response.AppendLine($"#### {MarkdownSubmissionHeadings.Description}");
//		response.AppendLine(_sessionDescriptionInstructions);
//		response.AppendLine();
//		response.AppendLine($"#### {MarkdownSubmissionHeadings.ElevatorPitch}");
//		response.AppendLine(_elevatorPitchInstructions);
//		response.AppendLine();
//		response.AppendLine($"#### {MarkdownSubmissionHeadings.AdditionalDetails}");
//		response.AppendLine(_additionalDetailsInstructions);
//		response.AppendLine();
//		response.AppendLine($"#### {MarkdownSubmissionHeadings.LearningObjectives}");
//		response.AppendLine($"- {_learningObjectivesInstructions}");
//		response.AppendLine();
//		response.AppendLine($"#### {MarkdownSubmissionHeadings.Tags}");
//		response.AppendLine($" -{_tagsInstructions}");
//		return response.ToString();
//	}

//	internal static void AddInstructions(this CallForSpeakerRequest callForSpeaker)
//	{
//		callForSpeaker.Permalink = _permalinkInstructions;
//		callForSpeaker.Status = _statusInstructions;
//		callForSpeaker.CallForSpeakerUrl = _callForSpeakersUrlInstructions;
//		callForSpeaker.CallForSpeakerStartDate = DateOnly.FromDateTime(DateTime.UtcNow);
//		callForSpeaker.CallForSpeakerEndDate = DateOnly.FromDateTime(DateTime.UtcNow);
//		callForSpeaker.IncludesSpeakerHonorarium = false;
//		callForSpeaker.SpeakerHonorariumAmount = 0;
//		callForSpeaker.SpeakerHonorariumCurrency = _honorariumCurrencyInstructions;
//		callForSpeaker.SpeakerHonorariumNotes = _honorariumNotesInstructions;
//		callForSpeaker.AreTravelExpenseCovered = false;
//		callForSpeaker.TravelNotes = _travelNotesInstructions;
//		callForSpeaker.AreAccommodationsCovered = false;
//		callForSpeaker.AccomodationNotes = _accomodationNotesInstructions;
//		callForSpeaker.EventFeeCovered = true;
//		callForSpeaker.EventFeeNotes = _eventFeeNotesInstructions;
//		callForSpeaker.SubmissionLimit = default;
//		callForSpeaker.EventName = _eventNameInstructions;
//		callForSpeaker.EventUrl = _eventUrlInstructions;
//		callForSpeaker.EventStartDate = DateOnly.FromDateTime(DateTime.UtcNow);
//		callForSpeaker.EventEndDate = DateOnly.FromDateTime(DateTime.UtcNow);
//		callForSpeaker.EventLocation = _eventLocationInstructions;
//		callForSpeaker.EventCity = _eventCityInstructions;
//		callForSpeaker.EventCountryCode = _eventCountryCodeInstructions;
//		callForSpeaker.EventCountryDivisionCode = _eventCountryDivisionCodeInstructions;
//		callForSpeaker.EventTimeZone = _eventTimeZoneInstructions;
//		callForSpeaker.Submissions.Add(new()
//		{
//			Status = _submissionStatusInstructions,
//			SubmissionDate = DateOnly.FromDateTime(DateTime.UtcNow),
//			ExpectedDecisionDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(42),
//			DecisionDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-28),
//			LanguageCode = _languageCodeInstructions,
//			PresentationPermalink = _presentationPermalinkInstructions,
//			SessionTitle = _sessionTitleInstructions,
//			SessionDescription = _sessionDescriptionInstructions,
//			SessionLength = 60,
//			SessionTrack = _sessionTrackInstructions,
//			SessionLevel = _sessionLevelInstructions,
//			ElevatorPitch = _elevatorPitchInstructions,
//			AdditionalDetails = _additionalDetailsInstructions,
//			LearningObjectives = [_learningObjectivesInstructions],
//			Tags = [_tagsInstructions]
//		});
//	}

//	internal static CallForSpeakerRequest ToCallForSpeakerRequest(this string markdown)
//	{
//		string[] lines = markdown.Split(Environment.NewLine);
//		CallForSpeakerRequest callForSpeakerRequest = new();
//		Dictionary<string, SubmissionDetail> submissions = [];
//		string? currentField = null;
//		string? currentSubmission = null;
//		string? currentSubmissionField = null;
//		string? attributeValue = null;
//		List<string> submittedPresentations = [];
//		foreach (string line in lines)
//		{
//			string currentMarkdownLine = line.Trim();
//			if (currentMarkdownLine.StartsWith("## "))
//			{
//				currentField = currentMarkdownLine[3..];
//				if (currentField == MarkdownCallForSpeakersHeadings.Submissions)
//					currentSubmissionField = null;
//			}
//			else if (currentMarkdownLine.StartsWith("### "))
//			{
//				currentSubmission = currentMarkdownLine.Split("-")[1].Trim();
//				submissions.Add(currentSubmission, new SubmissionDetail { PresentationPermalink = currentSubmission });
//			}
//			else if (currentMarkdownLine.StartsWith("#### "))
//			{
//				currentSubmissionField = currentMarkdownLine[5..];
//			}
//			else
//			{
//				switch (currentField)
//				{
//					case MarkdownCallForSpeakersHeadings.CallForSpeakerDetails:
//						attributeValue = (line.Contains('|')) ? line.Split('|')[2].Trim() : line;
//						ParseCallForSpeakerDetail(callForSpeakerRequest, attributeValue, line);
//						break;
//					case MarkdownCallForSpeakersHeadings.EventDetails:
//						attributeValue = (line.Contains('|')) ? line.Split('|')[2].Trim() : line;
//						ParseEventDetail(callForSpeakerRequest, attributeValue, line);
//						break;
//					case MarkdownCallForSpeakersHeadings.Submissions:
//						if (currentSubmission is not null)
//						{
//							switch (currentSubmissionField)
//							{
//								case MarkdownSubmissionHeadings.SubmissionDetails:
//									attributeValue = (line.Contains('|')) ? line.Split('|')[2].Trim() : line;
//									ParseSubmissionDetail(submissions, currentSubmission, attributeValue, line);
//									break;
//								case MarkdownSubmissionHeadings.Description:
//									submissions[currentSubmission].Description.AppendLine(line);
//									break;
//								case MarkdownSubmissionHeadings.ElevatorPitch:
//									submissions[currentSubmission].ElevatorPitch.AppendLine(line);
//									break;
//								case MarkdownSubmissionHeadings.AdditionalDetails:
//									submissions[currentSubmission].AdditionalDetails.AppendLine(line);
//									break;
//								case MarkdownSubmissionHeadings.LearningObjectives:
//									if (line.StartsWith("-"))
//										submissions[currentSubmission].LearningObjectives.Add(line[1..]);
//									break;
//								case MarkdownSubmissionHeadings.Tags:
//									if (line.StartsWith("-"))
//										submissions[currentSubmission].Tags.Add(line[1..]);
//									break;
//							}
//						}
//						break;
//				}
//			}
//		}

//		foreach (SubmissionDetail submission in submissions.Values)
//		{
//			callForSpeakerRequest.Submissions.Add(new SubmissionRequest
//			{
//				Status = submission.Status,
//				SubmissionDate = submission.SubmissionDate,
//				DecisionDate = submission.DecisionDate,
//				LanguageCode = submission.LanguageCode,
//				PresentationPermalink = submission.PresentationPermalink!,
//				SessionTitle = submission.Title,
//				SessionDescription = submission.Description.ToString(),
//				SessionLength = submission.Length,
//				SessionTrack = submission.Track,
//				SessionLevel = submission.Level,
//				ElevatorPitch = submission.ElevatorPitch.ToString(),
//				AdditionalDetails = submission.AdditionalDetails.ToString(),
//				LearningObjectives = submission.LearningObjectives,
//				Tags = submission.Tags
//			});
//		}

//		foreach (string presentation in submittedPresentations)
//		{
//			if (!callForSpeakerRequest.Submissions.Any(s => s.PresentationPermalink == presentation))
//			{
//				callForSpeakerRequest.Submissions.Add(new SubmissionRequest { PresentationPermalink = presentation });
//			}
//		}




//		return callForSpeakerRequest;
//	}

//	private static void ParseSubmissionDetail(
//		Dictionary<string, SubmissionDetail> submissions,
//		string? currentSubmission,
//		string attributeValue,
//		string line)
//	{
//		if (currentSubmission is not null)
//		{
//			if (line.StartsWith(MarkdownSubmissionAttributes.SessionTitle))
//				submissions[currentSubmission].Title = attributeValue;
//			else if (line.StartsWith(MarkdownSubmissionAttributes.Status))
//				submissions[currentSubmission].Status = attributeValue;
//			else if (line.StartsWith(MarkdownSubmissionAttributes.SubmissionDate))
//				submissions[currentSubmission].SubmissionDate = GetDateOnly(MarkdownSubmissionAttributes.SubmissionDate, attributeValue);
//			else if (line.StartsWith(MarkdownSubmissionAttributes.DecisionDate))
//				submissions[currentSubmission].DecisionDate = GetDateOnly(MarkdownSubmissionAttributes.DecisionDate, attributeValue);
//			else if (line.StartsWith(MarkdownSubmissionAttributes.LanguageCode))
//				submissions[currentSubmission].LanguageCode = attributeValue;
//			else if (line.StartsWith(MarkdownSubmissionAttributes.SessionLength))
//				submissions[currentSubmission].Length = int.Parse(attributeValue);
//			else if (line.StartsWith(MarkdownSubmissionAttributes.SessionTrack))
//				submissions[currentSubmission].Track = attributeValue;
//			else if (line.StartsWith(MarkdownSubmissionAttributes.SessionLevel))
//				submissions[currentSubmission].Level = attributeValue;
//		}
//	}

//	private static void ParseEventDetail(
//		CallForSpeakerRequest callForSpeakerRequest,
//		string attributeValue,
//		string line)
//	{
//		if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventName))
//			callForSpeakerRequest.EventName = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventUrl))
//			callForSpeakerRequest.EventUrl = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventStartDate))
//			callForSpeakerRequest.EventStartDate = GetDateOnly(MarkdownCallForSpeakersAttributes.EventStartDate, attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventEndDate))
//			callForSpeakerRequest.EventEndDate = GetDateOnly(MarkdownCallForSpeakersAttributes.EventEndDate, attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventLocation))
//			callForSpeakerRequest.EventLocation = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventCity))
//			callForSpeakerRequest.EventCity = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventCountryCode))
//			callForSpeakerRequest.EventCountryCode = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventCountryDivisionCode))
//			callForSpeakerRequest.EventCountryDivisionCode = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventTimeZone))
//			callForSpeakerRequest.EventTimeZone = attributeValue;
//	}

//	private static void ParseCallForSpeakerDetail(
//		CallForSpeakerRequest callForSpeakerRequest,
//		string attributeValue,
//		string line)
//	{
//		if (line.Contains(MarkdownCallForSpeakersAttributes.Permalink))
//			callForSpeakerRequest.Permalink = attributeValue;
//		else if (line.Contains(MarkdownCallForSpeakersAttributes.Status))
//			callForSpeakerRequest.Status = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.CallForSpeakerUrl))
//			callForSpeakerRequest.CallForSpeakerUrl = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.CallForSpeakersStartDate))
//			callForSpeakerRequest.CallForSpeakerStartDate = GetDateOnly(MarkdownCallForSpeakersAttributes.CallForSpeakersStartDate, attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.CallForSpeakersEndDate))
//			callForSpeakerRequest.CallForSpeakerEndDate = GetDateOnly(MarkdownCallForSpeakersAttributes.CallForSpeakersEndDate, attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.IncludesSpeakerHonorarium))
//			callForSpeakerRequest.IncludesSpeakerHonorarium = bool.Parse(attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.SpeakerHonorariumAmount))
//			callForSpeakerRequest.SpeakerHonorariumAmount = decimal.Parse(attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.SpeakerHonorariumCurrency))
//			callForSpeakerRequest.SpeakerHonorariumCurrency = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.SpeakerHonorariumNotes))
//			callForSpeakerRequest.SpeakerHonorariumNotes = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.AreTravelExpensesCovered))
//			callForSpeakerRequest.AreTravelExpenseCovered = bool.Parse(attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.TravelNotes))
//			callForSpeakerRequest.TravelNotes = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.AreAccommodationsCovered))
//			callForSpeakerRequest.AreAccommodationsCovered = bool.Parse(attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.AccomodationNotes))
//			callForSpeakerRequest.AccomodationNotes = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.IsEventFeeCovered))
//			callForSpeakerRequest.EventFeeCovered = bool.Parse(attributeValue);
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.EventFeeNotes))
//			callForSpeakerRequest.EventFeeNotes = attributeValue;
//		else if (line.StartsWith(MarkdownCallForSpeakersAttributes.SubmissionLimit))
//			callForSpeakerRequest.SubmissionLimit = int.Parse(attributeValue);
//	}

//	private static DateOnly GetDateOnly(string attributeName, string attributeValue)
//	{
//		if (DateOnly.TryParse(attributeValue, out DateOnly date))
//			return date;
//		else
//			throw new ArgumentException($"The '{attributeName[2..]}");
//	}

//}