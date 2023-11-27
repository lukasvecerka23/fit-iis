<!-- SystemDetail.svelte -->

<script>
    import { onMount, onDestroy } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import UsersCard from '../../components/UsersCardSystemDetail.svelte';
    import DevicesCard from '../../components/DevicesCardSystemDetail.svelte';
    import AssignCard from './AssignsCard.svelte';
    import Users from '../../../assets/users.svg'
    import Device from '../../../assets/device.svg';
    import DeviceDark from '../../../assets/device_dark.svg';
    import UsersDark from '../../../assets/users_dark.svg';
    import Assign from '../../../assets/assign.svg';
    import AssignDark from '../../../assets/assign_dark.svg';
    import Edit from '../../../assets/edit_black.svg'
    import { user, systemDetailActiveCard } from '../../../store.js';
  
    export let id;

    let system = null;
    let isLoading = true;
    let intervalId;
    let searchTerm = '';
    let currentPageIndex = 0;
    const pageSize = 10;
    let totalPages = 0;
    let devices = [];

    async function fetchDevices() {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
            systemId: id
        });
        if (searchTerm.length >= 3) {
            params.append('q', searchTerm);
        }

        try {
            const resp = await fetch(`https://localhost:7246/api/devices/search?${params}`);
            if (resp.ok){
                const data = await resp.json();
                devices = data.devices;
                totalPages = data.totalPages; // Update this based on your API response
            }
        } finally {
        }
    }

    async function fetchSystemDetail() {
        try {
            const resp = await fetch(`https://localhost:7246/api/systems/${id}`, 
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
            });
            if (resp.ok){
                system = await resp.json();
                await fetchDevices();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }

    let isSmallScreen = false;

    //for devices and users buttons description
    onMount(() => {
    const mediaQuery = window.matchMedia('(max-width: 1200px)');
    isSmallScreen = mediaQuery.matches;

    const updateScreenSize = () => {
        isSmallScreen = mediaQuery.matches;
    };

    mediaQuery.addListener(updateScreenSize);

    return () => {
        mediaQuery.removeListener(updateScreenSize);
    };
    });

    onMount( () => {
        systemDetailActiveCard.set('devices');
        fetchSystemDetail();
    });

    function MoveToUpdate(){
      navigate(`/systems/${id}/update`);
    }

    function goToPage(page) {
        currentPageIndex = page;
        if ($systemDetailActiveCard === 'devices'){
            fetchDevices();
        }
    }

    function startPolling() {
        if ($systemDetailActiveCard === 'devices') {
            intervalId = setInterval(fetchDevices, 5000);
        } else if ($systemDetailActiveCard === 'users') {
            fetchSystemDetail();
        }
    }

    function stopPolling() {
        if (intervalId) {
            clearInterval(intervalId);
            intervalId = null;
        }
    }

    $: if ($systemDetailActiveCard) {
        stopPolling(); // Stop any existing polling
        startPolling(); // Start new polling based on activeCard
    }

    onDestroy(() => {
        stopPolling(); // Make sure to clear the interval when the component is destroyed
    });

    function canSeeAssigns(){
        return $user && ($user.userId === system.creatorId || $user.role === "Admin")
    }
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
                    <h2 class="text-3xl font-bold mb-0 pt-10 pb-4 font-poppins-light text-left">{system.name}</h2>
                    <div class="">
                        <div class="pl-5">
                            <button class=" hover:bg-slate-200 mt-6 p-1  text-white font-medium rounded-3xl" on:click={() => MoveToUpdate()}>
                                <img src={Edit} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                    </div>
                </div>
                <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{system.description}</h1>
                <div class="flex-row flex pb-2">
                    <h1 class=" text-lg font-semibold text-black font-poppins-light text-left">Vytvořil:</h1>
                    <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">{system.creatorName}</h1>
                </div>
                <div class="grid { canSeeAssigns() ? "w-1/2 grid-cols-3" : "w-1/4 grid-cols-2"} gap-2 rounded-3xl bg-gray-300 p-1">
                    <div>
                        <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={() => (systemDetailActiveCard.set('devices'))}/>
                        <label for="1" class="{$systemDetailActiveCard === 'devices' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if $systemDetailActiveCard === 'devices'}
                                <img src={Device} alt="Device" class="w-6 h-6" />
                                {:else}
                                <img src={DeviceDark} alt="DeviceDark" class="w-6 h-6" />
                                {/if}
                                {#if !isSmallScreen}
                                    <p class="pl-2">Zařízení</p>
                                {/if}
                            </div>
                        </label>
                    </div>
            
                    <div>
                        <input type="radio" name="option" id="2" value="2" class="peer hidden" on:click={() => (systemDetailActiveCard.set('users'))}/>
                        <label for="2" class="{$systemDetailActiveCard === 'users' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400' } radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if $systemDetailActiveCard === 'users'}
                                <img src={Users} alt="Users" class="w-6 h-6" />
                                {:else}
                                <img src={UsersDark} alt="UsersDark" class="w-6 h-6" />
                                {/if}
                                {#if !isSmallScreen}
                                    <p class="pl-2">Uživatelé</p>
                                {/if}
                            </div>
                        </label>
                    </div>
                    
                    {#if canSeeAssigns()}
                    <div>
                        <input type="radio" name="option" id="3" value="3" class=" peer hidden" checked on:click={() => (systemDetailActiveCard.set('assigns'))}/>
                        <label for="3" class="{$systemDetailActiveCard === 'assigns' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if $systemDetailActiveCard === 'assigns'}
                                <img src={Assign} alt="Assigns" class="w-6 h-6" />
                                {:else}
                                <img src={AssignDark} alt="Assigns" class="w-6 h-6" />
                                {/if}
                                {#if !isSmallScreen}
                                    <p class="pl-2">Žádosti</p>
                                {/if}
                            </div>
                        </label>
                    </div>
                    {/if}
                </div>
                <div class="pt-4">
                    {#if $systemDetailActiveCard === 'devices'}
                    <DevicesCard devices={devices} />
                    {:else if $systemDetailActiveCard === 'users'}
                    <UsersCard users={system.users}/>
                    {:else if $systemDetailActiveCard === 'assigns'}
                    <AssignCard systemId={id}/>
                    {/if}
                </div>
                

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  