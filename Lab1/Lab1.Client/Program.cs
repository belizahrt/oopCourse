namespace Lab1.Client;
using PersonLib;
using System;

using CommandFunc = Func<PersonLib.PersonList, PersonLib.PersonList>;

/// <summary>
/// 
/// </summary>
public class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static int Main()
    { 
        PrintTitle();

        var persons = new PersonList();

        string? command = String.Empty;
        do
        {
            Console.Write("> ");
            


        } while ((command = Console.ReadLine()) != "exit");


        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    static private Dictionary<string, CommandFunc> _CommandsMap = 
        new Dictionary<string, CommandFunc>();

    /// <summary>
    /// 
    /// </summary>
    static private void PrintTitle()
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
