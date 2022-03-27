namespace Lab1.PersonLib;

/// <summary>
/// Class describes person entity
/// </summary>
public class Person
{
    /// <summary>
    /// Constructor for Person class
    /// </summary>
    /// <param name="firstName">Person's first name</param>
    /// <param name="secondName">Person's second name</param>
    /// <param name="age">Person's age</param>
    /// <param name="sex">Person's sex</param>
    public Person(string firstName, string secondName, 
        ushort age, PersonSex sex)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        Sex = sex;
    }
    
    /// <summary>
    /// First name property
    /// </summary>
    /// <value></value>
    public string FirstName 
    { 
        get => _firstName; 
        set => _firstName = value; 
    }

    /// <summary>
    /// Second name property
    /// </summary>
    /// <value></value>
    public string SecondName 
    { 
        get => _secondName; 
        set => _secondName = value; 
    }

    /// <summary>
    /// Age property
    /// </summary>
    /// <value></value>
    public ushort Age 
    { 
        get => _age; 
        set
        { 
            if (value > MaxAge)
            {
                throw new ArgumentException("Bad age");
            }

            _age = value; 
        }
    }

    /// <summary>
    /// Sex property
    /// </summary>
    /// <value></value>
    public PersonSex Sex 
    { 
        get => _sex; 
        set => _sex = value; 
    }

    /// <summary>
    /// String representation of person
    /// </summary>
    /// <returns>String in format 1stName 2ndName Age Sex</returns>
    public override string ToString()
    {
        return $"{FirstName} {SecondName}, {Age} y/o, {Sex.ToString()}";
    }

    /// <summary>
    /// Method generates new random person
    /// </summary>
    /// <returns>Person</returns>
    public static Person GetRandPerson()
    {
        var firstNames = new string[]
        {
            "Joss", "Casey", "Jackie", "Jodie",
            "Justice", "Rene", "Frankie", "Robbie",
            "Devan", "Stevie", "Gerry", "Jaylin",
            "Tracy", "Kris", "Tommie", "Jessie"
        };

        var secondNames = new string[]
        {
            "Adams", "Wilson", "Burton", "Harris",
            "Stevens", "Robinson", "Lewis", "Walker",
            "Payne", "Baker", "Owen", "Holmes",
            "Chapman", "Webb", "Allen", "Jones"
        };        

        var random = new Random();
        string firstName = firstNames[random.Next(firstNames.Count())];
        string secondName = secondNames[random.Next(secondNames.Count())];
        ushort age = (ushort)random.Next(1, MaxAge);
        PersonSex sex = (PersonSex)random.Next(1, 3);

        return new Person(firstName, secondName, age, sex);
    }

    /// <summary>
    /// Max age constant
    /// </summary>
    public const ushort MaxAge = 120;

    /// <summary>
    /// First name
    /// </summary>
    private string _firstName = String.Empty;

    /// <summary>
    /// Second name
    /// </summary>
    private string _secondName = String.Empty;

    /// <summary>
    /// Age
    /// </summary>
    private ushort _age = 0;

    /// <summary>
    /// Sex
    /// </summary>
    private PersonSex _sex = PersonSex.Undefined;

}