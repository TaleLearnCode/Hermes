namespace Hermes.Extensions;

internal static class TagExtensions
{

	internal static Tag ToTag(this string tag, HermesContext context)
	{
		Tag? tagRecord = context.Tags.FirstOrDefault(t => t.TagName == tag);
		if (tagRecord is null)
		{
			tagRecord = new Tag
			{
				TagName = tag
			};
			context.Tags.Add(tagRecord);
			context.SaveChanges();
		}
		return tagRecord;
	}

	internal static List<Tag> ToTagList(this List<string> tags, HermesContext context)
	{
		List<Tag> response = [];
		foreach (string tag in tags)
			response.Add(tag.ToTag(context));
		return response;
	}

	internal static List<PresentationTag> ToPresentationTagList(this List<string> presentationTags, HermesContext context)
	{
		List<PresentationTag> response = [];
		foreach (string tag in presentationTags)
			response.Add(new PresentationTag
			{
				Tag = tag.ToTag(context)
			});
		return response;
	}

}