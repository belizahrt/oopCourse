
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab1.PersonLib;

namespace Lab1.Tests;

[TestFixture]
public class PersonListTest
{
    [Test]
    public void AddPerson()
    {
        const int personsCount = 5;
        var persons = MakePersonList(personsCount);
        persons.Add(Person.GetRandPerson());

        Assert.AreEqual(persons.Size, personsCount + 1);
    }

    [Test]
    public void ExpandData()
    {
        const int personsCount = 10;
        const uint capacity = 10;
        var persons = MakePersonList(personsCount, capacity);
        persons.Add(Person.GetRandPerson());

        Assert.AreNotEqual(persons.Capacity, capacity);
    }

    [Test]
    public void GetPerson()
    {
        var person = Person.GetRandPerson();
        var persons = new PersonList();
        persons.Add(person);       

        Assert.AreEqual(persons.At(0), person);
    }

    [Test]
    public void ClearPersons()
    {
        const int personsCount = 20;
        var persons = MakePersonList(personsCount);
        persons.Clear();

        Assert.AreEqual(persons.Size, 0);
    }

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

    [TestCase(100, 0)]
    [TestCase(100, 49)]
    [TestCase(100, 99)]
    public void ErasePerson(uint listSize, uint index)
    {
        var persons = new PersonList(listSize);
        var personsGoal = new List<Person>((int)listSize);
        for (int i = 0; i < listSize; ++i)
        {
            var person = Person.GetRandPerson();
            persons.Add(person);
            personsGoal.Add(person);
        }
        
        persons.Erase(index);
        personsGoal.RemoveAt((int)index);

        Assert.AreEqual(persons.Size, personsGoal.Count);

        for (int i = 0; i < personsGoal.Count; ++i)
        {
            if (!personsGoal[i].Equals(persons.At((uint)i)))
            {
                Assert.Fail();
            }
        }      
    }

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

    private PersonList MakePersonList(int personsCount, uint capacity=10)
    {
        var persons = new PersonList(capacity);

        for (int i = 0; i < personsCount; ++i)
        {
            persons.Add(Person.GetRandPerson());
        }

        return persons;
    }
}
