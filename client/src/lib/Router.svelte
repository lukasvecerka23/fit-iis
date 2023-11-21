<script>
    import {Router, Route} from 'svelte-routing';
    import HomePage from './HomePage.svelte';
    import Systems from './pages/systems/Systems.svelte';
    import Devices from './pages/devices/Devices.svelte';
</script>

<Router>
    <Route path="/" component={HomePage}/>
    <Route path="/systems" component={Systems} />
    <Route path="/devices" component={Devices} />
</Router>