using DataStructuresLib.Exceptions;
using DataStructuresLib.Models;

AdjacencyMapGraph<string, int> countryMap = new(true);

var v1 = countryMap.InsertVertex("Qalabotjha");
var v2 = countryMap.InsertVertex("Villiers");
var v3 = countryMap.InsertVertex("Frankfort");
try
{
    var e1 = countryMap.InsertEdge(v1, v2, 10);
    var e2 = countryMap.InsertEdge(v1, v3, 30);
    var e3 = countryMap.InsertEdge(v2, v3, 25);
    var e4 = countryMap.InsertEdge(v1, v2, 0);
}
catch(EdgeExistsException e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine(v1.GetElement());
Console.WriteLine(v2.GetElement());
Console.WriteLine(v3.GetElement());
Console.WriteLine($"Num Vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");