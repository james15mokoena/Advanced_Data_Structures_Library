namespace DataStructuresLib.Models;

/// <summary>
/// A Position ADT.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPosition<T>
{
    /// <summary>
    /// Returns the element stored in the position.
    /// </summary>
    /// <returns></returns>
    T? GetElement();
}