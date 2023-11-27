<script>
    import AssignComp from './AssignComp.svelte';
    import {onMount} from 'svelte';
    export let systemId;

    let assigns = [];

    async function fetchAssigns() {
        const params = new URLSearchParams({
            systemId: systemId
        });

        const resp = await fetch(`https://localhost:7246/api/assignToSystem?${params}`);
        if (resp.ok){
            const data = await resp.json();
            assigns = data;
        }
    }

    onMount(() => {
        fetchAssigns();
    });
  </script>
  
<div class="w-full">
    <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
        <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
            <tr>
                <th scope="col" class="py-3 w-1/5 px-6 text-left">Uživatelské jméno</th>
                <th scope="col" class="py-3 w-1/5 px-6">Jméno a příjmení</th>
                <th scope="col" class="py-3 w-3/5 px-6"></th>
                <th scope="col" class="py-3 px-6"></th>
            </tr>
        </thead>
        <tbody>
            {#each assigns as assign (assign.id)}
            <AssignComp assign={assign} on:acceptAssign={() => fetchAssigns()}/>
            {/each}
        </tbody>
    </table>

</div>