<script lang="ts">
  import cytoscape from "cytoscape";
  import type { CytoscapeElement } from "src/models/CytoscapeElement";
  import { onMount } from "svelte";
  import { styles } from "./GraphStyles";

  let el;
  let graph;

  export let data: CytoscapeElement[];

  onMount(() => {
    graph = cytoscape({
      container: el,
      elements: data,
      wheelSensitivity: 0.2,
      boxSelectionEnabled: false,
      style: styles,
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
