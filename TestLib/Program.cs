using DataStructuresLib.Models;

AdjacencyMapGraph<string, int> countryMap = new(false);

var v1 = countryMap.InsertVertex("Qalabotjha");
var v2 = countryMap.InsertVertex("Villiers");
var v3 = countryMap.InsertVertex("Frankfort");
var e1 = countryMap.InsertEdge(v1, v2, 10);

Console.WriteLine(v1.GetElement());
Console.WriteLine(v2.GetElement());
Console.WriteLine(v3.GetElement());
Console.WriteLine($"Num Vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");