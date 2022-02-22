import { Writable, writable } from 'svelte/store';

export type Layout = 'circle' | 'grid' | 'cose' | 'cose-bilkent' | 'concentric'


export const graphManager: Writable<GraphManager> = writable({
    layout: "circle"
});

export type GraphManager = {
    layout: Layout
}
