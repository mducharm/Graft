import type { GraphDTO } from "./GraphDTO";


export type CytoscapeElement = {
    group: string,
    data: {
        id: string
        source?: string
        target?: string
    },
    scratch: {
        [p: string]: string
    },
}

export function mapToElement(graph: GraphDTO) : CytoscapeElement[] {
    return [
        ...graph.nodes.map(n => ({
            group: "nodes",
            data: {
                id: n.id
            },
            scratch:{}
        })),
        ...graph.edges.map(e => ({
            group: "edges",
            data: {
                id: `${e.a} - ${e.b}`,
                source: e.a,
                target: e.b,
            },
            scratch:{}
        }))
    ]
}