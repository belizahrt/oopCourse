namespace Lab1.Client;
using PersonLib;
using System;
using System.Text;

/// <summary>
/// Command function type alias
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
        Console.OutputEncoding = Encoding.UTF8;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Console.InputEncoding = Encoding.GetEncoding(1251);
        var commandsMap = MakeCommandsMap();

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

            if (commandsMap.ContainsKey(command))
            {
                persons = CommandFuncWrapper<PersonList>(
                    commandsMap[command], persons);
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
        var (firstName, firstNameLocale)
            = ReadPersonName("Введите имя: ");

        var (secondName, secondNameLocale)
            = ReadPersonName("Введите фамилию: ");

        if (firstNameLocale != secondNameLocale
            || firstNameLocale == Locale.Undefined)
        {
            throw new ApplicationException(_invalidNameExpectionMessage);
        }

        persons.Add(new Person()
        {
            FirstName = firstName,
            SecondName = secondName,
            Age = ReadPersonAge(),
            Sex = ReadPersonSex()
        });

        return persons;
    }

    /// <summary>
    /// Invalid name exception message
    /// </summary>
    private const string _invalidNameExpectionMessage =
        "Имя и фамилия должны состоять из символов\n" +
        "одного алфавита (русского или английского)\n" +
        "Не должны содержать специальных символов или цифр";

    /// <summary>
    /// Invalid name chars count exception message
    /// </summary>
    private const string _invalidCharCountExpectionMessage =
        "В имени должно быть два и более символов";

    /// <summary>
    /// Read person from console
    /// </summary>
    /// <returns>First and second names pair</returns>
    private static (string, Locale) ReadPersonName(string consoleText)
    {
        Console.Write(consoleText);
        string name = Console.ReadLine() ?? string.Empty;

        PersonNameValidState nameValidState
            = PersonNameValidator.GetState(name);

        if (nameValidState == PersonNameValidState.InvalidCharCount)
        {
            throw new ApplicationException(_invalidCharCountExpectionMessage);
        }

        Locale nameLocale = PersonNameValidator
            .GetTextLocale(name);

        if (nameValidState == PersonNameValidState.InvalidLocale)
        {
            throw new ApplicationException(_invalidNameExpectionMessage);
        }

        if (nameValidState == PersonNameValidState.InvalidShape)
        {
            string fixedName = PersonNameValidator.FixUp(name);
            Console.Write("Вы имели ввиду " + fixedName + "? [Д]а/[Н]ет: ");

            string? answer = Console.ReadLine() ?? string.Empty;

            switch (answer.ToLower())
            {
                case "yes":
                case "y":
                case "д":
                case "да":
                    name = fixedName;
                    break;
            }
        }

        return (name.Trim(), nameLocale);        
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
