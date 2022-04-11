
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab1.PersonLib;

namespace Lab1.Tests;

/// <summary>
/// Unit test class for PersonList class
/// </summary>
[TestFixture]
public class PersonListTest
{
    /// <summary>
    /// Complex test with 2 lists operations
    /// </summary>
    [Test]
    public void ComplexTest()
    {
        const int personsCount = 3;
        var persons1 = MakePersonList(personsCount);
        var persons2 = MakePersonList(personsCount);

        persons1.Add(Person.GetRandPerson());

        Person personForCopy = persons1.At(1);
        persons2.Add(personForCopy);
        Assert.AreEqual(personForCopy, persons2.At(persons2.Size - 1), 
            "Persons are not the same");

        persons1.Erase(1);
        Assert.AreEqual(persons1.Search(personForCopy), -1);
        Assert.AreNotEqual(persons2.Search(personForCopy), -1);

        persons2.Clear();
        Assert.AreEqual(persons2.Size, 0);
    }

    [Test, Description("Тест на добавление персоны в список")]
    public void AddPerson()
    {
        const int personsCount = 5;
        var persons = MakePersonList(personsCount);
        persons.Add(Person.GetRandPerson());

        Assert.AreEqual(persons.Size, personsCount + 1);
    }

    /// <summary>
    /// Expand data storage test
    /// </summary>
    [Test]
    public void ExpandData()
    {
        const int personsCount = 10;
        const int capacity = 10;
        var persons = MakePersonList(personsCount, capacity);
        persons.Add(Person.GetRandPerson());

        Assert.AreNotEqual(persons.Capacity, capacity);
    }

    /// <summary>
    /// Get person with At method test
    /// </summary>
    [Test]
    public void GetPerson()
    {
        var person = Person.GetRandPerson();
        var persons = new PersonList();
        persons.Add(person);       

        Assert.AreEqual(persons.At(0), person);
    }

    /// <summary>
    /// Clear person list test
    /// </summary>
    [Test]
    public void ClearPersons()
    {
        const int personsCount = 20;
        var persons = MakePersonList(personsCount);
        persons.Clear();

        Assert.AreEqual(persons.Size, 0);
    }

    /// <summary>
    /// Pop person from list test
    /// </summary>
    [Test]
    public void PopPerson()
    {
        const int personsCount = 20;
        var persons = MakePersonList(personsCount);
        var lastPerson = Person.GetRandPerson();
        persons.Add(lastPerson);
        
        Person poppedPerson = persons.Pop();

        Assert.AreEqual(poppedPerson, lastPerson);
        Assert.AreEqual(persons.Size, personsCount);
    }

    /// <summary>
    /// Test cases for erase person from list
    /// </summary>
    /// <param name="listSize">Persons in list count</param>
    /// <param name="index">Index for erase</param>
    [TestCase(100, 0)]
    [TestCase(100, 49)]
    [TestCase(100, 99)]
    public void ErasePerson(int listSize, int index)
    {
        var persons = new PersonList(listSize);
        var personsGoal = new List<Person>(listSize);
        for (int i = 0; i < listSize; ++i)
        {
            var person = Person.GetRandPerson();
            persons.Add(person);
            personsGoal.Add(person);
        }
        
        persons.Erase(index);
        personsGoal.RemoveAt(index);

        Assert.AreEqual(persons.Size, personsGoal.Count);

        for (int i = 0; i < personsGoal.Count; ++i)
        {
            if (!personsGoal[i].Equals(persons.At(i)))
            {
                Assert.Fail();
            }
        }      
    }

    /// <summary>
    /// Search person in list test
    /// </summary>
    [Test]
    public void SearchPerson()
    {
        const int personsCount = 53;
        var persons = MakePersonList(personsCount);
        
        var personForSearch = Person.GetRandPerson();
        persons.Add(personForSearch);
        persons.Add(Person.GetRandPerson());
        persons.Add(Person.GetRandPerson());
        persons.Add(Person.GetRandPerson());

        Assert.AreEqual(persons.Search(personForSearch), personsCount);

        var personNotIn = Person.GetRandPerson();
        Assert.AreEqual(persons.Search(personNotIn), -1);
    }

    /// <summary>
    /// Make person list helper function
    /// </summary>
    /// <param name="personsCount">Persons count</param>
    /// <param name="capacity">List capacity</param>
    /// <returns></returns>
    private PersonList MakePersonList(int personsCount, int capacity=10)
    {
        var persons = new PersonList(capacity);

        for (int i = 0; i < personsCount; ++i)
        {
            persons.Add(Person.GetRandPerson());
        }

        return persons;
    }
}
