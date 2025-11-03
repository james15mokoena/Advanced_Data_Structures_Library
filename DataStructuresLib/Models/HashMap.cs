namespace DataStructuresLib.Models;

/// <summary>
/// The implementation of the Map ADT with a hash table, that uses the MAD (Multiply-Add and Divide)
/// compression function to eliminate repeated patterns in the set of hash codes.<br /> <br />
/// <b>MAD = [(ai + b) mod p] mod N</b>, where <i>N</i> is the size of the bucket array, <i>p</i> is a
/// prime number larger than <i>N</i> and <i>a</i> and <i>b</i> are integers chosen at random from the
/// interval [0,p-1], with a > 0.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class HashMap<TKey, TValue> : IMap<TKey, TValue>
{

    /// <summary>
    /// The default capacity of the bucket array.
    /// </summary>
    private const int DefaultCapacity = 10;

    /// <summary>
    /// The number of entries in the map.
    /// </summary>
    private int _size;

    /// <summary>
    /// The prime factor.
    /// </summary>
    private int _prime;

    /// <summary>
    /// The scale factor.
    /// </summary>
    private int _scale;

    /// <summary>
    /// The shift factor.
    /// </summary>
    private int _shift;

    /// <summary>
    /// The underlying bucket array, which consists of doubly linked lists as buckets for collision
    /// handling or storing entries with the same key.
    /// </summary>
    private DLinkedList<MapEntry<TKey, TValue>>[]? _table;
    
    /// <summary>
    /// Creates a hash table with the specified capacity.<br /><br/>
    /// TC:O(n^2), because it depends on the <b>FindLargerPrimer</b>
    /// method which runs in O(n^2).
    /// </summary>
    /// <param name="capacity"></param>
    public HashMap(int capacity)
    {
        // create the table
        CreateTable(capacity);

        InitMAD();

        _size = 0;
    }

    /// <summary>
    /// Creates a hash table with the default capacity of 10.
    /// </summary>
    public HashMap() : this(DefaultCapacity) { }

    /// <summary>
    /// It initializes the parameters of the MAD compression function.<br /> <br />
    /// TC: O(D * sqrt(capacity))
    /// </summary>
    private void InitMAD()
    {
        // find the prime number larger than capacity.
        _prime = FindLargerPrime(_table!.Length);

        // will generate random numbers for the scale and shift factors.
        Random random = new();

        // compute the scale and shift factors.
        _scale = random.Next(1, _prime);
        _shift = random.Next(_prime);
    }
    /// <summary>
    /// Creates the underlying hash table.<br /> <br />
    /// TC: O(n), where n is the capacity of the hash table.
    /// </summary>
    /// <param name="capacity"></param>
    private void CreateTable(int capacity)
    {
        // create the bucket array.
        if (capacity < DefaultCapacity)
        {
            _table = new DLinkedList<MapEntry<TKey, TValue>>[DefaultCapacity];

            for (int i = 0; i < _table.Length; ++i)
                _table[i] = new();          // create a bucket.
        }
        else
        {
            _table = new DLinkedList<MapEntry<TKey, TValue>>[capacity];
            for (int i = 0; i < _table.Length; ++i)
                _table[i] = new();          // create a bucket.
        }
    }

    /// <summary>
    /// Returns the underlying bucket array or table. <br /> <br />
    /// TC: O(1)
    /// </summary>
    /// <returns></returns>
    public DLinkedList<MapEntry<TKey, TValue>>[]? GetTable() => _table;

    /// <summary>
    /// Returns the maximum number of entries that can be stored in the map.<br /> <br />
    /// TC: O(1)
    /// </summary>
    /// <returns></returns>
    public int? GetCapacity() => _table?.Length;

    /// <summary>
    /// Returns the value of the scale factor.<br /> <br />
    /// TC: O(1)
    /// </summary>
    /// <returns></returns>
    public int GetScale() => _scale;

    /// <summary>
    /// Returns the value of the shift factor. <br /> <br />
    /// TC: O(1)
    /// </summary>
    /// <returns></returns>
    public int GetShift() => _shift;

    /// <summary>
    /// Returns the value of the prime. <br /> <br />
    /// TC: O(1)
    /// </summary>
    /// <returns></returns>
    public int GetPrime() => _prime;

    /// <summary>
    /// Checks if the argument <i>num</i> is a prime number, by checking if it has a divisor <b>d</b> such
    /// that d is in the range [2 , sqrt(num)], in which case num would not be a prime number by the <b>Primality
    /// Test</b>: <i>"Let n > 1 be an integer. Then n is prime if and only if it has no divisors <i>d</i> such that
    /// d is in the range [2, sqrt(num)]"</i>.<br /><br />
    /// 
    /// TC: O(sqrt(num)) = O(sqrt(n))
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        if (num <= 3) return true;  // includes 2 and 3

        // skip even numbers since they are never a prime number.
        if (num % 2 == 0) return false;

        // compute the square root of this odd number.
        int squareRoot = (int)Math.Sqrt(num);

        // only test odd numbers, because they can be prime numbers, starting at 3, 5, 7, etc.
        for (int divisor = 3; divisor <= squareRoot; divisor += 2)
            if (num % divisor == 0) return false;

        return true;
    }

    /// <summary>
    /// Given the argument, it finds a prime number that is larger than the argument. <br /><br />
    /// TC: O(sqrt(num) * D), where D is the distance between num and the new prime number.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private static int FindLargerPrime(int num)
    {
        // check for a larger prime number
        ++num;          // O(1)

        // check if its even.
        if (num % 2 == 0) ++num;        // O(1)

        while (!IsPrime(num))       // O(sqrt(num)) * O(d) = O(sqrt(num) * d)
            num += 2;       // test odd numbers.       O(1)     

        return num;
    }

    /// <summary>
    /// Resizes the hash table, whenever the load factor (size/capacity) > 0.7.<br /> <br />
    /// TC: O(sqrt(capacity) * D), where D is the distance from the capacity to the new prime number.
    /// </summary>
    /// <returns></returns>
    private void ResizeTable()
    {        
        // resize the hash table when it is 70% filled.
        if ((double) _size / (double) _table!.Length >= 0.7)            // O(1)
        {
            int newCapacity = _table.Length * 2;                        // O(1)
            DLinkedList<MapEntry<TKey, TValue>>[] oldTable = _table;    // O(1)

            CreateTable(newCapacity);       // O(n)

            // reset the MAD compression function's parameters.
            InitMAD();      // O(D * sqrt(num))

            // reset the size
            _size = 0;

            // copy the entries to the new hash table
            foreach(var bucket in oldTable)
            {
                if(bucket.Size() > 0)
                {
                    foreach(var entry in bucket)
                    {
                        if(entry != null)
                            Put(entry.GetKey()!, entry.GetValue()!);
                    }
                }                
            }
        }
    }
    
    /// <summary>
    /// Hashes or converts the key into an index in the hash table, using the MAD compression function.
    /// <br /> <br />
    /// MAD (Multiply-Add and Divide) = ([scale * hashCode + shift] mod prime) mod capacity. <br /><br />
    /// TC: O(1)
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int ComputeHash(TKey key) => Math.Abs(((_scale * key!.GetHashCode() + _shift) % _prime) % _table!.Length);

    // TC: O(n), where n is the number of entries in the map.
    public IEnumerable<IEntry<TKey, TValue>> EntrySet()
    {
        if (!IsEmpty())
        {
            foreach (var bucket in _table!)
            {
                if (!bucket.IsEmpty())
                {
                    foreach (var entry in bucket)
                        yield return entry;
                }
            }
        }
    }

    // TC: O(n), where n is the number of entries in the map.
    public TValue? GetValue(TKey key)
    {
        int hash = ComputeHash(key);

        if (hash >= 0 && hash < _table?.Length && _table[hash] is DLinkedList<MapEntry<TKey, TValue>> bucket)
        {
            foreach (MapEntry<TKey, TValue> entry in bucket)
            {
                if (entry != null && entry.GetKey()!.Equals(key))
                    return entry.GetValue();
            }
        }

        return default;
    }

    public bool IsEmpty() => _size == 0;

    // TC: O(n), where n is the number of entries in the map.
    public IEnumerable<TKey> KeySet()
    {
        if (!IsEmpty())
        {
            foreach (var bucket in _table!)
            {
                if (!bucket.IsEmpty())
                {
                    foreach (var entry in bucket)
                        yield return entry.GetKey()!;
                }
            }
        }
    }

    // TC: O(n), becuase it depends on the ResizeTable() method that runs in O(n).
    public TValue? Put(TKey key, TValue value)
    {
        // O(1)
        int hash = ComputeHash(key);

        // O(n)
        if (hash >= 0 && hash < _table?.Length && (GetValue(key) is null || GetValue(key)!.Equals(default(TValue))) && _table[hash] is DLinkedList<MapEntry<TKey, TValue>> bucket)
        {
            
            // O(1)
            if ((double)_size / (double)_table.Length >= 0.7)
            {
                // O(1)
                bucket.AddLast(new(key, value));
                // O(1)
                ++_size;
                // O()
                ResizeTable();
                return default;
            }
            else
                bucket.AddLast(new(key, value));

            ++_size;
            return default;
        }
        else if (hash >= 0 && hash < _table?.Length && GetValue(key) is TValue oldValue && _table[hash] is DLinkedList<MapEntry<TKey, TValue>> bucket1)
        {
            bucket1.Set(bucket1.FindPosition(new(key, oldValue)), new(key, value));
            return oldValue;
        }

        return default;
    }

    // TC: O(n), because it depends on methods that run in O(n) or less.
    public TValue? Remove(TKey key)
    {
        int hash = ComputeHash(key);

        if (hash >= 0 && hash < _table!.Length && (GetValue(key) != null || !GetValue(key)!.Equals(default(TValue))) && _table[hash] is DLinkedList<MapEntry<TKey, TValue>> bucket)
        {
            var mapEntry = bucket.Remove(bucket.FindPosition(new(key, GetValue(key))));
            if (mapEntry != null)
            {
                --_size;
                return mapEntry.GetValue();
            }
        }

        return default;
    }

    // TC: O(1)
    public int Size() => _size;

    // TC: O(m + n) = O(n), where m is the capacity of the hash table and n is the number of entries in the map.
    public IEnumerable<TValue> Values()
    {
        if (!IsEmpty())
        {
            foreach (var bucket in _table!)
            {
                if (!bucket.IsEmpty())
                {
                    foreach (var entry in bucket)
                        yield return entry.GetValue()!;
                }
            }
        }
    }

    // TC: O(m + n), where m is the number of buckets in the table and n is the of entries in the map.
    public bool Clear()
    {
        if (!IsEmpty())
        {
            foreach (var bucket in _table!)
                bucket.Clear();

            _table = null;
            _size = 0;
            return _size == 0 && _table == null;
        }
        return false;
    }

    /// <summary>
    /// A map entry.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MapEntry<K, V>(K? key, V? value) : IEntry<K, V>
    {
        /// <summary>
        /// The key of the entry.
        /// </summary>
        private readonly K? _key = key;

        /// <summary>
        /// The value of the entry.
        /// </summary>
        private V? _value = value;

        /// <summary>
        /// Returns the key of the entry.
        /// </summary>
        /// <returns></returns>
        public K? GetKey() => _key;

        /// <summary>
        /// Returns the value of the entry.
        /// </summary>
        /// <returns></returns>
        public V? GetValue() => _value;

        /// <summary>
        /// Sets the value of the entry.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(V value) => _value = value;

        public override bool Equals(object? other)
        {
            // argument is null.
            if (other is null) return false;

            // same reference
            if (ReferenceEquals(this, other)) return true;

            // objects are different types.
            if (other is not MapEntry<K, V> mapObj) return false;

            bool isEqual = mapObj.GetKey()!.Equals(_key) && mapObj.GetValue()!.Equals(_value);

            return isEqual;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine<K, V>(_key!, _value!);
        }

        public override string ToString()
        {
            return $"{_key} - {_value}";
        }
    }
}