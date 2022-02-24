import type { CytoscapeElement } from 'src/models/CytoscapeElement';
import type { Edge, GraphDTO, Node } from 'src/models/GraphDTO';
import { writable } from 'svelte/store';

export type Layout = 'circle' | 'grid' | 'cose' | 'cose-bilkent' | 'concentric'


function createGraphManager() {
    const { subscribe, set, update } = writable<GraphManager>({
        graph: null,
        layout: "circle",
        elements: [],
        elementsToFilter: [],
        initialGraphData: null,
    });

    return {
        subscribe,
        set,
        update,
        initialize: (dto: GraphDTO) => update(g => {
            g.initialGraphData = dto;
            g.elements = mapToElements(dto);
            return g;
        }),
        layout(l: Layout) {
            update(g => {
                if (g.graph) {
                    const layout = g.graph.layout({
                        name: l,
                      });
              
                    layout.run();
                }
                return g;
            })
        },
        toggleVisibility(el: CytoscapeElement) {
            update(g => {

                let node = g.graph.$(`#` + el.data.id);
                let edges = node.connectedEdges();
                let childEdges = node.children().connectedEdges();
                
                if (el.isVisible) {
                    node.removeClass("hidden");
                    edges.removeClass("hidden");
                    childEdges.removeClass("hidden");
                } else {
                    node.addClass("hidden");
                    edges.addClass("hidden");
                    childEdges.addClass("hidden");
                }

                el.isVisible = !el.isVisible;

                return g;
            })

        },
        toggleSelectAll(allSelected: boolean) {
            update(g => {
                if (allSelected) {

                    g.graph.$("node").removeClass("hidden");
                    g.graph.$("edge").removeClass("hidden");
                    g.elements = g.elements.map(e => {
                        e.isVisible = true;
                        return e;
                    })

                    return g;
                }

                g.graph.$("node").addClass("hidden");
                g.graph.$("edge").addClass("hidden");

                g.elements = g.elements.map(e => {
                    e.isVisible = false;
                    return e;
                })

                return g;
            })
        },

    }
}

export const graphManager = createGraphManager();

export type GraphManager = {
    graph: any,
    layout: Layout,
    elements: CytoscapeElement[],
    initialGraphData?: GraphDTO,
    elementsToFilter: string[],
}

export function mapToElements(graph: GraphDTO): CytoscapeElement[] {
    return [
        ...graph.nodes.map(n => ({
            group: "nodes",
            data: {
                id: n.id,
                parent: n.data.Parent,
                lifetime: n.data.Lifetime,
            },
            classes: n.data.Parent === undefined ? n.data.Lifetime.toLowerCase() : "",
            scratch: {
                isInterface: n.data?.Parent === undefined || n.data?.Parent?.length === 0,
            },
            isVisible: true,
        })),
        ...graph.edges.map(e => ({
            group: "edges",
            data: {
                id: `${e.a} - ${e.b}`,
                source: e.a,
                target: e.b,
            },
            scratch: {
            },
            isVisible: true,
        }))
    ]
}