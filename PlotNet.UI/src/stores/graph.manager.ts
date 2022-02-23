import type { CytoscapeElement } from 'src/models/CytoscapeElement';
import type { Edge, GraphDTO, Node } from 'src/models/GraphDTO';
import { writable } from 'svelte/store';

export type Layout = 'circle' | 'grid' | 'cose' | 'cose-bilkent' | 'concentric'


function createGraphManager() {
    const { subscribe, set, update } = writable<GraphManager>({
        layout: "circle",
        elements: [],
        initialGraphData: null,
        nodeConfigs: new Map(),
        edgeConfigs: new Map(),
    });

    return {
        subscribe,
        set,
        update,
        initialize: (dto: GraphDTO) => update(g => {
            g.initialGraphData = dto;
            g.elements = mapToElements(dto);

            for (let node of dto.nodes) {
                g.nodeConfigs.set(node, {
                    isVisible: true,
                    isInterface: node.data.Parent === null,
                })
            }

            for (let edge of dto.edges) {
                g.edgeConfigs.set(edge, {
                    isVisible: true,
                })
            }

            return g;
        })
    }
}

export const graphManager = createGraphManager();

export type GraphManager = {
    layout: Layout,
    elements: CytoscapeElement[],
    initialGraphData?: GraphDTO,
    nodeConfigs: Map<Node, NodeConfig>,
    edgeConfigs: Map<Edge, EdgeConfig>,
}


export type NodeConfig = {
    isVisible: boolean,
    isInterface: boolean,
}

export type EdgeConfig = {
    isVisible: boolean
}


export function mapToElements(graph: GraphDTO) : CytoscapeElement[] {
    return [
        ...graph.nodes.map(n => ({
            group: "nodes",
            data: {
                id: n.id,
                parent: n.data.Parent,
                lifetime: n.data.Lifetime,
            },
            classes: n.data.Parent === undefined ? n.data.Lifetime.toLowerCase() : "",
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