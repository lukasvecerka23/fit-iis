<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import {devices} from '../../../store.js';
    import {deviceTypes} from '../../../store.js';
    import Device from '../../../assets/device.svg';
    import DeviceDark from '../../../assets/device_dark.svg'
    import DevicesCardBroker from './DevicesCardBroker.svelte';
    import { Link, navigate } from "svelte-routing";
  
    let searchTerm = '';
    let currentPageIndex = 0;
    const pageSize = 10;
    let totalPages = 0;

    async function fetchDevices() {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
        });
        if (searchTerm.length >= 3) {
            params.append('q', searchTerm);
        }

        try {
            const resp = await fetch(`https://localhost:7246/api/devices/search?${params}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
            });
            if (resp.ok){
                const data = await resp.json();
                devices.set(data.devices);
                totalPages = data.totalPages; // Update this based on your API response
            }
        } finally {
        }
    }

    onMount(() => {
        fetchDevices();
    });


  $: if (searchTerm.length >= 3 || searchTerm.length === 0) {
        currentPageIndex = 0;
        fetchDevices();
    }

    function goToPage(page) {
        currentPageIndex = page;
        fetchDevices();
    }

</script>

<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <!-- Pole pro filtraci nad seznamem devices -->
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-6 font-poppins-light text-left">Zadávání měření</h2>
                <div class="flex flex-row">
                    <div class="pb-4 w-1/3 self-start">
                        <input
                        type="text"
                        class="p-2 border border-gray-300 rounded-xl w-full"
                        bind:value={searchTerm}
                        placeholder="Filtrovat zařízení..."
                        />
                    </div>
                </div>
        
            <div class="w-full pt-4">
                <DevicesCardBroker/>

                <!-- Pagination Controls -->
                <div class="flex gap-2 items-center my-4">
                    <button 
                        class="px-4 py-2 rounded-xl bg-slate-500 hover:bg-slate-600 disabled:hover:bg-slate-500 text-white disabled:text-gray-300" 
                        on:click={goToPage(currentPageIndex - 1)} 
                        disabled={currentPageIndex === 0}>
                        Zpět
                    </button>
                    <button 
                        class="px-4 py-2 rounded-xl bg-slate-500 hover:bg-slate-600 text-white disabled:hover:bg-slate-500 disabled:text-gray-300" 
                        on:click={goToPage(currentPageIndex + 1)} 
                        disabled={!totalPages ? true : currentPageIndex === totalPages - 1}>
                        Další
                    </button>
                </div>

            </div>
        </div>
    </div>
  </div>
</div>
</div>
  
