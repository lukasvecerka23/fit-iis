<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
  
    let devices = []
  
    onMount(async () => {
      const resp = await fetch('https://localhost:7246/api/devices')
      devices = await resp.json()
    })
  </script>
  
  <div class="h-full w-full min-h-screen bg-slate-400 flex justify-center">
    <Sidebar />
    <div class="h-full w-full min-h-screen bg-primary-white flex justify-center">
      <ul>
        {#each devices as device (device.id)}
          <li class="text-red-900">{device.userAlias}</li>
        {/each}
      </ul>
    </div>
  </div>
  
  