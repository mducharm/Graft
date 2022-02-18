
export interface GraphDTO {
    nodes: Node[],
    edges: Edge[],
}

export interface Node {
    id: string,
    data: {
        lifetime: string,
        parent: string,
    }
}
export interface Edge {
    a: string,
    b: string,
}