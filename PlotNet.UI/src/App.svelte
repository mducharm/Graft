<script lang="ts">
  import "carbon-components-svelte/css/white.css";
  import Graph from "./lib/Graph.svelte";
  import { CytoscapeElement, mapToElement } from "./models/CytoscapeElement";
  import dummy_data from './dummy_data.json';
  import type { GraphDTO } from "./models/GraphDTO";
  import NavBar from "./lib/NavBar.svelte";
import RightClickMenu from "./lib/RightClickMenu.svelte";

  function getGraphData(): Promise<CytoscapeElement[]>{
    // return Promise.resolve(JSON.parse(`{"nodes":[{"id":"IServiceA","data":{"Lifetime":"Transient"}},{"id":"ServiceA","data":{"Lifetime":"Transient","Parent":"IServiceA"}},{"id":"IServiceB","data":{"Lifetime":"Transient"}},{"id":"ServiceB","data":{"Lifetime":"Transient","Parent":"IServiceB"}}],"edges":[{"a":"ServiceA","b":"IServiceB"}]}`))
    return Promise.resolve(dummy_data as GraphDTO)
      .then(r => mapToElement(r));
    // return fetch('/plotnet.json')
    //   .then(r => r.json())
    //   .then(r => mapToElement(r));
  }
</script>

<NavBar></NavBar>
<RightClickMenu></RightClickMenu>

<main>
  {#await getGraphData()}
    <p>...waiting</p>
  {:then data}
    {#if data.length === 0}
      <p>No services found.</p>
    {:else}
      <Graph {data} />
    {/if}
  {:catch error}
    <p style="color: red">{error.message}</p>
  {/await}
</main>

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
