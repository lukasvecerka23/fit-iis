<script>
    import NavButton from "./NavButton.svelte";
    import { Link , navigate} from "svelte-routing";
    import {user} from "../../store.js";
    $: activepage = location;

    function isActivePage(page) {
        return activepage.pathname === page;
    }

    async function handleLogout() {
        const response = await fetch('https://localhost:7246/api/auth/logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        });
        if (response.ok) {
            user.set(null);
            navigate('/', { replace: true });
        } else {
            // Handle login error
        }
    }
</script> 

<nav class=" bg-slate-500 w-full h-10 text-white flex justify-between items-center px-4 border-b border-gray-800 shadow-sm">
    <div class="flex space-x-4 ml-auto ">
        {#if $user}
            <button class="hover:bg-slate-400 rounded-xl p-1" on:click={async () => await handleLogout()}>Odhlásit</button>
        {:else}
            <Link to="/login" class="hover:bg-slate-400 rounded-xl p-1">Přihlásit se</Link>
            <Link to="/signup" class="hover:bg-slate-400 rounded-xl p-1">Registrace</Link>
        {/if}

    </div>
  </nav>