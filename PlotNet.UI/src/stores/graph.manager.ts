import type { CytoscapeElement } from 'src/models/CytoscapeElement';
import { Writable, writable } from 'svelte/store';

export type Layout = 'circle' | 'grid' | 'cose' | 'cose-bilkent' | 'concentric'


export const graphManager: Writable<GraphManager> = writable({
    layout: "circle",
    elements: [],
});

export type GraphManager = {
    layout: Layout,
    elements: CytoscapeElement[]
}
