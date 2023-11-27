<script>
    import Remove from "../../assets/remove.svg";
    import {systems, user, reloadSystemDetail} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../assets/users.svg";
    import Edit from "../../assets/edit.svg";
    export let userDetail;
    export let system;

    function canEdit(){
      return $user && ($user.role === 'Admin' || $user.userId === userDetail.userId || $user.userId === system.creatorId) && userDetail.userId !== system.creatorId;
    }

    async function removeUserFromSystem(){
      const resp = await fetch(`https://localhost:7246/api/userInSystems/${userDetail.id}`, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json'
        },
        credentials: 'include',
      });
      if (resp.ok){
        reloadSystemDetail.set(true);
      }
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-gray-300 text-base">
        <p>{userDetail.userFullname}</p>
    </td>
    <td class="py-4 px-0">
      {#if canEdit()}
        <button class="bg-transparent text-white font-semibold py-2 px-4 rounded"
        on:click={removeUserFromSystem}>
          <img src={Remove} alt="Remove" class="w-6 h-6" />
        </button>
      {/if}
    </td>
</tr>