<!-- UsersCard.svelte -->
<script>
    import ParameterCompDeviceDetail from './ParameterCompDeviceDetail.svelte';
    export let parameters;
  </script>
  
  <div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Jméno</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Min</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Max</th>
                <th scope="col" class="py-3 px-6 text-center w-1/6">Status</th>
            </tr>
        </thead>
        <tbody>
            {#each parameters as parameter (parameter.id)}
            <ParameterCompDeviceDetail parameter={parameter}/>
            {/each}
        </tbody>
    </table>
  
  </div>
  