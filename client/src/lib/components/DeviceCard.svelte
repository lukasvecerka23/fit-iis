<!-- DevicesCard.svelte -->
<script>
    import DeviceComp from './DeviceComp.svelte';
    export let devices;
  </script>
  
<div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Název</th>
                <th scope="col" class="py-3 px-6">Typ zařízení</th>
                <th scope="col" class="py-3 px-6">Systém</th>
                <th scope="col" class="py-3 px-6">Vytvořil</th>
                <th scope="col" class="py-3 px-6"></th>
                <th scope="col" class="py-3 px-6"></th>
            </tr>
        </thead>
        <tbody>
            {#each devices as device (device.id)}
            <DeviceComp device={device}/>
            {/each}
        </tbody>
    </table>

    </div>
