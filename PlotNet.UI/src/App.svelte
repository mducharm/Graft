<script lang="ts">
  import "carbon-components-svelte/css/white.css";
  import { Button } from "carbon-components-svelte";

  import logo from "./assets/PlotNet-logos.jpeg";
  import Graph from "./lib/Graph.svelte";

  function getGraphData() {
    // TODO use fetch call to get data
    return Promise.resolve([
      {
        data: { id: "a" },
      },
      {
        data: { id: "b" },
      },
      {
        data: { id: "ab", source: "a", target: "b" },
      },
    ]);
  }
</script>

<nav>
  <img src={logo} alt="Svelte Logo" />
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

  main {
    text-align: center;
    padding: 1em;
    margin: 0 auto;
  }

  img {
    border-radius: 50%;
    border: 1px solid rgb(194, 194, 194);
    height: 7rem;
    width: 7rem;
  }

  h1 {
    color: #ff3e00;
    text-transform: uppercase;
    font-size: 4rem;
    font-weight: 100;
    line-height: 1.1;
    margin: 2rem auto;
    max-width: 14rem;
  }

  p {
    max-width: 14rem;
    margin: 1rem auto;
    line-height: 1.35;
  }

  @media (min-width: 480px) {
    h1 {
      max-width: none;
    }

    p {
      max-width: none;
    }
  }

</style>
