namespace DataStructuresLib.Exceptions;

/// <summary>
/// An exception that gets thrown when attempting to get the opposite endpoint of an edge,
/// while the edge is not incident to one of the endpoints.
/// </summary>
public class EdgeNotIncidentException : Exception
{
    public EdgeNotIncidentException() : base() { }
    public EdgeNotIncidentException(string message) : base(message) { }
    public EdgeNotIncidentException(string message, Exception inner) : base(message,inner){}
}