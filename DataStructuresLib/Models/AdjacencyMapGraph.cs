
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

    public IEnumerable<IEdge<TEdge,TVertex>>? Edges()
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

            if (orig.GetOutgoingEdges().Size() < dest.GetIncomingEdges().Size())
                edge = (IEdge<TEdge,TVertex>?)orig.GetOutgoingEdges().GetValue(dest);
            else if (orig.GetOutgoingEdges().Size() > dest.GetIncomingEdges().Size())
                edge = (IEdge<TEdge,TVertex>?)dest.GetIncomingEdges().GetValue(orig);
        }

        return edge;
    }

    public IEnumerable<IEdge<TEdge,TVertex>>? IncomingEdges(IVertex<TVertex,TEdge> vertex)
    {
        if(vertex is Vertex<TVertex,TEdge> realVertex && !realVertex.GetIncomingEdges().IsEmpty())
        {
            foreach (var edge in realVertex.GetIncomingEdges().Values())
                yield return (IEdge<TEdge,TVertex>) edge;
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
            if (GetEdge(orig, dest) is null)
            {
                Edge<TEdge,TVertex> newEdge = new(orig, dest, element);
                newEdge.SetPosition(_edges.AddLast(newEdge));
                orig.GetOutgoingEdges().Put(dest, newEdge);
                dest.GetIncomingEdges().Put(orig, newEdge);
                return newEdge;
            }
            else
                throw new EdgeExistsException("An edge already exists between these vertices.");

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
                yield return (IEdge<TEdge,TVertex>) edge;
        }
    }

    public bool RemoveEdge(IEdge<TEdge,TVertex> edge)
    {
        throw new NotImplementedException();
    }

    public bool RemoveVertex(IVertex<TVertex,TEdge> vertex)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IVertex<TVertex,TEdge>>? Vertices()
    {
        if (!_vertices.IsEmpty())
        {
            foreach (var vertex in _vertices)
                yield return vertex;
        }
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
        /// Stores a reference to the position in the vertex list where this vertex is located.
        /// </summary>
        private IPosition<Vertex<V, E>>? _position;

        /// <summary>
        /// Stores the outgoing edges from the vertex.
        /// </summary>
        private readonly IMap<IVertex<V, E>, IEdge<E,V>> _outgoingEdges;
        /// <summary>
        /// Stores the incoming edges to the vertex.
        /// </summary>
        private readonly IMap<IVertex<V, E>, IEdge<E,V>> _incomingEdges;

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
        public IPosition<IVertex<V, E>>? GetPosition() => (IPosition<IVertex<V, E>>?)_position;

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

        public IMap<IVertex<V, E>, IEdge<E,V>> GetIncomingEdges() => _incomingEdges;

        /// <summary>
        /// Adds an incoming edge to a vertex's incoming edges map.
        /// </summary>
        /// <param name="incomingEdge"></param>
        public void InsertIncomingEdge(IEdge<E,V> incomingEdge) => _incomingEdges.Put(this, (Edge<E,V>)incomingEdge!);

        public IMap<IVertex<V, E>, IEdge<E,V>> GetOutgoingEdges()
        {
            IMap<IVertex<V, E>, object> map = (IMap<IVertex<V, E>, object>)new HashMap<IVertex<V, E>, IEdge<TEdge,TVertex>>();
            return default;
        }

        /// <summary>
        /// Adds an outgoing edge to a vertex's outgoing edges map.
        /// </summary>
        /// <param name="outgoingEdge"></param>
        public void InsertOutgoingEdge(IEdge<E,V> outgoingEdge) => _outgoingEdges.Put(this, outgoingEdge!);
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
        /// The location of the edge in the edge list of the graph.
        /// </summary>
        private IPosition<Edge<E,V>>? _position;

        /// <summary>
        /// The vertices that are connected by this edge.
        /// </summary>
        private readonly Vertex<TVertex,E>[]? _endpoints;

        public Edge(IVertex<TVertex,E> origin, IVertex<TVertex,E> destination, E element)
        {
            if (origin is Vertex<TVertex,E> orig && destination is Vertex<TVertex,E> dest)
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
        public IPosition<IEdge<E,V>>? GetPosition() => (IPosition<IEdge<E,V>>?)_position;

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

        public IVertex<V,E>[]? GetEndpoints() => (IVertex<V,E>[]?) _endpoints;
    }
}