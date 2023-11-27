<script>
    import NavButton from "./NavButton.svelte";
    import { Link , navigate} from "svelte-routing";
    import {user} from "../../store.js";
    import config from "../../config.js";
    $: activepage = location;

    function isActivePage(page) {
        return activepage.pathname === page;
    }

    async function handleLogout() {
        const response = await fetch(`${config.apiUrl}/api/auth/logout`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        });
        if (response.ok) {
            navigate('/', { replace: true });
            user.set(null);
        } else {
            // Handle login error
        }
    }
</script> 

<nav class=" bg-gray-800 w-full h-10 text-white flex justify-between items-center px-4 border-b border-gray-500 shadow-sm">
    <div class="flex space-x-4 ml-auto ">
        {#if $user}
            <div class="flex flex-row gap-4 items-center">
                <h1 class="text-white">{$user.username}</h1>
                <button class="hover:bg-slate-400 rounded-xl p-1" on:click={async () => await handleLogout()}>Odhlásit</button>
            </div>
        {:else}
            <Link to="/login" class="hover:bg-slate-400 rounded-xl p-1">Přihlásit se</Link>
            <!-- <Link to="/signup" class="hover:bg-slate-400 rounded-xl p-1">Registrace</Link> -->
        {/if}

    </div>
  </nav>