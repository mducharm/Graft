export type CytoscapeElement = {
    group: string,
    data: {
        id: string
        parent?: string
        source?: string
        target?: string
    },
    scratch: {
        [p: string]: string
    },
}
