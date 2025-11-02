namespace DataStructuresLib.Models;

/// <summary>
/// The abstraction of the Edge of the Graph ADT.
/// </summary>
/// <typeparam name="TElement"></typeparam>
public interface IEdge<TElement>
{
    /// <summary>
    /// Returns the element stored in the edge.
    /// </summary>
    /// <returns></returns>
    TElement? GetElement();

    /// <summary>
    /// Returns a references to an array containing the endpoints of the edge.
    /// </summary>
    /// <returns></returns>
    IVertex<object>[] GetEndpoints();
}