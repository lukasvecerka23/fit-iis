<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
  
    let devices = []
  
    onMount(async () => {
      const resp = await fetch('https://localhost:7246/api/devices')
      devices = await resp.json()
    })
</script>

<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <ul>
        {#each devices as device (device.id)}
            <li class="text-red-900">{device.userAlias}</li>
        {/each}
        </ul>
    </div>
  </div>
</div>
  
  