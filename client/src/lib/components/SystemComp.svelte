<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems, reloadSystems} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../assets/users.svg";
    import Devices from "../../assets/device.svg";
    import Edit from "../../assets/edit.svg";
    import {user} from "../../store.js";
    import StatusOk from '../../assets/status_ok.svg';
    import StatusWarning from '../../assets/status_warning.svg';
    import StatusBad from '../../assets/status_bad.svg';
    import {SystemStatus, AssignStatus} from '../../utils.js';
    export let system;

    async function deleteSystem(id) {
        try {
            const response = await fetch(`https://localhost:7246/api/systems/${id}`, {
                method: 'DELETE',
                credentials: 'include',
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

    async function sendAssignment(systemId){
      const response = await fetch(`https://localhost:7246/api/assignToSystem`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          systemId: systemId,
          userId: $user.userId
        }),
        credentials: 'include',
      });
    }

    async function leaveSystem(systemId){
      const response = await fetch(`https://localhost:7246/api/systems/${systemId}/leave`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        credentials: 'include',
      });
    }

    async function handleAssign(assignStatus, systemId){
      if(assignStatus === AssignStatus.CanAssign){
        await sendAssignment(systemId);
      }
      else if(assignStatus === AssignStatus.Leave){
        await leaveSystem(systemId);
      }
      reloadSystems.set(true);
    }

    function MoveToDetail(systemId){
      navigate(`/systems/${systemId}`);   
    }

    function MoveToUpdate(systemId){
      navigate(`/systems/${systemId}/update`);
    }


  </script>

<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left text-base text-gray-300 font-semibold">
      <button class= "hover:cursor-pointer hover:underline" on:click={() => MoveToDetail(system.id)}>
        <p>{system.name}</p>
      </button>
    </td>
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
    {#if $user}
    <td class="py-4 px-0">
      <div class="flex-row flex justify-center">
        {#if system.status === SystemStatus.Okay}
        <div class="bg-green-600 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusOk} alt="StatusOk" class="" />
        </div>
      {:else if system.status === SystemStatus.Warning}
        <div class="bg-orange-400 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusWarning} alt="StatusWarning" class="" />
        </div>
      {:else if system.status === SystemStatus.Critical}
        <div class="bg-red-600 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
          <img src={StatusBad} alt="StatusBad" class="" />
        </div>
        {:else if system.status === SystemStatus.None}
          -
      {/if}
      </div>
    </td>
      <td class="py-4 px-0">
        {#if system.assignStatus === AssignStatus.CanAssign}
        <button class="bg-gray-500 hover:bg-gray-400 w-full text-white font-poppins-light py-2 px-4 rounded" on:click={() => handleAssign(system.assignStatus, system.id)}>
            Zažádat správce o přístup
        </button>
        {:else if system.assignStatus === AssignStatus.Processing}
        <button class="bg-orange-600 text-white font-poppins-light w-full py-2 px-4 rounded cursor-default">
            Čeká se na schválení...
        </button>
        {:else if system.assignStatus === AssignStatus.Leave}
        <button class="bg-red-600 hover:bg-red-500 text-white w-full font-poppins-light py-2 px-4 rounded" on:click={() => handleAssign(system.assignStatus, system.id)}>
            Odejít ze systému
        </button>
        {/if}
      </td>
      <td class="py-4 px-0">
        <button class="bg-transparent text-white font-semibold py-2 px-4 rounded disabled:hidden" on:click={()=>MoveToUpdate(system.id)}
          disabled={!($user.role === "Admin" || $user.userId === system.creatorId)}>
          <img src={Edit} alt="Trash Bin" class="w-6 h-6" />
        </button>
      </td>
      <td class="py-4 px-0">
        <button class="bg-transparent text-white font-semibold py-2 px-4 rounded disabled:hidden" on:click={()=>deleteSystem(system.id)}
          disabled={!($user.role === "Admin" || $user.userId === system.creatorId)}>
          <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
        </button>
      </td>
    {:else}
      <td class="py-4 px-0">-</td>
      <td class="py-4 px-0"/>
      <td class="py-4 px-0"/>
      <td class="py-4 px-0"/>
    {/if}
</tr>