﻿using System.Text;

namespace Hermes.Extensions;

internal static class StringExtensions
{

	internal static string SplitCamelCase(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return input;

		StringBuilder sb = new();
		sb.Append(char.ToUpper(input[0]));

		for (int i = 1; i < input.Length; i++)
		{
			if (char.IsUpper(input[i]) && !char.IsUpper(input[i - 1]))
				sb.Append(' ');

			sb.Append(input[i]);
		}

		return sb.ToString();
	}

	internal static string Capitalize(this string input)
	{
		if (string.IsNullOrWhiteSpace(input))
			return input;

		return char.ToUpper(input[0]) + input[1..].ToLower();
	}

}