<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import SystemComp from '../../components/SystemComp.svelte'
  
    let systems = [];
    let searchTerm = '';
  
    onMount(async () => {
      const resp = await fetch('https://localhost:7246/api/systems')
      systems = await resp.json()
    })
</script>

<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <!-- Pole pro filtraci nad seznamem devices -->
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <h2 class="text-3xl font-bold mb-0 py-6 text-left">Systémy</h2>
                <div class="pb-4 w-1/3 self-start">
                    <input
                    type="text"
                    class="p-2 border border-gray-300 rounded w-full"
                    bind:value={searchTerm}
                    placeholder="Filtrovat systémy..."
                    />
                </div>
            </div>
            <div class="w-full">
                {#each systems as system (system.id)}
                    <SystemComp
                    name={system.name}
                    description={system.description}
                    creatorName={system.creatorName}
                    />
                {/each}

            </div>
        </div>
    </div>
  </div>
</div>
  
  