<script lang="ts">
  import { Tile } from "carbon-components-svelte";
  import { fly } from "svelte/transition";

  export let open = false;
</script>

{#if open}
  <nav transition:fly={{ x: -100 }}>
    <Tile>
      <slot name="top" />
      <hr>
      <div class="content">
        <slot name="body" />
      </div>
    </Tile>
  </nav>
{/if}

<style>
  nav {
    width: fit-content;
    height: 50%;
    padding: 10px;
    position: fixed;
    overflow-y: hidden;
    z-index: 10;
    border-radius: 10px;
  }
  .content {
    max-height: 500px;
    overflow-y: scroll;
    z-index: 11;
  }

  .content {
    -ms-overflow-style: none; /* Internet Explorer 10+ */
    scrollbar-width: none; /* Firefox */
  }
  .content::-webkit-scrollbar {
    display: none; /* Safari and Chrome */
  }
</style>
