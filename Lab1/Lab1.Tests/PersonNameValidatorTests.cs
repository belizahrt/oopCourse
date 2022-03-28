using NUnit.Framework;
using Lab1.PersonLib;

namespace Lab1.Tests;

/// <summary>
/// Unit test class for PersonNameValidator class
/// </summary>
[TestFixture]
public class PersonNameValidatorTest
{
    /// <summary>
    /// Validation Accept state
    /// </summary>
    private const PersonNameValidState _accept = 
        PersonNameValidState.Acceptable;

    /// <summary>
    /// Validation invalid locale state
    /// </summary>
    private const PersonNameValidState _invalidLocale = 
        PersonNameValidState.InvalidLocale;

    /// <summary>
    /// Validation invalid shape state
    /// </summary>
    private const PersonNameValidState _invalidShape = 
        PersonNameValidState.InvalidShape;

    /// <summary>
    /// Testcases for valid states
    /// </summary>
    [TestCase("Yuri", "Kovalenko", ExpectedResult=_accept)]
    [TestCase("Юрий", "Коваленко", ExpectedResult=_accept)]
    [TestCase("Yuri Andreevich", "Kovalenko", ExpectedResult=_accept)]
    [TestCase("Yuri", "Andreevich Kovalenko", ExpectedResult=_accept)]
    [TestCase("Yuri Andreevich", "Csharp Kovalenko", ExpectedResult=_accept)]
    [TestCase("Юрий", "Kovalenko", ExpectedResult=_invalidLocale)]
    [TestCase("Yuri", "Коваленко", ExpectedResult=_invalidLocale)]
    [TestCase("Юриi", "Kovalenko", ExpectedResult=_invalidLocale)]
    [TestCase("Юрий28", "Kovalenko", ExpectedResult=_invalidLocale)]
    [TestCase("Юрий", "Kovalenko - test", ExpectedResult=_invalidLocale)]
    [TestCase("YuRi", "KovaLenko", ExpectedResult=_invalidShape)]
    [TestCase("Юрий", "коваленко", ExpectedResult=_invalidShape)]
    public PersonNameValidState ValidState(string firstName,
        string secondName)
    {
        var validator = new PersonNameValidator(firstName, secondName);

        return validator.State;
    }

    /// <summary>
    /// Fix names test
    /// </summary>
    [TestCase("Yuri", "Correct Surname", ExpectedResult="Yuri")]
    [TestCase("yuri", "Correct Surname", ExpectedResult="Yuri")]
    [TestCase("YuRi", "Correct Surname", ExpectedResult="Yuri")]
    [TestCase("yUri andreeVich", "Correct Surname", 
        ExpectedResult="Yuri Andreevich")]
    public string FixUp(string firstName,
        string secondName)
    {
        var validator = new PersonNameValidator(firstName, secondName);

        return validator.FixUp().Item1;
    }   
}