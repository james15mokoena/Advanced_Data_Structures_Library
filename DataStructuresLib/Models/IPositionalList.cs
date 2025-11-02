namespace DataStructuresLib.Models;

/// <summary>
/// A PositionalList ADT.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPositionalList<T>
{
    /// <summary>
    /// Returns the position of the first element of the list, or null if empty.
    /// </summary>
    /// <returns></returns>
    IPosition<T>? First();

    /// <summary>
    /// Returns the position of the last element of the list, or null if empty.
    /// </summary>
    /// <returns></returns>
    IPosition<T>? Last();

    /// <summary>
    /// Returns the position of the list immediately <b>before</b> position pos, or
    /// null if pos is the first position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    IPosition<T>? Before(IPosition<T>? pos);

    /// <summary>
    /// Returns the position of the list immediately <b>after</b> position pos, or null
    /// if pos is the last position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    IPosition<T>? After(IPosition<T>? pos);

    /// <summary>
    /// Returns true if the list does not contain any elements.
    /// </summary>
    /// <returns></returns>
    bool IsEmpty();

    /// <summary>
    /// Returns the number of elements in the list.
    /// </summary>
    /// <returns></returns>
    int Size();

    /// <summary>
    /// Inserts a new element at the front of the list, returning the
    /// position of the new element.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    IPosition<T>? AddFirst(T element);

    /// <summary>
    /// Inserts a new element at the back of the list, returning the
    /// position of the new element.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    IPosition<T>? AddLast(T element);

    /// <summary>
    /// Inserts a new element in the list, just before position pos, returning
    /// the position of the new element.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    IPosition<T>? AddBefore(IPosition<T>? pos, T element);

    /// <summary>
    /// Inserts a new element in the list, just after position pos, returning
    /// the position of the new element.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    IPosition<T>? AddAfter(IPosition<T>? pos, T element);

    /// <summary>
    /// Replaces the element at position pos with the new element, returning the
    /// element formerly at position pos.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="newElement"></param>
    /// <returns></returns>
    T? Set(IPosition<T>? pos, T newElement);

    /// <summary>
    /// Removes and returns the element at position <b>pos</b> in the list, invalidating
    /// the position.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    T? Remove(IPosition<T>? pos);
}