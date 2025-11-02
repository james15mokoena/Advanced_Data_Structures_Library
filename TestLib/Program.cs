using DataStructuresLib.Models;

HashMap<string, string> map = new();

string? m1 = map.Put("Pheello", "Mokoena");
string? m2 = map.Put("Esther", "Mazibuko");
string? m3 = map.Put("Teboho", "Michael");
string? m4 = map.Put("Julia", "Modiehi");
string? m5 = map.Put("Joseph", "Rantau");
string? m6 = map.Put("Tumelo", "Meshack");
string? m7 = map.Put("Karabo", "Babo");
string? m8 = map.Put("Rethabile", "Mokoena");
string? m9 = map.Put("Boitumelo", "Mokoena");
string? m10 = map.Put("Pheello", "James");
string? m11 = map.Put("Jabu", "Gamede");

foreach (var value in map.Values())
    Console.WriteLine(value);

Console.WriteLine();

foreach (var key in map.KeySet())
    Console.WriteLine(key);

Console.WriteLine();

foreach (var e in map.EntrySet())
    Console.WriteLine(e);

Console.WriteLine($"Inserted: {m1}");
Console.WriteLine($"Size: {map.Size()}");
Console.WriteLine($"Value: {map.GetValue("Pheello")}");
Console.WriteLine($"Cap: {map.GetCapacity()}");
Console.WriteLine($"Removed: {map.Remove("Pheello")}");
Console.WriteLine($"Removed: {map.Remove("Julia")}");
Console.WriteLine($"Removed: {map.Remove("Joseph")}");
Console.WriteLine($"Removed: {map.Remove("Tumelo")}");
Console.WriteLine($"Removed: {map.Remove("Esther")}");
Console.WriteLine($"Removed: {map.Remove("Karabo")}");
Console.WriteLine($"Removed: {map.Remove("Rethabile")}");
Console.WriteLine($"Removed: {map.Remove("Boitumelo")}");
Console.WriteLine($"Removed: {map.Remove("Jabu")}");
Console.WriteLine($"Removed: {map.Remove("Teboho")}");
Console.WriteLine($"Removed: {map.Remove("Rethabile")}");
Console.WriteLine($"Size: {map.Size()}");