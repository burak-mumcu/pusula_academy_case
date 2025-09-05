using System;
using System.Collections.Generic;
using System.Text.Json;

public class VowelSequenceExtractor
{
    public static string GetVowelSequencesAsJson(string[] words)
    {
        if (words == null || words.Length == 0)
            return "[]";

        var vowels = "aeiouAEIOU";
        var outputList = new List<object>();

        foreach (var rawWord in words)
        {
            var cleanWord = rawWord.Trim();
            var vowelChars = new List<char>();

            foreach (var ch in cleanWord)
            {
                if (vowels.IndexOf(ch) >= 0)
                {
                    vowelChars.Add(ch);
                }
            }

            var vowelSequence = new string(vowelChars.ToArray());

            outputList.Add(new
            {
                word = cleanWord,
                sequence = vowelSequence,
                length = vowelSequence.Length
            });
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        return JsonSerializer.Serialize(outputList, options);
    }
}
