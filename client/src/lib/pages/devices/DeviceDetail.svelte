<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import Eye from '../../../assets/eye.svg';
  
    export let id;

    let device = null;
    let isLoading = true;
    let activeCard = 'parameters';
    let isSmallScreen = false;

    async function fetchDeviceDetail() {
        try {
            const resp = await fetch(`https://localhost:7246/api/devices/${id}`);
            if (resp.ok){
                device = await resp.json();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }

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

    onMount(fetchDeviceDetail);

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
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-4 font-poppins-light text-left">{device.userAlias}</h2>
                <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{device.description}</h1>
                <div class="flex-row flex pb-2">
                    <h1 class=" text-lg font-medium text-black font-poppins-light text-left">Vytvořil:</h1>
                    <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">CreatorName</h1>
                </div>
                <div class="grid w-1/6 grid-cols-1 gap-2 rounded-3xl bg-gray-300 p-1">
                    <div>
                        <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={() => (activeCard = 'parameters')}/>
                        <label for="1" class="{activeCard === 'parameters' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                            <div class="flex-row flex justify-center">
                                {#if activeCard === 'parameters'}
                                <img src={Eye} alt="Eye" class="w-6 h-6" />
                                {/if}
                                {#if !isSmallScreen}
                                    <p class="pl-2">Parametry</p>
                                {/if}
                            </div>
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  