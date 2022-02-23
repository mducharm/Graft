<script lang="ts">
  import { Tag, Tile } from "carbon-components-svelte";
</script>

<div class="fixed">
    <Tile>
        <Tag style="background-color: #4EA5D9; width: 5rem;">Transient</Tag>
        <Tag style="background-color: #ffb11f; width: 5rem;">Scoped</Tag>
        <Tag style="background-color: #f87060; width: 5rem;">Singleton</Tag>
    </Tile>
</div>

<style>
  div.fixed {
    position: fixed;
    bottom: 2%;
    left: 1%;
  }
</style>
