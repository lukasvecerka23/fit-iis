<!-- DevicesCard.svelte -->
<script>
    import DeviceCompSystemDetail from './DeviceCompSystemDetail.svelte';
    export let devices;

    function deleteDevice(id) {
        devices = devices.filter(device => device.id !== id);
    }
  </script>
  
<div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Název</th>
                <th scope="col" class="py-3 px-6">Typ zařízení</th>
                <th scope="col" class="py-3 px-6">Vytvořil</th>
                <th scope="col" class="py-3 px-6">Status</th>
                <th scope="col" class="py-3 px-6"></th>
                <th scope="col" class="py-3 px-6"></th>
            </tr>
        </thead>
        <tbody>
            {#each devices as device (device.id)}
            <DeviceCompSystemDetail device={device} on:deleteDevice={detail => deleteDevice(detail.detail.id)}/>
            {/each}
        </tbody>
    </table>

    </div>
