<script>
    import TrashBin from "../../assets/trash.svg";
    import {users} from "../../store.js";
    import { onMount } from 'svelte';
    import { createEventDispatcher } from "svelte";
    import config from "../../config.js";

    export let roles;
    export let user;

    const dispatch = createEventDispatcher();
    let selectedRoleId = user.roleId;

    async function deleteUser(id) {
        try {
            const response = await fetch(`${config.apiUrl}/api/users/${id}`, {
                method: 'DELETE',
                credentials: 'include',
            });

            if (response.ok) {
                // Remove the system from the local array
                users.update(currentUsers => { return currentUsers.filter(user => user.id !== id)});
            } else {
                console.error('Error deleting system:', await response.text());
            }
        } catch (error) {
            console.error('Error deleting system:', error);
        }
    }

    function onRoleChange() {
        dispatch('roleChange', { newId: selectedRoleId });
    }
</script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-base ">{user.username}</td>
    <td class="py-4 px-6 font-semibold text-base">{`${user.name} ${user.surname}`}</td>
    <td class="py-4 px-6 font-semibold text-base">
        <select 
        class="border border-gray-300 font-normal text-sm text-black rounded-lg p-1" 
        bind:value={selectedRoleId} on:change={onRoleChange}>
        {#each roles as role (role.id)}
            <option  class="font-normal text-" value={role.id}>{role.name}</option>
        {/each}
    </select>
    </td>
    <td class="py-4 px-0">
    </td>
    <td class="py-4 px-0">
      <button class="bg-transparent text-white font-semibold py-2 px-4 rounded" on:click={async () => await deleteUser(user.id)}>
        <img src={TrashBin} alt="Trash Bin" class="w-6 h-6" />
      </button>
    </td>
</tr>