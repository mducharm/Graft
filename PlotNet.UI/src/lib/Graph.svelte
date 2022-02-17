<script lang="ts">
  import cytoscape from "cytoscape";
  import { onMount } from "svelte";

  let container;
  let graph;

  export let data;

  onMount(() => {
    graph = cytoscape({
      container,
      elements: data,
      style: [
        // the stylesheet for the graph
        {
          selector: "node",
          style: {
            "background-color": "#666",
            label: "data(id)",
          },
        },

        {
          selector: "edge",
          style: {
            width: 3,
            "line-color": "#ccc",
            "target-arrow-color": "#ccc",
            "target-arrow-shape": "triangle",
            "curve-style": "bezier",
          },
        },
      ],

      layout: {
        name: "grid",
        rows: 1,
      },
    });

    // graph.add([
    //   { group: "nodes", data: { id: "n0" }, position: { x: 100, y: 100 } },
    //   { group: "nodes", data: { id: "n1" }, position: { x: 200, y: 200 } },
    //   { group: "edges", data: { id: "e0", source: "n0", target: "n1" } },
    // ]);
  });
</script>
test
<div bind:this={container} class="container" />

<style>
  .container {
    width: 100%;
    height: 100%;
    background-color: green;
    /* position: absolute; */
    top: 0px;
    left: 0px;
    border: 1px;
    border-color: blue;
  }
  .container > canvas {
    width: 100%;
    height: 100%;

  }
</style>
