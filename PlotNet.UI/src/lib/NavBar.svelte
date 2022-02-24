<script lang="ts">
  import logo from "../assets/PlotNet-logos_transparent.png";
  import {
    Button,
    Checkbox,
    Modal,
    OverflowMenu,
    OverflowMenuItem,
  } from "carbon-components-svelte";
  import LogoGithub32 from "carbon-icons-svelte/lib/LogoGithub32";
  import Menu32 from "carbon-icons-svelte/lib/Menu32";
  import { graphManager } from "../stores/graph.manager";
  import Drawer from "./Drawer.svelte";

  let open = false;

  let showFilterServices = false;
  let allSelected = true;
</script>

<nav>
  <div class="left">
    <OverflowMenu icon={Menu32}>
      <OverflowMenuItem
        text="Filter Services"
        on:click={() => (showFilterServices = !showFilterServices)}
      />
    </OverflowMenu>
  </div>
  <div class="center">
    <img src={logo} alt="Svelte Logo" />
  </div>
  <div class="right">
    <a href="https://github.com/mducharm/PlotNet" class="github">
      <LogoGithub32 />
    </a>
  </div>
</nav>

<Drawer bind:open={showFilterServices}>
  <div slot="top">
    <Checkbox
      labelText={"Select/Deselect All"}
      bind:checked={allSelected}
      on:click={() => {
        graphManager.toggleSelectAll(allSelected);
        allSelected = !allSelected;
      }}
    />
  </div>

  <div slot="body">
    {#each $graphManager.elements as el}
      {#if el.scratch.isInterface}
        <Checkbox
          labelText={el.data.id}
          bind:checked={el.isVisible}
          on:click={() => {
            graphManager.toggleVisibility(el);
          }}
        />
      {/if}
    {/each}
  </div>
</Drawer>

<style>
  nav {
    display: flex;
    flex: auto;
    flex-direction: row;
    justify-content: center;
    background-color: #efefef;
    overflow: visible;
    height: 6rem;
    width: 100%;
  }

  .left,
  .center,
  .right {
    flex-basis: 100%;
  }

  .left {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    margin: 20px;
  }

  .center {
    display: flex;
    flex-direction: row;
    justify-content: center;
  }

  .right {
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    margin: 20px;
  }

  img {
    height: 7rem;
    width: 7rem;
  }

  .github {
    color: black;
  }
</style>
