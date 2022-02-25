<script lang="ts">
  import "carbon-components-svelte/css/white.css";
  import Graph from "./lib/Graph.svelte";
  import dummy_data from "./dummy_data.json";
  import type { GraphDTO } from "./models/GraphDTO";
  import NavBar from "./lib/NavBar.svelte";
  import RightClickMenu from "./lib/RightClickMenu.svelte";
  import { graphManager } from "./stores/graph.manager";
  import { onMount } from "svelte";
  import Legend from "./lib/Legend.svelte";

  onMount(async () => {
    let graphDTO = await fetch('/graft.json').then(r => r.json());
    // let graphDTO = await Promise.resolve(dummy_data as GraphDTO);

    graphManager.initialize(graphDTO);
  });
</script>

<NavBar />
<RightClickMenu />

<main>
  {#if $graphManager.elements.length === 0}
    <p>No services found.</p>
  {:else}
    <Graph />
  {/if}
</main>
<Legend />

<style>
  :root {
    font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen,
      Ubuntu, Cantarell, "Open Sans", "Helvetica Neue", sans-serif;
    margin: 0;
    height: 100%;
  }
  :global(html, body) {
    margin: 0;
    height: 100%;
  }

  main {
    padding: 0;
    margin: 0 auto;
    display: flex;
    flex-flow: column-reverse nowrap;
    min-height: calc(100vh - (6rem));
  }
</style>
