namespace Lab1.PersonLib;
public class PersonList
{   
    public PersonList()
    {
        _data = new Person[_defaultCapacity];
        Capacity = _defaultCapacity;
    }

    public PersonList(uint capacity)
    {
        _data = new Person[capacity];
        Capacity = capacity;
    }   

    public uint Size { get; private set; }
    public uint Capacity { get; private set; }

    public void Add(Person person)
    {
        if (Size == Capacity)
        {
            ExpandData();
        }

        _data[Size] = person;
        ++Size;
    }

    public Person Pop()
    {
        var last = _data[Size - 1];
        Erase(Size - 1);
        return last;
    }

    public void Erase(uint index)
    {
        if (index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        for (uint i = index; i < Size; ++i)
        {
            _data[i] = _data[i + 1];
        }
        
        _data[Size - 1] = null;
        --Size;
    }

    public Person At(uint index)
    {
        if (index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        return _data[index];
    }

    public int Search(Person person, int begin = 0)
    {
        if (begin >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = begin; i < Size; ++i)
        {
            if (_data[i].Equals(person))
            {
                return i;
            }
        }

        return -1;
    }

    public void Clear()
    {
        _data = new Person[Capacity];
        Size = 0;
    }

    private void ExpandData()
    {
        Capacity *= _capacityFactor;
        
        Person[] newData = new Person[Capacity];
        Array.Copy(_data, newData, Size);
        _data = newData;
    }

    private const uint _defaultCapacity = 10;
    private const uint _capacityFactor = 2;
    private Person[] _data;
}
