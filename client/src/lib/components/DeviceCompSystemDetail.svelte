<script>
    import Remove from "../../assets/remove.svg";
    import { navigate } from "svelte-routing";
    import Edit from "../../assets/edit.svg";
    import StatusOk from '../../assets/status_ok.svg';
    import StatusWarning from '../../assets/status_warning.svg';
    import StatusBad from '../../assets/status_bad.svg';
    import {DeviceStatus} from '../../utils.js';
    import {createEventDispatcher} from "svelte";


    const dispatch = createEventDispatcher();
    export let device;

    // async function DeleteDevice(id) {
    //     try {
    //         const response = await fetch(`https://localhost:7246/api/devices/${id}`, {
    //             method: 'DELETE',
    //             credentials: 'include',
    //         });

    //         if (response.ok) {
    //             // Remove the device from the local array
    //             dispatch('deleteDevice', { id: id });
    //         } else {
    //             console.error('Error deleting system:', await response.text());
    //         }
    //     } catch (error) {
    //         console.error('Error deleting system:', error);
    //     }
    // }

    function RemoveDeviceFromSystem(id)
    {

    }

    function MoveToDetail(deviceId){
      navigate(`/devices/${deviceId}`);
    }

    function MoveToUpdate(deviceId){
      navigate(`/devices/${deviceId}/update`)
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left text-gray-300 font-semibold text-base">
      <button class="hover:cursor-pointer hover:underline" on:click={() => MoveToDetail(device.id)}>
        {device.userAlias}
      </button>
    </td>
    <td class="py-4 px-6">{device.deviceTypeName}</td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {device.creatorName}
        </div>
      </div>
    </td>
    <td class="py-4 px-0">
      <div class="flex-row flex justify-center">
        {#if device.status === DeviceStatus.Okay}
        <div class="bg-green-600 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusOk} alt="StatusOk" class="" />
        </div>
      {:else if device.status === DeviceStatus.Warning}
        <div class="bg-orange-400 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusWarning} alt="StatusWarning" class="" />
        </div>
      {:else if device.status === DeviceStatus.Critical}
        <div class="bg-red-600 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusBad} alt="StatusBad" class="" />
        </div>
      {/if}
      </div>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>MoveToUpdate(device.id)}>
        <img src={Edit} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>RemoveDeviceFromSystem(device.id)}>
        <img src={Remove} alt="Remove" class="w-6 h-6" />
      </button>
    </td>
</tr>