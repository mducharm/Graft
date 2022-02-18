
using System.Text.Json.Serialization;

namespace PlotNet.Models;


internal class CytoscapeElement
{
    public enum GroupType { Nodes, Edges }

    [JsonConstructor]
    public CytoscapeElement()
    {
        
    }

    public GroupType Group { get; set; }
    public CytoscapeElementData Data { get; set; }
    public Dictionary<string, string>? Scratch { get; set; }

    public CytoscapeElementPosition? Position { get; set; }
    public bool? Selected { get; set; }
    public bool? Selectable { get; set; }
    public bool? Locked { get; set; }
    public bool? Grabbable { get; set; }
    public bool? Pannable { get; set; }
    public IEnumerable<string>? Classes { get; set; }
}

internal class CytoscapeElementData
{
    public string Id { get; set; }
    public string? Parent { get; set; }
    public string? Source { get; set; }
    public string? Target { get; set; }
}

internal class CytoscapeElementPosition
{
    public int X { get; set; }
    public int Y { get; set; }
}