<script lang="ts">
  import logo from "../assets/PlotNet-logos_transparent.png";
  import LogoGithub32 from "carbon-icons-svelte/lib/LogoGithub32";
</script>

<nav>
  <div class="left" />
  <div class="center">
    <img src={logo} alt="Svelte Logo" />
  </div>
  <div class="right">
    <a href="https://github.com/mducharm/PlotNet" class="github">
      <LogoGithub32 />
    </a>
  </div>
</nav>

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
