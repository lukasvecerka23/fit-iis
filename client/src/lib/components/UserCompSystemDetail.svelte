<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems} from "../../store.js";
    import Users from "../../assets/users.svg";
    import Edit from "../../assets/edit.svg";
    export let user;

    /*async function deleteDevice(id) {
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
    }*/
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-base">{user.username}</td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded">
        <img src={Edit} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded">
        <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
</tr>