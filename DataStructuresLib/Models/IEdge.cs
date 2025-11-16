namespace DataStructuresLib.Models;

/// <summary>
/// The abstraction of the Edge of the Graph ADT.
/// </summary>
/// <typeparam name="TEdge"></typeparam>
/// <typeparam name="TVertex"></typeparam>
public interface IEdge<TEdge,TVertex>
{
    /// <summary>
    /// Returns the element stored in the edge.
    /// </summary>
    /// <returns></returns>
    TEdge? GetElement();

    /// <summary>
    /// Returns a references to an array containing the endpoints of the edge.
    /// </summary>
    /// <returns></returns>
    IVertex<TVertex, TEdge>[]? GetEndpoints();
    
    /// <summary>
    /// It is used in graph traversal algorithms to determine if the edge has been visited.
    /// </summary>
    /// <returns>true if the edge has been visited, otherwise false.</returns>
    bool IsVisited();
}