namespace DataStructuresLib.Models;

/// <summary>
/// The Map ADT.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IMap<TKey, TValue>
{
    /// <summary>
    /// Returns the number of entries in the map.
    /// </summary>
    /// <returns></returns>
    int Size();

    /// <summary>
    /// Returns a boolean indicating whether the map is empty.
    /// </summary>
    /// <returns></returns>
    bool IsEmpty();

    /// <summary>
    /// Returns the value associated with key, if such an entry exists; otherwise returns null.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    TValue? GetValue(TKey key);

    /// <summary>
    /// If the map does not have an entry with key equal to "key", then adds entry (key, value) to the map
    /// and returns null; else, replaces with "value" the existing value of the entry with key equal to "key"
    /// and returns the old value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    TValue? Put(TKey key, TValue value);

    /// <summary>
    /// Removes from the map the entry with key equal to "key", and returns its value; if the map has no such
    /// entry, then returns null.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    TValue? Remove(TKey key);

    /// <summary>
    /// Returns an iterable collection containing all the keys stored in the map.
    /// </summary>
    /// <returns></returns>
    IEnumerable<TKey> KeySet();

    /// <summary>
    /// Returns an iterable collection containing all the values of entries stored in the map (with repetition
    /// if multiple keys map to the same value).
    /// </summary>
    /// <returns></returns>
    IEnumerable<TValue> Values();

    /// <summary>
    /// Returns an iterable collection containing all the key-value entries in the map.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IEntry<TKey, TValue>> EntrySet();

    /// <summary>
    /// Removes all entries from the map.
    /// </summary>
    /// <returns></returns>
    bool Clear();
}