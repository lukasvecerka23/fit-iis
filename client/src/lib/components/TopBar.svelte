<script>
    import NavButton from "./NavButton.svelte";
    import { Link} from "svelte-routing";
    $: activepage = location;

    function isActivePage(page) {
        return activepage.pathname === page;
    }
</script> 

<nav class=" bg-slate-500 w-full h-10 text-white flex justify-between items-center px-4">
    <div class="flex space-x-4 ml-auto">
        <Link to="/login" class="{isActivePage('/login') ? 'text-blue-500' : ''}">Log in</Link>
        <Link to="/signup" class="{isActivePage('/signup') ? 'text-blue-500' : ''}">Sign up</Link>
    </div>
  </nav>