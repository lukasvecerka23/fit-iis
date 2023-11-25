<script>
  import Router from "./lib/Router.svelte";
  import SystemDetail from './lib/pages/systems/SystemDetail.svelte';
  import { user, loadUser } from './store.js';
  import { onMount } from 'svelte';
  import { get } from 'svelte/store';

  onMount(async () => {
    if (get(user) == null){
      await loadUser();
    }
  })
</script>

<main class="font-poppins">
  <Router />
</main>