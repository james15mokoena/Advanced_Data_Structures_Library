namespace DataStructuresLib.Models;

/// <summary>
/// The Entry ADT.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IEntry<TKey, TValue>
{
    /// <summary>
    /// Returns the key of an entry.
    /// </summary>
    /// <returns></returns>
    TKey? GetKey();

    /// <summary>
    /// Returns the value of an entry.
    /// </summary>
    /// <returns></returns>
    TValue? GetValue();
}