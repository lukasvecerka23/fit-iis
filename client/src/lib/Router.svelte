<script>
    import { Router, Route } from 'svelte-routing';
    import Counter from './Counter.svelte';

</script>

<Router>
    <Route path="/" component={Counter}/>
</Router>