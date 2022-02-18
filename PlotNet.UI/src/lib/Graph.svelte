<script lang="ts">
  import cytoscape from "cytoscape";
  import type { CytoscapeElement } from "src/models/CytoscapeElement";
  import { onMount } from "svelte";

  let el;
  let graph;

  export let data: CytoscapeElement[];

  onMount(() => {
    graph = cytoscape({
      container: el,
      elements: data,
      wheelSensitivity: 0.2,
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
  });
</script>

<div bind:this={el} class="graph" />

<style>
  .graph {
    flex-grow: 1;
    top: 0px;
    left: 0px;
  }
</style>
