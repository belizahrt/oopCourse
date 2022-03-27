namespace Lab1.PersonLib;
public class Person
{
    public Person(string firstName, string secondName, 
        ushort age, PersonSex sex)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        Sex = sex;
    }
    
    public string FirstName 
    { 
        get => _firstName; 
        set => _firstName = value; 
    }

    public string SecondName 
    { 
        get => _secondName; 
        set => _secondName = value; 
    }

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

    public PersonSex Sex 
    { 
        get => _sex; 
        set => _sex = value; 
    }

    public override string ToString()
    {
        return $"{FirstName} {SecondName}, {Age} y/o, {Sex.ToString()}";
    }

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

    public const ushort MaxAge = 120;

    private string _firstName = String.Empty;
    private string _secondName = String.Empty;
    private ushort _age = 0;
    private PersonSex _sex = PersonSex.Undefined;

}