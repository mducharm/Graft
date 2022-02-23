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

  let open = false;
</script>

<nav>
  <div class="left">
    <OverflowMenu icon={Menu32}>
      <OverflowMenuItem text="Filter Services" on:click={() => (open = true)} />
      <OverflowMenuItem text="Settings" />
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

<Modal passiveModal bind:open modalHeading="Filter Services" on:open on:close>
  <p>Create a new Cloudant database in the US South region.</p>

  {#each $graphManager.elements as el}
    <Checkbox labelText={el.data.id} />
  {/each}
</Modal>

<style>
  nav {
    display: flex;
    flex: auto;
    flex-direction: row;
    justify-content: center;
    background-color: #efefef;
    overflow: visible;
    height: 6rem;
    max-width: 1460px;
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
