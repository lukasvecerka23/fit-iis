<script>
    import {Router, Route} from 'svelte-routing';
    import HomePage from './HomePage.svelte';
    import Systems from './pages/systems/Systems.svelte';
    import Devices from './pages/devices/Devices.svelte';
    import SystemDetail from './pages/systems/SystemDetail.svelte';
</script>

<Router>
    <Route path="/" component={HomePage}/>
    <Route path="/systems" component={Systems} />
    <Route path="/devices" component={Devices} />
    <Route path="/systemDetail" component={SystemDetail} />
    <Route path="/systemDetail/:id" let:params>
        <SystemDetail id={params.id}/>
    </Route>
</Router>