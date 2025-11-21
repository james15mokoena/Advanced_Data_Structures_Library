using DataStructuresLib.Models;

/*
DLinkedList<string> names = new();

names.AddLast("Pheello");
names.AddLast("Ricky");
names.AddLast("Mansa");
names.AddLast("James");
names.AddLast("JOhn");

Console.WriteLine($"Is cleared: {names.Clear()}");
Console.WriteLine($"Size: {names.Size()}");*/

/*HashMap<string, int> map = new();

var p1 = map.Put("Pheello", 1);
var p2 = map.Put("James", 2);
var p3 = map.Put("Mokoena", 3);
var p4 = map.Put("Ana", 4);
var p5 = map.Put("Mitch", 5);

Console.WriteLine($"Size: {map.Size()}");
Console.WriteLine($"Is Cleared: {map.Clear()}");
Console.WriteLine($"Size: {map.Size()}");*/

AdjacencyMapGraph<string, int> countryMap = new(true);

var v1 = countryMap.InsertVertex("Qalabotjha");
var v2 = countryMap.InsertVertex("Villiers");
var v3 = countryMap.InsertVertex("Frankfort");
var v4 = countryMap.InsertVertex("Cornelia");
var v5 = countryMap.InsertVertex("Reitz");

var e1 = countryMap.InsertEdge(v1, v2, 10);
var e11 = countryMap.InsertEdge(v2, v1, 10);
var e2 = countryMap.InsertEdge(v1, v3, 30);
var e22 = countryMap.InsertEdge(v3, v1, 30);
var e3 = countryMap.InsertEdge(v2, v3, 25);
//var e33 = countryMap.InsertEdge(v3, v2, 30);
var e4 = countryMap.InsertEdge(v3, v4, 40);
var e44 = countryMap.InsertEdge(v4, v3, 40);
var e5 = countryMap.InsertEdge(v5, v4, 25);
var e55 = countryMap.InsertEdge(v4, v5, 25);
//var e6 = countryMap.InsertEdge(v5, v1, 25);

// will store the result of DFS on the graph.
//HashMap<IVertex<string, int>, IEdge<int, string>> forest = new();
// stores discovered vertices
//DLinkedList<IVertex<string, int>> knownVertices = new();

// perform DFS
//AdjacencyMapGraph<string, int>.DFS(countryMap, knownVertices,v1, forest);

//Console.WriteLine($"Forest length: {forest.Size()}");
Console.WriteLine($"Is graph connected: {countryMap.IsConnected()}");

// construct a path.
/*IPositionalList<IEdge<int, string>> path = AdjacencyMapGraph<string, int>.ConstructPath(countryMap, v2, v5);

Console.WriteLine($"Path length: {path.Size()}");

int count = path.Size();

while (!path.IsEmpty())
{
    var pos = path.First();
    var edge = pos!.GetElement();

    if (path.Size() >= 1 && countryMap.IsGraphDirected())
        Console.Write(edge!.GetElement());
    else if(path.Size() >= 1)
        Console.Write(edge!.GetElement());

    if (path.Size() > 1 && countryMap.IsGraphDirected())
        Console.Write(" -> ");
    else if (path.Size() > 1)
        Console.Write(" <-> ");

    path.Remove(pos);
}*/

Console.WriteLine();



/*
Console.WriteLine($"In-degree: {countryMap.InDegree(v1)}");
Console.WriteLine($"Out-degree: {countryMap.OutDegree(v1)}");
Console.WriteLine($"In-degree: {countryMap.InDegree(v5)}");
Console.WriteLine($"Out-degree: {countryMap.OutDegree(v5)}");
Console.WriteLine($"Opposite: {countryMap.Opposite(v1, e2!)?.GetElement()}");
Console.WriteLine($"Opposite: {countryMap.Opposite(v3,e2!)?.GetElement()}");*/

//Console.WriteLine($"Is Graph cleared: {countryMap.Clear()}");


/*Console.WriteLine($"Num Vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Edge: {countryMap.RemoveEdge(e1!)}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Edge: {countryMap.RemoveEdge(e2!)}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Edge: {countryMap.RemoveEdge(e3!)}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Edge: {countryMap.RemoveEdge(e3!)}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Vertex: {countryMap.RemoveVertex(v1)}");
Console.WriteLine($"Num vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Vertex: {countryMap.RemoveVertex(v2)}");
Console.WriteLine($"Num vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");
Console.WriteLine($"Remove Vertex: {countryMap.RemoveVertex(v3)}");
Console.WriteLine($"Num vertices: {countryMap.NumVertices()}");
Console.WriteLine($"Num Edges: {countryMap.NumEdges()}");*/