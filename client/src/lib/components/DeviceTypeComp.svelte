<script>
    import TrashBin from "../../assets/trash.svg";
    import {deviceTypes} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Edit from "../../assets/edit.svg";
    import { user } from "../../store.js";
    import config from "../../config.js";
    export let deviceType;

    async function deleteDeviceType(id) {
        try {
            const response = await fetch(`${config.apiUrl}/api/deviceTypes/${id}`, {
                method: 'DELETE',
                credentials: 'include',
            });

            if (response.ok) {
                // Remove the system from the local array
                deviceTypes.update(currentDeviceTypes => { return currentDeviceTypes.filter(deviceType => deviceType.id !== id)});
            } else {
                console.error('Error deleting deviceType:', await response.text());
            }
        } catch (error) {
            console.error('Error deleting deviceType:', error);
        }
    }

    function MoveToDetail(deviceTypeId){
      navigate(`/deviceTypes/${deviceTypeId}`);
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-base text-gray-300">
        <p>{deviceType.name}</p>
    </td>
    <td class="py-4 px-0">
    </td>
    <td class="py-4 px-0">
        {#if $user && $user.role === "Admin"}
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>deleteDeviceType(deviceType.id)}>
        <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
      </button>
        {/if}
    </td>
</tr>