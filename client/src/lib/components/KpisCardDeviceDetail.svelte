<!-- UsersCard.svelte -->
<script>
    import KpiCompDeviceDetail from './KpiCompDeviceDetail.svelte';
    export let parameters;
    
    let isLoading = true;
    let param = null;

    async function fetchParameterDetail(paramId) {
        try {
            const resp = await fetch(`https://localhost:7246/api/parameters/${paramId}`);
            if (resp.ok){
                param = await resp.json();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }
    fetchParameterDetail(parameters[0].id);
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
                <th scope="col" class="py-3 px-6 text-center">Funkce</th>
                <th scope="col" class="py-3 px-6 text-center">Hodnota</th>
                <th scope="col" class="py-3 px-6 text-center">Status</th>
            </tr>
        </thead>
        <tbody>
            {#if !isLoading}
                {#each parameters as parameter (parameter.id)}
                    <!-- {fetchParameterDetail(parameter.id)} -->
                    {#each param.kpis as kpi (kpi.id)}
                        <KpiCompDeviceDetail kpi={kpi}, parameter={parameter}/>
                    {/each}
                {/each}
            {/if}
        </tbody>
    </table>
  
  </div>
  {/if}
  