using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.PersonLib;

/// <summary>
/// Person name validator class
/// </summary>
public static class PersonNameValidator
{
    /// <summary>
    /// Valid state
    /// </summary>
    /// <value></value>
    public static PersonNameValidState GetState(string name)
    {
        Locale nameLocale = GetTextLocale(name);

        if (nameLocale == Locale.Undefined)
        {
            return PersonNameValidState.InvalidLocale;
        }

        if (!IsValidCharCount(name))
        {
            return PersonNameValidState.InvalidCharCount;
        }

        if (!IsValidNameShape(name))
        {
            return PersonNameValidState.InvalidShape;
        }

        return PersonNameValidState.Acceptable;
    }

    /// <summary>
    /// Invalidate 1st name and 2nd name. DOESN'T fix alphabets
    /// </summary>
    /// <returns>Pair of 1st and 2nd names</returns>
    public static string FixUp(string name)
    {
        return FixNameShape(name);
    }

    /// <summary>
    /// Reformat name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>Fixed name</returns>
    private static string FixNameShape(string name)
    {
        name = name.Trim();

        var words = name.Split();

        int wordsCount = words.Count();
        if (wordsCount > MaxWordsInName)
        {
            wordsCount = MaxWordsInName;
        }
        
        string fixedName = string.Empty;
        for (int i = 0; i < wordsCount; ++i)
        {
            if (words[i].Count() < 2)
            {
                continue;
            }

            fixedName += char.ToUpper(words[i].First());
            fixedName += new string(words[i].Substring(1).Select(
                (c) => Char.ToLower(c)).ToArray());

            fixedName += ' ';
        }

        fixedName = fixedName.TrimEnd();

        return fixedName;
    }

    /// <summary>
    /// Detect text locale
    /// </summary>
    /// <param name="text"></param>
    /// <returns>Locale</returns>
    public static Locale GetTextLocale(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return Locale.Undefined;
        }

        text = text.ToUpper();

        Locale firstCharLocale = GetLetterLocale(text[0]);
        for (int i = 1; i < text.Count(); ++i)
        {
            if (char.IsWhiteSpace(text[i]))
            {
                continue;
            }

            var charLocale = GetLetterLocale(text[i]);

            if (charLocale == Locale.Undefined 
                || (charLocale != firstCharLocale))
            {
                return Locale.Undefined;
            }
        }

        return firstCharLocale;
    }

    /// <summary>
    /// Detect letter locale
    /// </summary>
    /// <param name="letter">Letter</param>
    /// <returns>Locale of letter</returns>
    private static Locale GetLetterLocale(char letter)
    {
        // upper case unicode sets
        var localeRanges = new Dictionary<Locale, (uint, uint)>
        {
            { Locale.English, (65, 122) },
            { Locale.Russian, (1040, 1103) }
        };

        foreach (var locale in localeRanges)
        {
            if (letter >= locale.Value.Item1
                && letter <= locale.Value.Item2)
            {
                return locale.Key;
            }
        }

        return Locale.Undefined;
    }

    /// <summary>
    /// Check for valid name characters count
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>Is valid</returns>
    private static bool IsValidCharCount(string name)
    {
        var words = name.Trim().Split();

        foreach (var word in words)
        {
            if (word.Count() < 2)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Check for valid name shape
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>Is valid</returns>
    private static bool IsValidNameShape(string name)
    {
        var words = name.Trim().Split();
        int wordsCount  = words.Count();
        
        if (wordsCount == 0 || wordsCount > MaxWordsInName)
        {
            return false;
        } 

        foreach (var word in words)
        {
            var noLowerChars = word.Substring(1).Where(
                (c) => !Char.IsLower(c));

            if (!Char.IsUpper(word.First())
                || noLowerChars.Count() != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Max words count in name
    /// </summary>
    public const int MaxWordsInName = 2;
}