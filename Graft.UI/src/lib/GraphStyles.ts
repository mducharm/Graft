export const styles = [
    {
        selector: ".transient",
        css: {
            "background-color": "#4EA5D9",
            "background-opacity": 0.5,
        }
    },
    {
        selector: ".scoped",
        css: {
            "background-color": "#ffb11f",
            "background-opacity": 0.5,
        }
    },
    {
        selector: ".singleton",
        css: {
            "background-color": "#f87060",
            "background-opacity": 0.5,
        }
    },
    {
        selector: ".hidden",
        css: {
            "background-color": "blue",
            "opacity": 0,
        }
    },
    {
        selector: "node",
        css: {
            content: "data(id)",
            "text-valign": "center",
            "text-halign": "center",
        },
    },
    {
        selector: ":parent",
        css: {
            "text-valign": "top",
            "text-halign": "center",
        },
    },
    {
        selector: "edge",
        css: {
            "curve-style": "bezier",
            "target-arrow-shape": "triangle",
        },
    },
];
