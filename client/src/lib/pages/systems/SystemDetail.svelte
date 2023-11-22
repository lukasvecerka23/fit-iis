<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import DeviceComp from '../../components/DeviceComp.svelte';
    import {devices} from '../../../store.js';
    import UsersCard from '../../components/UserCard.svelte';
    import DevicesCard from '../../components/DeviceCard.svelte';
  
    export let id;

    let searchTerm = '';

    let system = null;
    let isLoading = true;
    let activeCard = 'users';

    async function fetchSystemDetail() {
        try {
            const resp = await fetch(`https://localhost:7246/api/systems/${id}`);
            if (resp.ok){
                system = await resp.json();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }

    onMount(fetchSystemDetail);

  </script>
  <!-- Zde můžete provést další operace, například získání dat o systému pomocí systemId -->

{#if isLoading}
<div class="flex flex-col w-full h-screen bg-slate-400">
    <p>Loading...</p>
</div>
{:else}
<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <!-- Pole pro filtraci nad seznamem devices -->
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-4 font-poppins-light text-left">{system.name}</h2>
                <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{system.description}</h1>
                <div class="grid w-1/6 grid-cols-2 gap-2 rounded-3xl bg-gray-300 p-1">
                    <div>
                        <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={() => (activeCard = 'devices')}/>
                        <label for="1" class="{activeCard === 'devices' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">Zařízení</label>
                    </div>
            
                    <div>
                        <input type="radio" name="option" id="2" value="2" class="peer hidden" on:click={() => (activeCard = 'users')}/>
                        <label for="2" class="{activeCard === 'users' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400' } radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">Uživatelé</label>
                    </div>
                </div>
                <div class="pt-4">
                    {#if activeCard === 'devices'}
                    <DevicesCard devices={system.devices} />
                    {:else}
                    <UsersCard isActive={activeCard === 'users'} />
                    {/if}
                </div>
                <div class="flex flex-row">
                    <div class="pb-4 w-1/3 self-start">
                        <input
                        type="text"
                        class="p-2 border border-gray-300 rounded-xl w-full"
                        bind:value={searchTerm}
                        placeholder="Filtrovat systémy..."
                        />
                    </div>
                    <div class="pb-4 ml-auto rounded-xl">
                        <button class="bg-slate-500 hover:bg-slate-300  text-white font-semibold py-2 px-4 rounded-xl">
                            <div class="flex flex-row">
                                <span>Nový</span>
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
                    {#each system.devices as device (device.id)}
                      <DeviceComp device={device}/>
                    {/each}
                </tbody>
            </table>
            <!-- Pagination Controls
            <div class="flex justify-between items-center my-4">
                <button 
                    class="px-4 py-2 rounded-xl bg-slate-500 text-white disabled:text-gray-300" 
                    on:click={goToPage(currentPageIndex - 1)} 
                    disabled={currentPageIndex === 0}>
                    Zpět
                </button>
                <button 
                    class="px-4 py-2 rounded-xl bg-slate-500 text-white disabled:text-gray-300" 
                    on:click={goToPage(currentPageIndex + 1)} 
                    disabled={currentPageIndex === totalPages - 1}>
                    Další
                </button>
            </div> -->

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  