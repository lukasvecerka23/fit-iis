<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import { user} from "../../../store.js";


    export let id;

    let isLoading = true;
    let isSmallScreen = false;
    let devices = [];
    let showDescription = false;
    let selectedDevices = new Set();
    let system = null;

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

    async function getDevices(){
        try {
            const response = await fetch(`https://localhost:7246/api/devices`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                devices = await response.json();
                devices = devices.filter(device => (device.systemId === null || device.systemId === id) && ($user && (device.creatorId === $user.userId || $user.role === "Admin")));
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
    }

    function toggleDeviceSelection(deviceId, event) {
        if (event.target.checked) {
            selectedDevices.add(deviceId);
        } else {
            selectedDevices.delete(deviceId);
        }
        console.log(selectedDevices);
    }


    async function fetchSystemDetail() {
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
                system.devices.forEach(device => selectedDevices.add(device.id));
                isLoading = false;
            } else {
                throw new Error('Error')
            }
    }

    async function saveSystem(){
        const updatedDeviceIds = Array.from(selectedDevices);
        const resp = await fetch(`https://localhost:7246/api/systems/${id}`, 
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: system.name,
                description: system.description,
                creatorId: system.creatorId,
                deviceIds: updatedDeviceIds
            }),
            credentials: 'include',
        });
        if (resp.ok){
            navigate(`/systems/${id}`, {replace: true});
        }
    }


    onMount(() => {
        getDevices();
        fetchSystemDetail();
    })

</script>


<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    {#if !isLoading}
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <div class="flex-row flex w-4/5 items-start">
            <div class="flex-col flex w-1/3 items-start">
                <div class = "flex-col flex w-full">
                    <div class = "flex-row flex w-full items-center pt-10">
                        <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">{system.name}</h2>
                    </div>
                    <div class="mb-4 w-full">
                        <div class="flex-row flex">
                            <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Název</label>
                        </div>
                        <input type="text" id="username" bind:value={system.name} class="w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Zadejte název systému..."/>
                    </div>
                    <div class="mb-4 w-full">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                        <textarea id="device-description" bind:value={system.description} class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis systému..."></textarea>
                    </div>
                    <div class="flex  w-full justify-end">
                        <button 
                            class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white" on:click={async () => await saveSystem()}>
                            Uložit
                        </button>
                    </div>
    
                </div>
            </div>
            <div class="font-semibold text-base w-1/2 pl-20 mt-20 pt-10 mb-4">
                <h3 class="block mb-1 text-lg font-medium text-gray-700">Zařízení v systému</h3>
                {#if devices.length == 0}
                        <p class="font-normal text-gray-700 italic">Momentálně neexistují zařízení bez systému.</p>
                {:else}
                <ul class="w-full text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg dark:bg-slate-700 dark:border-gray-600 dark:text-white">
                    {#each devices as device (device.id)}
                    <li class="w-full border-b border-gray-200 rounded-t-lg dark:border-slate-500">
                        <div class="flex items-center ps-3">
                            <input on:change={(event) => toggleDeviceSelection(device.id, event)} type="checkbox" value={device.id} checked={selectedDevices.has(device.id)} class="w-4 h-4 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-700 dark:focus:ring-offset-gray-700 focus:ring-2 dark:bg-slate-500 dark:border-gray-500">
                            <label for="device-checkbox-{device.id}" class="w-full py-3 ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                            >{device.userAlias}</label>
                        </div>
                    </li>
                    {/each}               
                </ul>
                {/if} 
              </div>
        </div>
    </div>
    {/if}
  </div>
</div>


  