namespace Lab1.PersonLib;

/// <summary>
/// Person list class
/// </summary>
public class PersonList
{   
    /// <summary>
    /// Person list constructor
    /// </summary>
    public PersonList()
    {
        _data = new Person[_defaultCapacity];
        Capacity = _defaultCapacity;
    }

    /// <summary>
    /// Constructor specifies storage capcity
    /// </summary>
    /// <param name="capacity">Capacity</param>
    public PersonList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException();
        }

        _data = new Person[capacity];
        Capacity = capacity;
    }   

    /// <summary>
    /// Count of elements
    /// </summary>
    /// <value></value>
    public int Size { get; private set; }

    /// <summary>
    /// Storage capacity
    /// </summary>
    /// <value></value>
    public int Capacity { get; private set; }

    /// <summary>
    /// Add element to list
    /// </summary>
    /// <param name="person">Person</param>
    public void Add(Person person)
    {
        if (Size == Capacity)
        {
            ExpandData();
        }
        
        _data[Size] = person;
        ++Size;
    }

    /// <summary>
    /// Get and erase last list element
    /// </summary>
    /// <returns>Last element</returns>
    public Person Pop()
    {
        var last = _data[Size - 1];
        Erase(Size - 1);
        return last ?? new Person();
    }

    /// <summary>
    /// Erase list element at index
    /// </summary>
    /// <param name="index">Index of element</param>
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

    /// <summary>
    /// Retrieves person at index
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Person at index</returns>
    public Person At(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        return _data[index] ?? new Person();
    }

    /// <summary>
    /// Searching for person
    /// </summary>
    /// <param name="person">Person</param>
    /// <param name="begin">Search start index</param>
    /// <returns>Person index or -1</returns>
    public int Search(Person person, int begin = 0)
    {
        if (begin < 0 || begin >= Size)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = begin; i < Size; ++i)
        {
            if (At(i).Equals(person))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Erase all list elements
    /// </summary>
    public void Clear()
    {
        _data = new Person[Capacity];
        Size = 0;
    }

    /// <summary>
    /// Expand data storage
    /// </summary>
    private void ExpandData()
    {
        Capacity *= _growthFactor;
        
        var newData = new Person[Capacity];
        Array.Copy(_data, newData, Size);
        _data = newData;
    }

    /// <summary>
    /// Default capacity size
    /// </summary>
    private const int _defaultCapacity = 10;

    //TODO: growthFactor
    /// <summary>
    /// Multiplier of capacity
    /// </summary>
    private const int _growthFactor = 2;

    /// <summary>
    /// Data storage
    /// </summary>
    private Person?[] _data;
}
