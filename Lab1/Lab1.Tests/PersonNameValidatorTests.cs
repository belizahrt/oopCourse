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
    /// Validation invalid char count state
    /// </summary>
    private const PersonNameValidState _invalidCharCount =
        PersonNameValidState.InvalidCharCount;

    /// <summary>
    /// Testcases for valid states
    /// </summary>
    [TestCase("Yuri", ExpectedResult=_accept)]
    [TestCase("Юрий", ExpectedResult=_accept)]
    [TestCase("Yuri Andreevich", ExpectedResult=_accept)]
    [TestCase("Юрiй", ExpectedResult=_invalidLocale)]
    [TestCase("   Yuri", ExpectedResult=_invalidLocale)]
    [TestCase("Юрий28", ExpectedResult=_invalidLocale)]
    [TestCase("Kovalenko - test", ExpectedResult=_invalidLocale)]
    [TestCase("", ExpectedResult = _invalidLocale)]
    [TestCase("     ", ExpectedResult = _invalidLocale)]
    [TestCase("YuRi", ExpectedResult=_invalidShape)]
    [TestCase("коваленко", ExpectedResult=_invalidShape)]
    [TestCase("Y", ExpectedResult = _invalidCharCount)]
    [TestCase("Y", ExpectedResult = _invalidCharCount)]
    public PersonNameValidState ValidState(string name)
    {
        return PersonNameValidator.GetState(name);
    }

    /// <summary>
    /// Fix names test
    /// </summary>
    [TestCase("Yuri", ExpectedResult="Yuri")]
    [TestCase("yuri", ExpectedResult="Yuri")]
    [TestCase("YuRi", ExpectedResult="Yuri")]
    [TestCase("yUri andreeVich", ExpectedResult="Yuri Andreevich")]
    public string FixUp(string name)
    {
        return PersonNameValidator.FixUp(name);
    }   
}