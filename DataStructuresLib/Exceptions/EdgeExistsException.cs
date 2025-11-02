namespace DataStructuresLib.Exceptions;

/// <summary>
/// An exception that gets thrown when an attempt is made to connect two vertices
/// that are connected.
/// </summary>
public class EdgeExistsException : Exception
{
    public EdgeExistsException() : base() { }
    public EdgeExistsException(string message) : base(message) { }
    public EdgeExistsException(string message, Exception inner) : base(message,inner){}
}