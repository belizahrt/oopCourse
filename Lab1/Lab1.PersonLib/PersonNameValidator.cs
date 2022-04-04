using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.PersonLib;

/// <summary>
/// Person name validator class
/// </summary>
public class PersonNameValidator
{
    /// <summary>
    /// Constructor for class
    /// </summary>
    public PersonNameValidator()
    {
    }

    /// <summary>
    /// Constructor specifies 1st name and 2nd name
    /// </summary>
    /// <param name="name"></param>
    /// //TODO: XML
    public PersonNameValidator(string firstName, string secondName)
    {
        FirstName = firstName;
        SecondName = secondName;
    }

    /// <summary>
    /// Constructor specifies class Person
    /// </summary>
    /// <param name="person"></param>
    public PersonNameValidator(Person person)
        : this(person.FirstName, person.SecondName)
    {
    }

    /// <summary>
    /// First name
    /// </summary>
    /// <value></value>
    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    /// <summary>
    /// Second name
    /// </summary>
    /// <value></value>
    public string SecondName
    {
        get => _secondName;
        set => _secondName = value;
    }

    /// <summary>
    /// Valid state
    /// </summary>
    /// <value></value>
    public PersonNameValidState State
    {
        get
        {
            Locale firstNameLocale = GetTextLocale(FirstName);
            Locale secondNameLocale = GetTextLocale(SecondName);

            if (firstNameLocale == Locale.Undefined 
                || firstNameLocale != secondNameLocale)
            {
                return PersonNameValidState.InvalidLocale;
            }

            if (!IsValidNameShape(FirstName) 
                || !IsValidNameShape(SecondName))
            {
                return PersonNameValidState.InvalidShape;
            }

            return PersonNameValidState.Acceptable;
        }
    }

    /// <summary>
    /// Invalidate 1st name and 2nd name. DOESN'T fix alphabets
    /// </summary>
    /// <returns>Pair of 1st and 2nd names</returns>
    public (string, string) FixUp()
    {
        string firstName = FixNameShape(FirstName);
        string secondName = FixNameShape(SecondName);

        return (firstName, secondName);
    }

    /// <summary>
    /// Reformat name
    /// </summary>
    /// <param name="name">Name</param>
    /// <returns>Fixed name</returns>
    private string FixNameShape(string name)
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
    private static Locale GetTextLocale(string text)
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
        //TODO: RSDN
        // upper case unicode sets
        var LocaleRanges = new Dictionary<Locale, (uint, uint)>
        {
            { Locale.English, (65, 122) },
            { Locale.Russian, (1040, 1103) }
        };

        foreach (var locale in LocaleRanges)
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
    /// Check for valid name shape
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Is valid</returns>
    private static bool IsValidNameShape(string name)
    {
        var words = name.Split();
        int wordsCount  = words.Count();
        
        if (wordsCount == 0 || wordsCount > 2)
        {
            return false;
        } 

        foreach (var word in words)
        {
            if (word.Count() < 2)
            {
                return false;
            }

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


    public const int MaxWordsInName = 2;

    /// <summary>
    /// First name
    /// </summary>
    private string _firstName = String.Empty;

    /// <summary>
    /// Second name
    /// </summary>
    private string _secondName = String.Empty;
}