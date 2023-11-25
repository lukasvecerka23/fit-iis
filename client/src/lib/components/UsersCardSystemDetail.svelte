<!-- UsersCard.svelte -->
<script>
  import UserCompSystemDetail from './UserCompSystemDetail.svelte';
  export let users;
</script>

<div class="w-full">
  <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
          <tr>
              <th scope="col" class="py-3 px-6 text-left">Jméno</th>
              <th scope="col" class="py-3 px-6"></th>
              <th scope="col" class="py-3 px-6"></th>
          </tr>
      </thead>
      <tbody>
          {#each users as user (user.id)}
          <UserCompSystemDetail user={user}/>
          {/each}
      </tbody>
  </table>

</div>
