namespace Lib1.PersonLib;

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
            if (value == 0 || value > MaxAge)
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

    public const ushort MaxAge = 120;

    private string _firstName = String.Empty;
    private string _secondName = String.Empty;
    private ushort _age = 0;
    private PersonSex _sex = PersonSex.Undefined;

}