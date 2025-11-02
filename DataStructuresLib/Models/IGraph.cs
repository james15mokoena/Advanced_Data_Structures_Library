namespace DataStructuresLib.Models;

/// <summary>
/// The Graph ADT.
/// </summary>
/// <typeparam name="TVertex"></typeparam>
/// <typeparam name="TEdge"></typeparam>
public interface IGraph<TVertex, TEdge>
{
    /// <summary>
    /// Returns the number of vertices of the graph.
    /// </summary>
    /// <returns></returns>
    int NumVertives();

    /// <summary>
    /// Returns an iteration of all the vertices of the graph.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IVertex<TVertex>>? Vertices();

    /// <summary>
    /// Returns the number of edges of the graph.
    /// </summary>
    /// <returns></returns>
    int NumEdges();

    /// <summary>
    /// Returns an iteration of all the edges of the graph.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IEdge<TEdge>>? Edges();

    /// <summary>
    /// Returns the edge from vertex "origin" to vertex "destination", if one exists, otherwise
    /// returns null.
    /// </summary>
    /// <param name="orgin"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    IEdge<TEdge>? GetEdge(IVertex<TVertex> orgin, IVertex<TVertex> destination);

    /// <summary>
    /// Returns an array containing the two endpoints of the edge. If the graph is directed, the first
    /// vertex is the origin and the second is the destination.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns></returns>
    IVertex<TVertex>[]? EndVertices(IEdge<TEdge> edge);

    /// <summary>
    /// For edge "edge" incident to vertex "endpoint", it returns the other vertex of the edge. An error
    /// occurs if "edge" is not incident to "endpoint".
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="edge"></param>
    /// <returns></returns>
    IVertex<TVertex>? Opposite(IVertex<TVertex> endpoint, IEdge<TEdge> edge);

    /// <summary>
    /// Returns the number of outgoing edges from the vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    int OutDegree(IVertex<TVertex> vertex);

    /// <summary>
    /// Returns the number of incoming edges from the vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    int InDegree(IVertex<TVertex> vertex);

    /// <summary>
    /// Returns an iteration of all outgoing edges from the vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    IEnumerable<IEdge<TEdge>>? OutgoingEdges(IVertex<TVertex> vertex);

    /// <summary>
    /// Returns an iteration of all incoming edges to the vertex. For an undirected graph, it returns
    /// the same collection as does <b>OutgoingEdges()</b>.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    IEnumerable<IEdge<TEdge>>? IncomingEdges(IVertex<TVertex> vertex);

    /// <summary>
    /// Creates and returns a new IVertex storing the element.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    IVertex<TVertex>? InsertVertex(TVertex element);

    /// <summary>
    /// Creates and returns a new IEdge from vertex "origin" to vertex "destination", storing the element.
    /// An error occurs if there already exists an edge from origin to destination.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    IEdge<TEdge>? InsertEdge(IVertex<TVertex> origin, IVertex<TVertex> destination, TEdge element);

    /// <summary>
    /// Removes the vertex and all incident edges from the graph.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns>true if the vertex is successfully removed, otherwise false.</returns>
    bool RemoveVertex(IVertex<TVertex> vertex);

    /// <summary>
    /// Removes the edge from the graph.
    /// </summary>
    /// <param name="edge"></param>
    /// <returns>true if the edge is successfully removed, otherwise false.</returns>
    bool RemoveEdge(IEdge<TEdge> edge);
}