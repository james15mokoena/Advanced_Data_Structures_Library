namespace DataStructuresLib.Models;

/// <summary>
/// An abstraction of the Vertex of the Graph ADT.
/// </summary>
/// <typeparam name="TVertex"></typeparam>
/// <typeparam name="TEdge"></typeparam>
public interface IVertex<TVertex,TEdge>
{
    /// <summary>
    /// Returns the value stored in the vertex.
    /// </summary>
    /// <returns></returns>
    TVertex? GetElement();

    /// <summary>
    /// Returns a reference to the map of outgoing edges, with keys being the adjacent
    /// vertices and values being the edges.
    /// </summary>
    /// <returns></returns>
    IMap<IVertex<TVertex,TEdge>, IEdge<TEdge,TVertex>> GetOutgoingEdges();

    /// <summary>
    /// Returns a reference to the map of incoming edges, with keys being the adjacent
    /// vertices and values being the edges.
    /// </summary>
    /// <returns></returns>
    IMap<IVertex<TVertex, TEdge>, IEdge<TEdge, TVertex>> GetIncomingEdges();

    /// <summary>
    /// It is used in graph traversal algorithms to determine if the vertex has been visited.
    /// </summary>
    /// <returns>true if the vertex has been visited, otherwise false.</returns>
    bool IsVisited();
}