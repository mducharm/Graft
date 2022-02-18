
namespace PlotNet.Models;

public record GraphDTO(
    IEnumerable<Node> Nodes, 
    IEnumerable<Edge> Edgess
);

public record Node(string Id, Dictionary<string, string> Data);

public record Edge(string A, string B);