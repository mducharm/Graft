export type CytoscapeElement = {
    group: string,
    isVisible?: boolean,
    data: {
        id: string
        parent?: string
        source?: string
        target?: string
    },
    scratch: {
        [p: string]: string | boolean,
    },
}
