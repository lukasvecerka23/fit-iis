<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import QuestionMark from '../../../assets/question_mark.svg';
    import { Link, navigate } from "svelte-routing";

    export let id;

    let isLoading = true;
    let activeCard = 'parameters';
    let isSmallScreen = false;
    let systems = [];
    let deviceTypes = [];
    let showDescription = false;
    let device = null;
    let selectedSystemId = null;
    let selectedDeviceTypeId = null;


    //for parameter button description
    onMount(() => {
    const mediaQuery = window.matchMedia('(max-width: 1100px)');
    isSmallScreen = mediaQuery.matches;

    const updateScreenSize = () => {
        isSmallScreen = mediaQuery.matches;
    };

    mediaQuery.addListener(updateScreenSize);

    return () => {
        mediaQuery.removeListener(updateScreenSize);
    };
    });

    async function getSystems(){
        try {
            const response = await fetch(`https://localhost:7246/api/systems`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                systems = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
    }

    async function getDeviceTypes(){
        try {
            const response = await fetch(`https://localhost:7246/api/deviceTypes`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                deviceTypes = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
    }

    async function fetchDeviceDetail(){
            const resp = await fetch(`https://localhost:7246/api/devices/${id}`);
            if (resp.ok){
                device = await resp.json();
                isLoading = false;
            } else {
                throw new Error('Error')
            }
            selectedSystemId = device.systemId;
            selectedDeviceTypeId = device.deviceTypeId;
    }

    function toggleDescription() {
    showDescription = !showDescription;
    }

    onMount(() => {
        getSystems();
        getDeviceTypes();
        fetchDeviceDetail();
       
    })



  </script>

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
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <div class = "flex-row flex w-full items-center pt-10">
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">{device.userAlias}</h2>
                </div>
                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Uživatelský alias</label>
                        <img src="{QuestionMark}" alt="QuestionMark" class=" pl-1 h-5 w-5" on:blur={toggleDescription} on:mouseover={toggleDescription} on:focus={toggleDescription} on:mouseout={toggleDescription}>
                        {#if showDescription === true}
                        <div class="pl-2 pr-2  rounded-xl text-sm text-gray-600">
                            <p>Pod tímto názvem se bude zařízení zobrazovat v seznamu zařízení.</p>
                        </div>
                        {/if}
                    </div>
                    <input type="text" id="username" value="{device.userAlias}" class="w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Přidejte uživatelský alias..."/>
                </div>
                <div class="mb-4 w-1/3">
                    <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                    <textarea id="device-description" value="{device.description}" class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis zařízení..."></textarea>
                </div>
                <div class="font-semibold text-base w-1/3 mb-4">
                    <label for="deviceType" class="block mb-1 text-lg font-medium text-gray-700">Typ zařízení</label>
                    <select 
                        bind:value={selectedDeviceTypeId}
                        class="border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer" >
                        {#each deviceTypes as deviceType (deviceType.id)}
                            <option value={deviceType.id}>{deviceType.name}</option>
                        {/each}
                    </select>
                </div>
                <div class="font-semibold text-base w-1/3 mb-4">
                    <label for="system" class="block mb-1 text-lg font-medium text-gray-700">Systém</label>
                    <select 
                        bind:value={selectedSystemId}
                        class="border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer" >
                        <option value={null}>Žádný systém</option>
                        {#each systems as system (system.id)}
                            <option value={system.id}>{system.name}</option>
                        {/each}
                    </select>
                </div>
                <div class="flex  w-1/3 justify-end">
                    <button 
                        class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                        Uložit
                    </button>
                </div>

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  