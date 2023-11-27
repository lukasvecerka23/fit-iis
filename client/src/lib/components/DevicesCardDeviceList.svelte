<!-- DevicesCard.svelte -->
<script>
    import DeviceCompDeviceDetail from './DeviceCompDeviceList.svelte';
    import {devices} from '../../store.js';
  </script>
  
<div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 px-6 text-left">Alias</th>
                <th scope="col" class="py-3 px-6">Identifikátor</th>
                <th scope="col" class="py-3 px-6">Typ zařízení</th>
                <th scope="col" class="py-3 px-6">Systém</th>
                <th scope="col" class="py-3 px-6">Vytvořil</th>
                <th scope="col" class="py-3 px-6">Status</th>
                <th scope="col" class="py-3 px-6"></th>
                <th scope="col" class="py-3 px-6"></th>
            </tr>
        </thead>
        <tbody>
            {#each $devices as device (device.id)}
            <DeviceCompDeviceDetail device={device}/>
            {/each}
        </tbody>
    </table>

    </div>
