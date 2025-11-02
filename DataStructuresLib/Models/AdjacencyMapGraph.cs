
namespace DataStructuresLib.Models;

/// <summary>
/// An implementation of the Graph ADT with the <b>Adjacency Map Structure</b>.
/// </summary>
/// <typeparam name="TVertex"></typeparam>
/// <typeparam name="TEdge"></typeparam>
public class AdjacencyMapGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
{
    public IEnumerable<IEdge<TEdge>>? Edges()
    {
        throw new NotImplementedException();
    }

    public IVertex<TVertex>[]? EndVertices(IEdge<TEdge> edge)
    {
        throw new NotImplementedException();
    }

    public IEdge<TEdge>? GetEdge(IVertex<TVertex> orgin, IVertex<TVertex> destination)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IEdge<TEdge>>? IncomingEdges(IVertex<TVertex> vertex)
    {
        throw new NotImplementedException();
    }

    public int InDegree(IVertex<TVertex> vertex)
    {
        throw new NotImplementedException();
    }

    public IEdge<TEdge>? InsertEdge(IVertex<TVertex> origin, IVertex<TVertex> destination, TEdge element)
    {
        throw new NotImplementedException();
    }

    public IVertex<TVertex>? InsertVertex(TVertex element)
    {
        throw new NotImplementedException();
    }

    public int NumEdges()
    {
        throw new NotImplementedException();
    }

    public int NumVertives()
    {
        throw new NotImplementedException();
    }

    public IVertex<TVertex>? Opposite(IVertex<TVertex> endpoint, IEdge<TEdge> edge)
    {
        throw new NotImplementedException();
    }

    public int OutDegree(IVertex<TVertex> vertex)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IEdge<TEdge>>? OutgoingEdges(IVertex<TVertex> vertex)
    {
        throw new NotImplementedException();
    }

    public bool RemoveEdge(IEdge<TEdge> edge)
    {
        throw new NotImplementedException();
    }

    public bool RemoveVertex(IVertex<TVertex> vertex)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IVertex<TVertex>>? Vertices()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// The implementation of the IVertex interface.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public class Vertex<V> : IVertex<V>
    {
        /// <summary>
        /// The element stored in the vertex.
        /// </summary>
        private V? _element;

        /// <summary>
        /// Stores a reference to the position in the vertex list where this vertex is located.
        /// </summary>
        private IPosition<Vertex<V>>? _position;

        /// <summary>
        /// Stores the outgoing edges from the vertex.
        /// </summary>
        private readonly HashMap<Vertex<V>, object>? _outgoingEdges;
        /// <summary>
        /// Stores the incoming edges to the vertex.
        /// </summary>
        private readonly HashMap<Vertex<V>, object>? _incomingEdges;

        public Vertex(V element, bool isGraphDirected)
        {
            _element = element;

            _outgoingEdges = new HashMap<Vertex<V>, object>();

            _incomingEdges = isGraphDirected ? new HashMap<Vertex<V>, object>() : _outgoingEdges;
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
        public IPosition<IVertex<V>>? GetPosition() => (IPosition<IVertex<V>>?) _position;

        /// <summary>
        /// Sets the position in the vertex list of the graph where this vertex is located.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(IPosition<IVertex<V>> position)
        {
            if (position is IPosition<Vertex<V>> pos)
                _position = pos;
            else if (position == null)
                _position = null;
            else
                throw new InvalidCastException("Cannot cast IPosition<IVertex<V>> to IPosition<Vertex<V>>, check your types.");
        }

        public IMap<IVertex<V>, object>? GetIncomingEdges() => (IMap<IVertex<V>, object>?)_incomingEdges;

        /// <summary>
        /// Adds an incoming edge to a vertex's incoming edges map.
        /// </summary>
        /// <param name="incomingEdge"></param>
        public void InsertIncomingEdge(TEdge incomingEdge) => _incomingEdges!.Put(this, incomingEdge!);

        public IMap<IVertex<V>, object>? GetOutgoingEdges() => (IMap<IVertex<V>, object>?)_outgoingEdges;

        /// <summary>
        /// Adds an outgoing edge to a vertex's outgoing edges map.
        /// </summary>
        /// <param name="outgoingEdge"></param>
        public void InsertOutgoingEdge(TEdge outgoingEdge) => _outgoingEdges!.Put(this, outgoingEdge!);
    }

    /// <summary>
    /// An implementation of the IEdge interface.
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class Edge<E> : IEdge<E>
    {
        /// <summary>
        /// The element stored in the edge.
        /// </summary>
        private E? _element;

        /// <summary>
        /// The location of the edge in the edge list of the graph.
        /// </summary>
        private IPosition<Edge<E>>? _position;

        /// <summary>
        /// The vertices that are connected by this edge.
        /// </summary>
        private readonly Vertex<TVertex>[]? _endpoints;

        public Edge(IVertex<TVertex> origin, IVertex<TVertex> destination, E element)
        {
            if (origin is Vertex<TVertex> orig && destination is Vertex<TVertex> dest)
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
        public IPosition<IEdge<E>>? GetPosition() => (IPosition<IEdge<E>>?)_position;

        /// <summary>
        /// Sets the position in the edge list of the graph where this edge is located.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(IPosition<IEdge<E>> position)
        {
            if (position is IPosition<Edge<E>> pos)
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

        public IVertex<object>[]? GetEndpoints() => (IVertex<object>[]?) _endpoints;
    }
}