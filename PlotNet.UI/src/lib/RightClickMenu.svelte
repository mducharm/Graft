<script lang="ts">
  import {
    ContextMenu,
    ContextMenuDivider,
    ContextMenuOption,
    ContextMenuRadioGroup,
  } from "carbon-components-svelte";

  import { graphManager, Layout } from "../stores/graph.manager";

  let selectedId: Layout = "circle";

  $: console.log("selectedId", selectedId);

  $: graphManager.update(g => {
      g.layout = selectedId;
      return g;
  })
</script>

<ContextMenu>
  <!-- <ContextMenuOption indented labelText="Open" /> -->
  <ContextMenuDivider />
  <ContextMenuRadioGroup bind:selectedId labelText="Layout">
    <ContextMenuOption id="circle" labelText="Circle" />
    <ContextMenuOption id="grid" labelText="Grid" />
    <ContextMenuOption id="cose" labelText="CoSE" />
    <ContextMenuOption id="cose-bilkent" labelText="CoSE-Bilkent" />
    <ContextMenuOption id="concentric" labelText="Concentric" />
  </ContextMenuRadioGroup>
  <ContextMenuDivider />
  <!-- <ContextMenuOption selectable labelText="Lock layer" /> -->
</ContextMenu>

<style>
  div {
    position: absolute;
    width: calc(100% - var(--cds-spacing-05));
    height: calc(100% - var(--cds-spacing-06));
    display: flex;
    align-items: center;
    justify-content: center;
    color: var(--cds-text-02);
  }
</style>
