export const styles = [
    {
        selector: ".transient",
        css: {
            "background-color": "#4EA5D9"
        }
    },
    {
        selector: ".scoped",
        css: {
            "background-color": "#e7d146"
        }
    },
    {
        selector: ".singleton",
        css: {
            "background-color": "#f87060"
        }
    },
    {
        selector: "node",
        css: {
            content: "data(id)",
            "text-valign": "top",
            "text-halign": "left",
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
