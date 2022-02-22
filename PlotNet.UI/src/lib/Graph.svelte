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
      boxSelectionEnabled: false,
      style: [
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
