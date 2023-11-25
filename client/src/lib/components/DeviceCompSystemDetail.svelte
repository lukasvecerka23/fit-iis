<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems} from "../../store.js";
    import Users from "../../assets/users.svg";
    import Devices from "../../assets/device.svg";
    import Edit from "../../assets/edit.svg";
    export let device;

    async function deleteDevice(id) {
        try {
            const response = await fetch(`https://localhost:7246/api/devices/${id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                // Remove the system from the local array
                systems.update(currentDevices => { return currentDevices.filter(device => device.id !== id)});
            } else {
                console.error('Error deleting system:', await response.text());
            }
        } catch (error) {
            console.error('Error deleting system:', error);
        }
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-base">{device.userAlias}</td>
    <td class="py-4 px-6">{device.deviceTypeName}</td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {device.creatorName}
        </div>
      </div>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>deleteDevice(device.id)}>
        <img src={Edit} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded">
        <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
</tr>