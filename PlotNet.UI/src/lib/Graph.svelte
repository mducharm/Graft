<script lang="ts">
  import cytoscape from "cytoscape";
  import type { CytoscapeElement } from "src/models/CytoscapeElement";
  import { onMount } from "svelte";
  import { styles } from "./GraphStyles";
  import coseBilkent from "cytoscape-cose-bilkent";
  import { graphManager, Layout } from "../stores/graph.manager";

  cytoscape.use(coseBilkent);

  let el;


  onMount(() => {
    $graphManager.graph = cytoscape({
      container: el,
      elements: $graphManager.elements,
      wheelSensitivity: 0.2,
      boxSelectionEnabled: false,
      style: styles,
      layout: {
        name: $graphManager.layout
        // rows: 1,
      },
    });

    // $graphManager.graph.$(`#IServiceA`).addClass("hidden");

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
