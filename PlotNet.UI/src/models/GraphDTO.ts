
export interface GraphDTO {
    nodes: Node[],
    edges: Edge[],
}

export interface Node {
    id: string,
    data: {
        Lifetime: string,
        Parent: string,
    }
}
export interface Edge {
    a: string,
    b: string,
}