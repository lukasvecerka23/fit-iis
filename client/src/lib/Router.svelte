<script>
    import {Router, Route} from 'svelte-routing';
    import HomePage from './HomePage.svelte';
    import Systems from './pages/systems/Systems.svelte';
    import Devices from './pages/devices/Devices.svelte';
    import SystemDetail from './pages/systems/SystemDetail.svelte';
    import DeviceDetail from './pages/devices/DeviceDetail.svelte';
    import LoginPage from './pages/auth/LoginPage.svelte';
    import RegisterPage from './pages/auth/RegisterPage.svelte';
    import Users from './pages/auth/Users.svelte';
    import DeviceNew from './pages/devices/DeviceNew.svelte';
    import SystemNew from './pages/systems/SystemNew.svelte';
    import Broker from './pages/broker/Broker.svelte';
    import BrokerDetail from './pages/broker/BrokerDetail.svelte';
</script>

<Router>
    <Route path="/" component={Systems} />
    <Route path="/devices" component={Devices} />
    <Route path="/systems/:id" let:params>
        <SystemDetail id={params.id}/>
    </Route>
    <Route path="/devices/:id" let:params>
        <DeviceDetail id={params.id}/>
    </Route>
    <Route path="/login" component={LoginPage} />
    <Route path="/signup" component={RegisterPage} />
    <Route path="/users" component={Users} />
    <Route path="/devices/new" component={DeviceNew}/>
    <Route path="/systems/new" component={SystemNew} />

    <Route path="/broker" component={Broker} />
    <Route path="/broker/:id" let:params>
        <BrokerDetail id={params.id}/>
    </Route>

</Router>