<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../assets/users.svg";
    import Devices from "../../assets/device.svg";
    import Edit from "../../assets/edit.svg";
    export let system;

    async function deleteSystem(id) {
        try {
            const response = await fetch(`https://localhost:7246/api/systems/${id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                // Remove the system from the local array
                systems.update(currentSystems => { return currentSystems.filter(system => system.id !== id)});
            } else {
                console.error('Error deleting system:', await response.text());
            }
        } catch (error) {
            console.error('Error deleting system:', error);
        }
    }

    function MoveToDetail(systemId){
      navigate(`/systems/${systemId}`);   
    }


  </script>

<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left text-base text-gray-300 font-semibold hover:cursor-pointer hover:underline" on:click={() => MoveToDetail(system.id)}>{system.name}</td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {system.devicesCount}
        </div>
        <div class="pl-3">
          <img src={Devices} alt="Users" class="w-5 h-5 mr-2">
        </div>
      </div>
    </td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {system.usersCount}
        </div>
        <div class="pl-3">
          <img src={Users} alt="Users" class="w-5 h-5 mr-2">
        </div>
      </div>
    </td>
    <td class="py-4 px-6">{system.creatorName}</td>
    <td class="py-4 px-0">
      <button class="bg-gray-500 hover:bg-gray-400 text-white font-poppins-light py-2 px-4 rounded">
          Zažádat správce o přístup
      </button>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>deleteSystem(system.id)}>
        <img src={Edit} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={()=>deleteSystem(system.id)}>
        <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
</tr>