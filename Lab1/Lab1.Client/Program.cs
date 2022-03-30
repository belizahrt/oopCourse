namespace Lab1.Client;
using PersonLib;
using System;

/// <summary>
/// Command funciton type alias
/// </summary>
using CommandFunc = Func<PersonLib.PersonList, PersonLib.PersonList>;

/// <summary>
/// Client program class
/// </summary>
public class Program
{
    /// <summary>
    /// Main function
    /// </summary>
    /// <returns></returns>
    static int Main()
    { 
        var CommandsMap = MakeCommandsMap();

        PrintTitle();

        var persons = new PersonList();

        string? command = String.Empty;
        Console.Write("> ");
        while ((command = Console.ReadLine()) != "exit")
        {
            if (string.IsNullOrEmpty(command))
            {
                Console.Write("> ");
                continue;
            }

            if (CommandsMap.ContainsKey(command))
            {
                persons = CommandFuncWrapper<PersonList>(
                    CommandsMap[command], persons);
            }
            else
            {
                Console.WriteLine("Неизвестная команда!");
            }
            
            Console.Write("> ");
        } 

        return 0;
    }

    /// <summary>
    /// Command errors handler function
    /// </summary>
    /// <param name="function">Command function</param>
    /// <param name="param">Param passing in command function</param>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Result of command invoke</returns>
    static T CommandFuncWrapper<T>
        (Func<T, T> function, T param)
    {
        try
        {
            return function.Invoke(param);
        }
        catch(Exception ex)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(ex.Message);

            Console.ForegroundColor = foregroundColor;
        }

        return param;
    }

    /// <summary>
    /// Make commands map
    /// </summary>
    /// <returns>Dictionary of pairs name-command</returns>
    private static Dictionary<string, CommandFunc> MakeCommandsMap()
    {
        Dictionary<string, CommandFunc> commandsMap =
            new Dictionary<string, CommandFunc>();

        commandsMap.Add("add", AddCommand);
        commandsMap.Add("rem", RemoveCommand);
        commandsMap.Add("clr", ClearCommand);
        commandsMap.Add("out", OutputCommand);

        return commandsMap;
    }
    
    /// <summary>
    /// Add new person command
    /// </summary>
    /// <param name="persons">Destination person list</param>
    /// <returns>Modified person list</returns>
    private static PersonList AddCommand(PersonList persons)
    {
        var newPerson = new Person();

        var (firstName, secondName) = ReadPersonName();
        ushort age = ReadPersonAge();
        PersonSex sex = ReadPersonSex();

        newPerson.FirstName = firstName;
        newPerson.SecondName = secondName;
        newPerson.Age = age;
        newPerson.Sex = sex;

        persons.Add(newPerson);

        return persons;
    }

    /// <summary>
    /// Read person from console
    /// </summary>
    /// <returns>First and second names pair</returns>
    private static (string, string) ReadPersonName()
    {
        Console.Write("Введите имя: ");
        string firstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите фамилию: ");
        string secondName = Console.ReadLine() ?? string.Empty;

        var nameValidator = new PersonNameValidator(firstName, secondName);

        if (nameValidator.State == PersonNameValidState.InvalidLocale)
        {
            throw new ApplicationException(
                "Имя и фамилия должны состоять из символов\n" + 
                "одного алфавита (русского или английского)\n" +
                "Не должны содержать специальных символов или цифр");
        }
        else if (nameValidator.State == PersonNameValidState.InvalidShape)
        {
            var (fixedFirstname, fixedSecondName) = nameValidator.FixUp();
            Console.Write("Вы имели ввиду " + fixedFirstname +
                " " + fixedSecondName + "? [Д]а/[Н]ет: ");

            string? answer = Console.ReadLine() ?? string.Empty; 

            switch (answer.ToLower())
            {
                case "yes":
                case "y":
                case "д":
                case "да":
                default:
                    firstName = fixedFirstname;
                    secondName = fixedSecondName;
                    break;
            }
        }

        return (firstName, secondName);        
    }

    /// <summary>
    /// Read person age from console
    /// </summary>
    /// <returns>Person age</returns>
    private static ushort ReadPersonAge()
    {
        Console.Write($"Введите возраст (0 - {Person.MaxAge}): ");

        if (!ushort.TryParse(Console.ReadLine(), out var age))
        {
            throw new ApplicationException("Неверный формат возраста");
        }

        if (age > Person.MaxAge)
        {
            throw new ApplicationException("Недопустимое значение возраста");
        }

        return age;
    }

    /// <summary>
    /// Read person sex from console
    /// </summary>
    /// <returns>Person sex</returns>
    private static PersonSex ReadPersonSex()
    {
        Console.Write($"Введите пол [М]уж/[Ж]ен: ");

        string? answer = Console.ReadLine() ?? string.Empty;
        PersonSex sex = PersonSex.Undefined;
        switch (answer.ToLower())
        {
            case "male":
            case "m":
            case "м":
            case "муж":
                sex = PersonSex.Male;
                break;
            case "female":
            case "f":
            case "ж":
            case "жен":
                sex = PersonSex.Female;
                break;
            default:
                throw new ApplicationException("Пол введен неверно");
        }

        return sex;
    }

    /// <summary>
    /// Remove person command
    /// </summary>
    /// <param name="persons">Destination person list</param>
    /// <returns>Modified person list</returns>
    private static PersonList RemoveCommand(PersonList persons)
    {
        Console.Write("Введите индекс в списке: ");
        if (!int.TryParse(Console.ReadLine(), out var index))
        {
            throw new ApplicationException("Индекс должен быть " +
                "неотрицательным числом");
        }

        persons.Erase(index);

        return persons;
    }

    /// <summary>
    /// Clear person list
    /// </summary>
    /// <param name="persons">Person list</param>
    /// <returns>Modified person list</returns>
    private static PersonList ClearCommand(PersonList persons)
    {
        persons.Clear();
        Console.WriteLine("Список очищен");
        return persons;
    }

    /// <summary>
    /// Output command
    /// </summary>
    /// <param name="persons">Origin person list</param>
    /// <returns>Modified person list</returns>
    private static PersonList OutputCommand(PersonList persons)
    {
        Console.WriteLine("Список персон:");
        for (int i = 0; i < persons.Size; ++i)
        {
            Console.WriteLine($"{i} - {persons.At(i).ToString()}");
        }

        return persons;
    }

    /// <summary>
    /// Print title
    /// </summary>
    private static void PrintTitle()
    {
        Console.WriteLine("Lab1.Persons");
        Console.WriteLine("Создан пустой список персон");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Список команд для редактирования:");
        Console.WriteLine("add - добавить новый экземпляр");
        Console.WriteLine("rem - удалить конкретный экземпляр");
        Console.WriteLine("clr - очистить список");
        Console.WriteLine("out - вывести список в консоль");
        Console.WriteLine("exit - выход");
        Console.WriteLine("---------------------------------");
    }
}
