<!-- UsersCard.svelte -->
<script>
    import MeasurementsCompDeviceDetail from './MeasurementsCompDeviceDetail.svelte';
    import {onMount} from 'svelte';
    import {selectedParameterId} from '../../store.js';
    import config from '../../config.js';
    
    export let deviceId;

    let currentPageIndex = 0;
    let totalPages = 0;
    const pageSize = 10;
    let isLoading = true;
    let measurements = null;

    async function fetchMeasurements(parameterId) {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
            deviceId: deviceId
        });
        if (parameterId) {
            params.append('parameterId', parameterId);
        }
        const resp = await fetch(`${config.apiUrl}/api/measurements/search?${params}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        });
        if (resp.ok){
            const data = await resp.json();
            measurements = data.measurements;
            totalPages = data.totalPages;
            isLoading = false;
        }
    }

    onMount(() => {
        selectedParameterId.subscribe(value => {
            fetchMeasurements(value); })});

    
    function goToPage(page) {
        currentPageIndex = page;
        fetchMeasurements();
    }
  </script>
  
  {#if isLoading}
  <div class="flex flex-col w-full h-screen bg-slate-400">
      <p>Loading...</p>
  </div>
  {:else}
  <div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Parametr</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Čas</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Hodnota</th>
            </tr>
        </thead>
        <tbody>
            {#if !isLoading}
                {#each measurements as measurement (measurement.id)}
                    <MeasurementsCompDeviceDetail measurement={measurement}/>
                {/each}
            {/if}
        </tbody>
    </table>
  
    <!-- Pagination Controls -->
    <div class="flex gap-2 items-center my-4">
        <button 
            class="px-4 py-2 rounded-xl bg-slate-500 hover:bg-slate-600 disabled:hover:bg-slate-500 text-white disabled:hidden" 
            on:click={goToPage(currentPageIndex - 1)} 
            disabled={currentPageIndex === 0}>
            Zpět
        </button>
        <button 
            class="px-4 py-2 rounded-xl bg-slate-500 hover:bg-slate-600 text-white disabled:hover:bg-slate-500 disabled:hidden" 
            on:click={goToPage(currentPageIndex + 1)} 
            disabled={!totalPages ? true : currentPageIndex === totalPages - 1}>
            Další
        </button>
    </div>
  </div>
  {/if}
  