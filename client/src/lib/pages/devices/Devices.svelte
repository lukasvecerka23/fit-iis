<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import DeviceComp from '../../components/DeviceComp.svelte';
    import {devices} from '../../../store.js';
    import New from '../../../assets/new.svg';
  
    let searchTerm = '';

  onMount(async () => {
    const resp = await fetch('https://localhost:7246/api/devices')
    devices.set(await resp.json())
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
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-6 font-poppins-light text-left">Zařízení</h2>
                <div class="flex flex-row">
                    <div class="pb-4 w-1/3 self-start">
                        <input
                        type="text"
                        class="p-2 border border-gray-300 rounded-xl w-full"
                        bind:value={searchTerm}
                        placeholder="Filtrovat zařízení..."
                        />
                    </div>
                    <div class="pb-4 ml-auto rounded-xl">
                        <button class="bg-slate-500 hover:bg-slate-300  text-white font-semibold py-2 px-4 rounded-xl">
                            <div class="flex flex-row">
                                <img src={New} alt="New" class="w-6 h-6 mr-2 font-poppins-light">
                                <span>Nové</span>
                            </div>
                        </button>
                    </div>
                </div>
            </div>
            <div class="w-full">
              <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="py-3 px-6 text-left">Název</th>
                        <th scope="col" class="py-3 px-6">Typ zařízení</th>
                        <th scope="col" class="py-3 px-6">Systém</th>
                        <th scope="col" class="py-3 px-6">Vytvořil</th>
                        <th scope="col" class="py-3 px-6"></th>
                        <th scope="col" class="py-3 px-6"></th>
                    </tr>
                </thead>
                <tbody>
                    {#each $devices as device (device.id)}
                      <DeviceComp device={device}/>
                    {/each}
                </tbody>
            </table>

            </div>
        </div>
    </div>
  </div>
</div>
  
