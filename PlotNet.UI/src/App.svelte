<script lang="ts">
  import "carbon-components-svelte/css/white.css";
  import logo from "./assets/PlotNet-logos_transparent.png";
  import Graph from "./lib/Graph.svelte";
  import { CytoscapeElement, mapToElement } from "./models/CytoscapeElement";

  function getGraphData(): Promise<CytoscapeElement[]>{
    return Promise.resolve(JSON.parse(`{"nodes":[{"id":"IServiceA","data":{"Lifetime":"Transient"}},{"id":"ServiceA","data":{"Lifetime":"Transient","Parent":"IServiceA"}},{"id":"IServiceB","data":{"Lifetime":"Transient"}},{"id":"ServiceB","data":{"Lifetime":"Transient","Parent":"IServiceB"}}],"edges":[{"a":"ServiceA","b":"IServiceB"}]}`))
      .then(r => mapToElement(r));
    // return fetch('/plotnet.json')
    //   .then(r => r.json())
    //   .then(r => mapToElement(r));
  }
</script>

<nav>
  <div class="nav-container">
    <img src={logo} alt="Svelte Logo" />
  </div>
</nav>
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
  nav {
    display: flex;
    flex-direction: row;
    justify-content: center;
    background-color: #efefef;
    overflow: visible;
    height: 6rem;
  }
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

  .nav-container {
    max-width: 1460px;
  }

  main {
    padding: 0;
    margin: 0 auto;
    display: flex;
    flex-flow: column-reverse nowrap;
    min-height: calc(100vh - (6rem));
  }

  img {
    height: 7rem;
    width: 7rem;
  }
</style>
