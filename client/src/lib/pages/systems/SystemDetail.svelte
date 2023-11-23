<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import UsersCard from '../../components/UsersCard.svelte';
    import DevicesCard from '../../components/DevicesCard.svelte';
    import Users from '../../../assets/users.svg'
    import Device from '../../../assets/device.svg';
    import DeviceDark from '../../../assets/device_dark.svg';
    import UsersDark from '../../../assets/users_dark.svg'
  
    export let id;

    let system = null;
    let isLoading = true;
    let activeCard = 'devices';

    async function fetchSystemDetail() {
        try {
            const resp = await fetch(`https://localhost:7246/api/systems/${id}`);
            if (resp.ok){
                system = await resp.json();
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

    onMount(fetchSystemDetail);

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
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-4 font-poppins-light text-left">{system.name}</h2>
                <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{system.description}</h1>
                <div class="flex-row flex pb-2">
                    <h1 class=" text-lg font-medium text-black font-poppins-light text-left">Vytvořil:</h1>
                    <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">{system.creatorName}</h1>
                </div>
                <div class="grid w-1/4 grid-cols-2 gap-2 rounded-3xl bg-gray-300 p-1">
                    <div>
                        <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={() => (activeCard = 'devices')}/>
                        <label for="1" class="{activeCard === 'devices' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if activeCard === 'devices'}
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
                        <input type="radio" name="option" id="2" value="2" class="peer hidden" on:click={() => (activeCard = 'users')}/>
                        <label for="2" class="{activeCard === 'users' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400' } radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if activeCard === 'users'}
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
                </div>
                <div class="pt-4">
                    {#if activeCard === 'devices'}
                    <DevicesCard devices={system.devices} />
                    {:else}
                    <UsersCard users={system.users} />
                    {/if}
                </div>
                

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  