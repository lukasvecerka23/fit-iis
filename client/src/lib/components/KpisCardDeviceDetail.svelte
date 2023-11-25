<!-- UsersCard.svelte -->
<script>
    import KpiCompDeviceDetail from './KpiCompDeviceDetail.svelte';
    import {onMount} from 'svelte';
    import {selectedParameterId} from '../../store.js';
    
    let currentPageIndex = 0;
    let isLoading = true;
    let totalPages = 0;
    const pageSize = 10;
    let kpis = null;

    async function fetchKpis(parameterId) {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
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
        selectedParameterId.subscribe(value => {
            fetchKpis(value);
        });
    });
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
  
  </div>
  