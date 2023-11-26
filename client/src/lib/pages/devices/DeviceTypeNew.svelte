<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import New from '../../../assets/new.svg';

    let isLoading = true;
    let isSmallScreen = false;
    let systems = [];
    let deviceTypes = [];
    let parameters = [];
    let measurementValue = null;

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

    async function getSystems(){
        try {
            const response = await fetch(`https://localhost:7246/api/systems`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                systems = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
        isLoading = false;
    }

    async function getDeviceTypes(){
        try {
            const response = await fetch(`https://localhost:7246/api/deviceTypes`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                deviceTypes = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
        isLoading = false;
    }

    function addParameter() {
        parameters = [...parameters, 'Ahoj'];
    }

    onMount(getSystems);
    onMount(getDeviceTypes);



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
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">Nový typ zařízení</h2>
                </div>
                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Jméno</label>
                    </div>
                    <input type="text" id="username" class="w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Přidejte jméno..."/>
                </div>
                <div class="mb-4 w-full">
                    <label for="parameters" class="block w-1/3 mb-1 text-lg font-medium text-gray-700">Parametry</label>
                    {#each parameters as parameter}
                    <div class="pb-2 flex-row flex items-center">
                        <input type="text" id="name" class="w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Název parametru"/>
                        <label for="min" class=" px-3 block mb-1 text-base font-medium text-gray-700">Min. hodnota:</label>
                        <input type="number" bind:value={measurementValue} required class="border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer" />
                        <label for="min" class=" px-3 block mb-1 text-base font-medium text-gray-700">Max. hodnota:</label>
                        <input type="number" bind:value={measurementValue} required class="border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer" />
                    </div>
                    {/each}
                    <div class="flex  w-1/3 justify-start">
                        <button 
                            on:click={addParameter}
                            class="px-2 py-2 rounded-3xl bg-slate-500 hover:bg-slate-700 text-white">
                            <img src={New} alt="New" class="h-5 w-5"/>
                        </button>
                    </div>
                </div>
                <div class="flex  w-1/3 justify-end">
                    <button 
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

  