
using System.Collections;
using DataStructuresLib.Exceptions;

namespace DataStructuresLib.Models;

/// <summary>
/// An implementation of the Graph ADT with the <b>Adjacency Map Structure</b>. <br /><br/>The parameter
/// <b>isGraphDirected</b> indicates whether the graph is directed or undirected.
/// </summary>
/// <typeparam name="TVertex"></typeparam>
/// <typeparam name="TEdge"></typeparam>
/// <param name="isGraphDirected">Indicates whether the graph is directed or undirected.</param>
public class AdjacencyMapGraph<TVertex, TEdge>(bool isGraphDirected) : IGraph<TVertex, TEdge>
{
    /// <summary>
    /// Contains all the vertices of the graph.
    /// </summary>
    private readonly DLinkedList<Vertex<TVertex,TEdge>> _vertices = new();

    /// <summary>
    /// Contains all the edges of the graph.
    /// </summary>
    private readonly DLinkedList<Edge<TEdge,TVertex>> _edges = new();

    /// <summary>
    /// Indicated whether this is a directed or undirected graph.
    /// </summary>
    private readonly bool _isGraphDirected = isGraphDirected;

    /// <summary>
    /// It implements a <b>Depth-First Search</b> traversal of a graph.
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="startVertex"></param>
    /// <param name="discoveryEdges">It associates each vertex with the edge that discovers it.</param>
    public static void DFS(IGraph<TVertex, TEdge> graph, IVertex<TVertex, TEdge> startVertex, HashMap<IVertex<TVertex, TEdge>, IEdge<TEdge, TVertex>> discoveryEdges)
    {
        if (graph is AdjacencyMapGraph<TVertex, TEdge> grp && startVertex is Vertex<TVertex, TEdge> vertex)
        {
            if (!vertex.IsVisited())
            {
                // mark the vertex as "visited".
                vertex.SetVisited(true);

                foreach (var edge in vertex.GetOutgoingEdges().Values())
                {
                    if (!edge.IsVisited())
                    {
                        // get the adjacent vertex
                        IVertex<TVertex, TEdge> adjVertex = grp.Opposite(startVertex, edge)!;

                        if (!adjVertex.IsVisited())
                        {
                            // record the discovery edge with the discovered vertex.
                            discoveryEdges.Put(adjVertex, edge);

                            // mark the edge as visited
                            ((Edge<TEdge, TVertex>)edge).SetVisited(true);

                            // visit the discovered vertex.
                            DFS(grp, adjVertex, discoveryEdges);
                        }
                        // mark the edge as visited
                        else
                            ((Edge<TEdge, TVertex>)edge).SetVisited(true);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// It constructs a path from the origin vertex to the destination vertex and returns the path, if it exists.
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="origin"></param>
    /// <param name="dest"></param>
    /// <param name="forest"></param>
    /// <returns></returns>
    public static IPositionalList<IEdge<TEdge,TVertex>> ConstructPath(IGraph<TVertex, TEdge> graph, IVertex<TVertex, TEdge> origin,
                    IVertex<TVertex,TEdge> dest)
    {
        // will store the result of DFS on the graph.
        HashMap<IVertex<TVertex, TEdge>, IEdge<TEdge, TVertex>> forest = new();

        // perform DFS on the graph, to get the forest.
        DFS(graph, origin, forest);
        
        // will store the edges from the origin vertex to the destination vertex.
        DLinkedList<IEdge<TEdge, TVertex>> path = new();

        // check if dest was discovered during DFS.
        if(forest.GetValue(dest) != null)
        {
            // will be used to construct the path from back to front.
            IVertex<TVertex, TEdge> currVertex = dest;

            while(currVertex != origin)
            {
                // get the discovery edge
                var edge = forest.GetValue(currVertex)!;

                // add the edge to the front so that the path is ordered from origin to dest
                path.AddFirst(edge);
                
                // get the opposite vertex
                currVertex = graph.Opposite(currVertex, edge)!;
            }

        }

        return path;
    }

    /// <summary>
    /// Returns a value indicating whether the graph is directed or undirected.
    /// </summary>
    /// <returns>true if the graph is directed, otherwise false.</returns>
    public bool IsGraphDirected() => _isGraphDirected == true;

    public IEnumerable<IEdge<TEdge, TVertex>>? Edges()
    {
        if (!_edges.IsEmpty())
        {
            foreach (var edge in _edges)
                yield return edge;
        }
    }

    public IVertex<TVertex,TEdge>[]? EndVertices(IEdge<TEdge,TVertex> edge)
    {
        if (edge is Edge<TEdge,TVertex> realEdge)
            return realEdge.GetEndpoints();
        return null;
    }

    public IEdge<TEdge,TVertex>? GetEdge(IVertex<TVertex,TEdge> origin, IVertex<TVertex,TEdge> destination)
    {
        IEdge<TEdge,TVertex>? edge = null;

        if (origin is Vertex<TVertex,TEdge> orig && destination is Vertex<TVertex,TEdge> dest)
        {
            if (orig.GetOutgoingEdges().Size() <= dest.GetIncomingEdges().Size())
                edge = orig.GetOutgoingEdges().GetValue(dest);
            else if (dest.GetIncomingEdges().Size() < orig.GetOutgoingEdges().Size())
                edge = dest.GetIncomingEdges().GetValue(orig);
        }

        return edge;
    }

    public IEnumerable<IEdge<TEdge,TVertex>>? IncomingEdges(IVertex<TVertex,TEdge> vertex)
    {
        if(vertex is Vertex<TVertex,TEdge> realVertex && !realVertex.GetIncomingEdges().IsEmpty())
        {
            foreach (var edge in realVertex.GetIncomingEdges().Values())
                yield return edge;
        }
    }

    public int InDegree(IVertex<TVertex,TEdge> vertex)
    {
        if (vertex is Vertex<TVertex,TEdge> vert)
            return vert.GetIncomingEdges().Size();
        return 0;
    }


    public IEdge<TEdge,TVertex>? InsertEdge(IVertex<TVertex,TEdge> origin, IVertex<TVertex,TEdge> destination, TEdge element)
    {
        if (origin is Vertex<TVertex,TEdge> orig && destination is Vertex<TVertex,TEdge> dest)
        {
            // No parallel edges or self-loops are allowed.            
            if (GetEdge(orig, dest) is null && GetEdge(dest, orig) is null && orig != dest)
            {                
                Edge<TEdge, TVertex> newEdge = new(orig, dest, element);
                newEdge.SetPosition(_edges.AddLast(newEdge));
                orig.GetOutgoingEdges().Put(dest, newEdge);
                dest.GetIncomingEdges().Put(orig, newEdge);
                return newEdge;
            }
            else if(orig != dest)
                throw new EdgeExistsException("An edge already exists between these vertices. Simple graphs (directed or undirected) do not allow parallel edges.");
            else if (orig == dest)
                throw new EdgeExistsException("Simple graphs (directed or undirected) do not allow self-loops.");
        }
        return null;
    }

    public IVertex<TVertex,TEdge> InsertVertex(TVertex element)
    {
        Vertex<TVertex,TEdge> newVertex = new(element, _isGraphDirected);
        newVertex.SetPosition(_vertices.AddLast(newVertex));
        return newVertex;
    } 

    public int NumEdges() => _edges.Size();

    public int NumVertices() => _vertices.Size();

    public IVertex<TVertex,TEdge>? Opposite(IVertex<TVertex,TEdge> endpoint, IEdge<TEdge,TVertex> edge)
    {
        if (endpoint is Vertex<TVertex,TEdge> realEndpoint && edge is Edge<TEdge,TVertex> realEdge)
        {
            // is realEdge incident to/from realEndpoint?
            if (realEndpoint == realEdge.GetEndpoints()![0])
                return (IVertex<TVertex,TEdge>?)realEdge.GetEndpoints()![1];
            else if (realEndpoint == realEdge.GetEndpoints()![1])
                return (IVertex<TVertex,TEdge>?)realEdge.GetEndpoints()![0];
            else
                throw new EdgeNotIncidentException("Edge is not incident to the endpoint.");
        }
        
        return null;
    }

    public int OutDegree(IVertex<TVertex,TEdge> vertex)
    {
        if (vertex is Vertex<TVertex,TEdge> vert)
            return vert.GetOutgoingEdges().Size();
        return 0;
    }

    public IEnumerable<IEdge<TEdge,TVertex>>? OutgoingEdges(IVertex<TVertex,TEdge> vertex)
    {
        if(vertex is Vertex<TVertex,TEdge> realVertex && !realVertex.GetOutgoingEdges().IsEmpty())
        {
            foreach (var edge in realVertex.GetOutgoingEdges().Values())
                yield return edge;
        }
    }

    public bool RemoveEdge(IEdge<TEdge,TVertex> edge)
    {
        if (!_edges.IsEmpty() && edge is Edge<TEdge, TVertex> realEdge)
        {
            if(realEdge.GetEndpoints()![0] != null && realEdge.GetEndpoints()![1] != null)
            {
                if(_edges.Remove(realEdge.GetPosition()) is not null)
                    return realEdge.RemoveEndpoints();
            }
        }

        return false;
    }

    public bool RemoveVertex(IVertex<TVertex,TEdge> vertex)
    {
        if(!_vertices.IsEmpty() && vertex is Vertex<TVertex,TEdge> realVertex)
        {
            // remove all outgoing edges.
            if (!realVertex.GetOutgoingEdges().IsEmpty())
            {
                // will temporarily store outgoing edges to be removed.
                List<Edge<TEdge, TVertex>> edges = [];

                foreach (var edge in realVertex.GetOutgoingEdges().Values())
                    edges.Add((Edge<TEdge, TVertex>)edge);

                // remove all outgoing edges that are incident from this vertex.
                for (int e = 0; e < edges.Count; ++e)
                    RemoveEdge(edges.ElementAt(e));
            }

            // return early if the graph is undirected.
            if (!IsGraphDirected())
            {
                // remove the vertex
                _vertices.Remove(realVertex.GetPosition());
                return realVertex.GetOutgoingEdges().IsEmpty() && realVertex.GetIncomingEdges().IsEmpty();
            }

            // if the graph is directed, then remove all edges incident to this vertex.
            if (IsGraphDirected() && !realVertex.GetIncomingEdges().IsEmpty())
            {
                // will temporarily store incoming edges to be removed.
                List<Edge<TEdge, TVertex>> edges = [];

                foreach (var edge in realVertex.GetIncomingEdges().Values())
                    edges.Add((Edge<TEdge, TVertex>)edge);

                // remove all incoming edges that are incident to this vertex.
                for (int e = 0; e < edges.Count; ++e)
                    RemoveEdge(edges.ElementAt(e));
            }
            
            // remove the vertex
            _vertices.Remove(realVertex.GetPosition());
            return realVertex.GetOutgoingEdges().IsEmpty() && realVertex.GetIncomingEdges().IsEmpty();
            
        }
        return false;
    }

    public IEnumerable<IVertex<TVertex, TEdge>>? Vertices()
    {
        if (!_vertices.IsEmpty())
        {
            foreach (var vertex in _vertices)
                yield return vertex;
        }
    }
    
    public bool Clear()
    {        
        // will temporarily store the vertices to be removed.
        List<Vertex<TVertex, TEdge>> vertices = [];

        foreach (var vertex in _vertices)
            vertices.Add(vertex);

        for (int v = 0; v < vertices.Count; ++v)
            RemoveVertex(vertices[v]);
        
        return _vertices.IsEmpty() && _edges.IsEmpty();
    }

    /// <summary>
    /// The implementation of the IVertex interface.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <typeparam name="E"></typeparam>
    public class Vertex<V, E> : IVertex<V, E>
    {
        /// <summary>
        /// The element stored in the vertex.
        /// </summary>
        private V? _element;

        /// <summary>
        /// Indicates whether this vertex has been visited in during graph traversal
        /// either by DFS or BFS.
        /// </summary>
        private bool _isVisited;

        public bool IsVisited() => _isVisited;

        /// <summary>
        /// Updates the isVisited stated of the vertex.
        /// </summary>
        /// <param name="visited"></param>
        public void SetVisited(bool visited) => _isVisited = visited;

        /// <summary>
        /// Stores a reference to the position in the vertex list where this vertex is located.
        /// </summary>
        private IPosition<Vertex<V, E>>? _position;

        /// <summary>
        /// Stores the outgoing edges from the vertex.
        /// </summary>
        private HashMap<IVertex<V, E>, IEdge<E,V>>? _outgoingEdges;
        /// <summary>
        /// Stores the incoming edges to the vertex.
        /// </summary>
        private HashMap<IVertex<V, E>, IEdge<E,V>>? _incomingEdges;

        public Vertex(V element, bool isGraphDirected)
        {
            _element = element;

            _outgoingEdges = new HashMap<IVertex<V, E>, IEdge<E,V>>();

            _incomingEdges = isGraphDirected ? new HashMap<IVertex<V, E>, IEdge<E,V>>() : _outgoingEdges;
        }

        /// <summary>
        /// Sets the element to be stored in the vertex.
        /// </summary>
        /// <param name="element"></param>
        public void SetElement(V element) => _element = element;

        public V? GetElement() => _element;

        /// <summary>
        /// Returns the position where this vertex is located in the vertex list of the graph.
        /// </summary>
        /// <returns></returns>
        public IPosition<Vertex<V, E>>? GetPosition() => _position;

        /// <summary>
        /// Sets the position in the vertex list of the graph where this vertex is located.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(IPosition<Vertex<V, E>> position)
        {
            if (position is IPosition<Vertex<V, E>> pos)
                _position = pos;
            else if (position == null)
                _position = null;
            else
                throw new InvalidCastException("Cannot cast IPosition<IVertex<V>> to IPosition<Vertex<V>>, check your types.");
        }

        public IMap<IVertex<V, E>, IEdge<E,V>> GetIncomingEdges() => _incomingEdges!;

        /// <summary>
        /// Adds an incoming edge to a vertex's incoming edges map.
        /// </summary>
        /// <param name="incomingEdge"></param>
        public void InsertIncomingEdge(IEdge<E,V> incomingEdge) => _incomingEdges!.Put(this, (Edge<E,V>)incomingEdge!);

        public IMap<IVertex<V, E>, IEdge<E, V>> GetOutgoingEdges() => _outgoingEdges!;

        /// <summary>
        /// Adds an outgoing edge to a vertex's outgoing edges map.
        /// </summary>
        /// <param name="outgoingEdge"></param>
        public void InsertOutgoingEdge(IEdge<E, V> outgoingEdge) => _outgoingEdges!.Put(this, outgoingEdge!);
        
        /// <summary>
        /// Removes all edges incident to this vertex, when the vertex is being removed from the graph.
        /// </summary>
        public void RemoveIncidentEdges()
        {
            if (_outgoingEdges != null)
                _outgoingEdges = null;

            if (_incomingEdges != null)
                _incomingEdges = null;
        }
    }

    /// <summary>
    /// An implementation of the IEdge interface.
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Edge<E,V> : IEdge<E,V>
    {
        /// <summary>
        /// The element stored in the edge.
        /// </summary>
        private E? _element;

        /// <summary>
        /// Indicates whether this edge has been visited in during graph traversal
        /// either by DFS or BFS.
        /// </summary>
        private bool _isVisited;

        public bool IsVisited() => _isVisited;

        /// <summary>
        /// Updates the isVisited stated of the edge.
        /// </summary>
        /// <param name="visited"></param>
        public void SetVisited(bool visited) => _isVisited = visited;

        /// <summary>
        /// The location of the edge in the edge list of the graph.
        /// </summary>
        private IPosition<Edge<E, V>>? _position;

        /// <summary>
        /// The vertices that are connected by this edge.
        /// </summary>
        private Vertex<V,E>[]? _endpoints;

        public Edge(IVertex<V,E> origin, IVertex<V,E> destination, E element)
        {
            if (origin is Vertex<V,E> orig && destination is Vertex<V,E> dest)
            {
                _endpoints = [orig, dest];                
                _element = element;
            }
            else
                throw new ArgumentException("Please check the argument types for correct types.");
        }

        /// <summary>
        /// Returns the position where this edge is located in the edge list of the graph.
        /// </summary>
        /// <returns></returns>
        public IPosition<Edge<E,V>>? GetPosition() => _position;

        /// <summary>
        /// Sets the position in the edge list of the graph where this edge is located.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(IPosition<Edge<E,V>> position)
        {
            if (position is IPosition<Edge<E,V>> pos)
                _position = pos;
            else if (position == null)
                _position = null;
            else
                throw new InvalidCastException("Cannot cast IPosition<IEdge<E>> to IPosition<Edge<E>>, check your types.");
        }

        /// <summary>
        /// Sets the element to be stored in the edge.
        /// </summary>
        /// <param name="element"></param>
        public void SetElement(E element) => _element = element;

        public E? GetElement() => _element;

        public IVertex<V, E>[]? GetEndpoints() => _endpoints;
        
        /// <summary>
        /// It is responsible for removing the endpoints when the edge is being removed from
        /// the graph.
        /// </summary>
        public bool RemoveEndpoints()
        {
            Vertex<V, E> origin = _endpoints![0];
            Vertex<V, E> dest = _endpoints![1];
            origin.GetOutgoingEdges().Remove(dest);
            dest.GetIncomingEdges().Remove(origin);
            _endpoints = null;
            return _endpoints == null &&
                (origin.GetOutgoingEdges().GetValue(dest) is null || origin.GetOutgoingEdges().GetValue(dest)!.Equals(default(Vertex<V, E>))) &&
                (dest.GetIncomingEdges().GetValue(origin) is null || dest.GetIncomingEdges().GetValue(origin)!.Equals(default(Vertex<V, E>)));
        }
    }
}