<!-- Router.svelte -->
<script>
    import { Router, Route, Link } from 'svelte-routing'
    import HomePage from '../pages/home-page/HomePage.svelte';
    import Systems from '../pages/systems/Systems.svelte';
</script>
  
<Router>
    <Route path="home" component={HomePage} />
    <Route path="systems" component={Systems} />
</Router>