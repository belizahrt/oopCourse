namespace Lab1.PersonLib;
public class PersonList
{   
    public PersonList()
    {
        _data = new Person[_defaultCapacity];
        Capacity = _defaultCapacity;
    }

    public PersonList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException();
        }

        _data = new Person[capacity];
        Capacity = capacity;
    }   

    public int Size { get; private set; }
    public int Capacity { get; private set; }

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

    public void Erase(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = index; i < Size - 1; ++i)
        {
            _data[i] = _data[i + 1];
        }
        
        _data[Size - 1] = null;
        --Size;
    }

    public Person At(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        return _data[index];
    }

    public int Search(Person person, int begin = 0)
    {
        if (begin < 0 || begin >= Size)
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
        
        var newData = new Person[Capacity];
        Array.Copy(_data, newData, Size);
        _data = newData;
    }

    private const int _defaultCapacity = 10;
    private const int _capacityFactor = 2;
    private Person[] _data;
}
