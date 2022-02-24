<script lang="ts">
  import {
    ContextMenu,
    ContextMenuDivider,
    ContextMenuOption,
    ContextMenuRadioGroup,
  } from "carbon-components-svelte";

  import { graphManager, Layout } from "../stores/graph.manager";

  let selectedId: Layout = "circle";

  $: graphManager.layout(selectedId);

</script>

<ContextMenu>
  <ContextMenuRadioGroup bind:selectedId labelText="Layout">
    <ContextMenuOption id="circle" labelText="Circle" />
    <ContextMenuOption id="grid" labelText="Grid" />
    <ContextMenuOption id="cose" labelText="CoSE" />
    <ContextMenuOption id="cose-bilkent" labelText="CoSE-Bilkent" />
    <ContextMenuOption id="concentric" labelText="Concentric" />
  </ContextMenuRadioGroup>
</ContextMenu>

<style>
</style>
