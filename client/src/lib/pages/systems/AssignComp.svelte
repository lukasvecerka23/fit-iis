<script>
    import { navigate } from "svelte-routing";
    import {createEventDispatcher} from "svelte";
    import {systemDetailActiveCard} from '../../../store.js';

    export let assign;

    const dispatch = createEventDispatcher();

    async function acceptAssign(){
        const resp = await fetch(`https://localhost:7246/api/assignToSystem/${assign.id}/accept`, 
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        });
        if (resp.ok){
            dispatch('acceptAssign', {id: assign.id});
            systemDetailActiveCard.set('users');
        }
    }

    async function rejectAssign(){
        const resp = await fetch(`https://localhost:7246/api/assignToSystem/${assign.id}`,
        {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        });
        if (resp.ok){
            systemDetailActiveCard.set('devices');
        }
    }

  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left text-base">{assign.userName}</td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {assign.userFullName}
        </div>
      </div>
    </td>
    <td class="py-4 px-0">
      <div class="flex-row flex justify-end">
        <div class="bg-green-600 text-white font-semibold py-2 px-4 rounded-3xl">
        <button on:click={async () => await acceptAssign()}>
            Příjmout
        </button>
        </div>
      </div>
    </td>
    <td class="py-4 px-4">
        <div class="flex-row flex justify-center">
            <div class="bg-red-600 text-white font-semibold py-2 px-2 rounded-3xl">
                <button on:click={async () => await rejectAssign()}>
                Odmítnout
                </button>
            </div>
        </div>
    </td>
</tr>