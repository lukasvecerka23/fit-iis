<!-- UsersCard.svelte -->
<script>
    import KpiCompDeviceDetail from './KpiCompDeviceDetail.svelte';
    import {onMount, onDestroy} from 'svelte';
    import {selectedParameterId} from '../../store.js';

    export let deviceId;

    let intervalId;
    let currentPageIndex = 0;
    let isLoading = true;
    let totalPages = 0;
    const pageSize = 10;
    let kpis = null;


    async function fetchKpis(parameterId) {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
            deviceId: deviceId
        });
        if (parameterId) {
            params.append('parameterId', parameterId);
        }
        const resp = await fetch(`https://localhost:7246/api/kpis/search?${params}`);
        if (resp.ok){
            const data = await resp.json();
            kpis = data.kpis;
            totalPages = data.totalPages;
            isLoading = false;
        }
    }

    onMount(() => {
        const unsubscribe = selectedParameterId.subscribe(value => {
            fetchKpis(value);
        });

        intervalId = setInterval(() => {
            // Re-fetch KPIs with the current selected parameter
            selectedParameterId.subscribe(value => {
                fetchKpis(value);
            })();
        }, 5000);
        
        return unsubscribe;
    });

    onDestroy(() => {
        clearInterval(intervalId);
    });

    function goToPage(page) {
        currentPageIndex = page;
        fetchKpis();
    }
  </script>
  
  <div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Parametr</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Funkce</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Hodnota</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Status</th>
            </tr>
        </thead>
        <tbody>
            {#if !isLoading}
                {#each kpis as kpi (kpi.id)}
                    <KpiCompDeviceDetail kpi={kpi}/>
                {/each}
            {/if}
        </tbody>
    </table>
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
  