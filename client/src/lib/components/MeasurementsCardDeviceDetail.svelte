<!-- UsersCard.svelte -->
<script>
    import MeasurementsCompDeviceDetail from './MeasurementsCompDeviceDetail.svelte';
    import {onMount} from 'svelte';
    import {selectedParameterId} from '../../store.js';
    
    let currentPageIndex = 0;
    let totalPages = 0;
    const pageSize = 10;
    let isLoading = true;
    let measurements = null;

    async function fetchMeasurements(parameterId) {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
        });
        if (parameterId) {
            params.append('parameterId', parameterId);
        }
        const resp = await fetch(`https://localhost:7246/api/measurements/search?${params}`);
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
                <th scope="col" class="py-3 px-6 text-center">Čas</th>
                <th scope="col" class="py-3 px-6 text-center">Hodnota</th>
                <!-- <th scope="col" class="py-3 px-6 text-center">Status</th> -->
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
  
  </div>
  {/if}
  