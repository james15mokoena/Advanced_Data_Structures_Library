namespace DataStructuresLib.Models;

/// <summary>
/// An abstraction of the Vertex of a Graph ADT.
/// </summary>
/// <typeparam name="TElement"></typeparam>
public interface IVertex<TElement>
{
    /// <summary>
    /// Returns the value stored in the vertex.
    /// </summary>
    /// <returns></returns>
    TElement? GetElement();

    /// <summary>
    /// Returns a reference to the map of outgoing edges, with keys being the adjacent
    /// vertices and values being the edges.
    /// </summary>
    /// <returns></returns>
    IMap<IVertex<TElement>, object> GetOutgoingEdges();

    /// <summary>
    /// Returns a reference to the map of incoming edges, with keys being the adjacent
    /// vertices and values being the edges.
    /// </summary>
    /// <returns></returns>
    IMap<IVertex<TElement>, object> GetIncomingEdges();
}