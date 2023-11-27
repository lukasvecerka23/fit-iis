<!-- UserNew.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';

    let isLoading = true;
    let isSmallScreen = false;
    let roles = [];
    const user = {
        username: null,
        name: null,
        surname: null,
        roleId: null,
        password: null,
    }
    let checkDone = false;

    //for parameter button description
    onMount(() => {
    const mediaQuery = window.matchMedia('(max-width: 1100px)');
    isSmallScreen = mediaQuery.matches;

    const updateScreenSize = () => {
        isSmallScreen = mediaQuery.matches;
    };

    mediaQuery.addListener(updateScreenSize);

    return () => {
        mediaQuery.removeListener(updateScreenSize);
    };
    });

    async function getRoles(){
        try {
            const response = await fetch(`https://localhost:7246/api/roles`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                roles = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
        isLoading = false;
    }

    function checkMandatory()
    {
        if (user.name === null || user.roleId === null || user.surname === null || user.password === null || user.password === "" || user.username === "" || user.name === "" || user.surname === "")
        {
            checkDone = true;
            return false;
        }else
        {
            checkDone = true;
            return true;
        }
    }


    onMount(getRoles);

  </script>

{#if isLoading}
<div class="flex flex-col w-full h-screen bg-slate-400">
    <p>Loading...</p>
</div>
{:else}
<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <div class = "flex-row flex w-full items-center pt-10">
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">Nový uživatel</h2>
                </div>
                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Uživatelské jméno *</label>
                    </div>
                    <input bind:value={user.username} type="text" id="username" class={`w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700 ${checkDone && (user.username === null || user.username === "") ? 'border-red-500 border-2' : ''}`}/>
                </div>
                <div class="mb-4 w-1/3">
                    <label for="pwd" class="block mb-1 text-lg font-medium text-gray-700">Heslo *</label>
                    <input bind:value={user.password} type="password" id="pwd" class={`w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700 ${checkDone && (user.password === null || user.password === "") ? 'border-red-500 border-2' : ''}`}/>
                </div>
                <div class="mb-4 w-1/3">
                    <label for="name" class="block mb-1 text-lg font-medium text-gray-700">Jméno *</label>
                    <input bind:value={user.name} type="text" id="name" class={`w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700 ${checkDone && (user.name === null || user.name === "") ? 'border-red-500 border-2' : ''}`}/>
                </div>
                <div class="mb-4 w-1/3">
                    <label for="surname" class="block mb-1 text-lg font-medium text-gray-700">Příjmení *</label>
                    <input bind:value={user.surname}  type="text" id="surname" class={`w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700  ${checkDone && (user.surname === null || user.surname === "") ? 'border-red-500 border-2' : ''}`}/>
                </div>
                <div class="font-semibold text-base w-1/3 mb-4">
                    <label for="role" class="block mb-1 text-lg font-medium text-gray-700">Role *</label>
                    <select
                        bind:value={user.roleId} 
                        id="role"
                        class={`border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer ${checkDone && (user.roleId === null || user.roleId === "") ? 'border-red-500 border-2' : ''}`} >
                        <option value = {null}>Vyberte roli...</option>
                        {#each roles as role (role.id)}
                            <option value={role.id}>{role.name}</option>
                        {/each}
                    </select>
                </div>
                <div class="flex  w-1/3 justify-end">
                    <button on:click={() => checkMandatory()} 
                        class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                        Vytvořit
                    </button>
                </div>

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  