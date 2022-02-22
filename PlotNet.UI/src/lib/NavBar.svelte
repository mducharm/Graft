<script lang="ts">
  import logo from "../assets/PlotNet-logos_transparent.png";
</script>

<nav>
  <div class="nav-container">
    <img src={logo} alt="Svelte Logo" />
  </div>
</nav>

<style>
  nav {
    display: flex;
    flex-direction: row;
    justify-content: center;
    background-color: #efefef;
    overflow: visible;
    height: 6rem;
  }

  .nav-container {
    max-width: 1460px;
  }

  img {
    height: 7rem;
    width: 7rem;
  }
</style>
