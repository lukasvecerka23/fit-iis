<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import QuestionMark from '../../../assets/question_mark.svg';
    import New from '../../../assets/new.svg';
    import { Link, navigate } from "svelte-routing";
    import {KpiFunction, mapKpiFunctionToString} from "../../../utils"

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
    const parameter = {
        parameterId: null,
        function: null,
        value: null,
    };
    let parameters = [];
    let kpis = [];


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
                selectedSystemId = device.systemId;
                selectedDeviceTypeId = device.deviceTypeId;
                await fetchParameters();
            } else {
                throw new Error('Error')
            }
    }

    async function fetchParameters(){
            const resp = await fetch(`https://localhost:7246/api/deviceTypes/${selectedDeviceTypeId}`);
            if (resp.ok){
                const data = await resp.json();
                parameters = data.parameters;
            } else {
                throw new Error('Error')
            }
    }

    function toggleDescription() {
    showDescription = !showDescription;
    }

    function addKpi(parameterId, functionType, value) {
    const newKpi = {
        parameterId,
        functionType,
        value
    };
    kpis = [...kpis, newKpi];
    console.log(kpis);
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
        <div class="flex-row flex w-4/5 items-start">
            <div class="flex-col flex w-full justify-start">
                <div class = "w-full items-center pt-10">
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">{device.userAlias}</h2>
                </div>
                <div class="flex-row w-full flex items-start">
                    <div class = "flex-col flex w-1/3 justify-start">
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
                            <input type="text" id="username" value="{device.userAlias}" class="w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Přidejte uživatelský alias..."/>
                        </div>
                        <div class="mb-4 w-full">
                            <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                            <textarea id="device-description" value="{device.description}" class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis zařízení..."></textarea>
                        </div>
                        <div class="font-semibold text-base w-full mb-4">
                            <label for="deviceType" class="block mb-1 text-lg font-medium text-gray-700">Typ zařízení</label>
                            <select 
                                bind:value={selectedDeviceTypeId}
                                on:change={() => fetchParameters()}
                                class="border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer" >
                                {#each deviceTypes as deviceType (deviceType.id)}
                                    <option value={deviceType.id}>{deviceType.name}</option>
                                {/each}
                            </select>
                        </div>
                        <div class="font-semibold text-base w-full mb-4">
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
                        <div class="flex  w-full justify-end">
                            <button 
                                class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                                Uložit
                            </button>
                        </div>
                    </div>
                    <div class="w-2/3 flex-col flex justify-start">
                        <div class="mb-4 w-full ml-10">
                            <label for="parameters" class="block w-full  mb-1 text-lg font-medium text-gray-700">Klíčové identifikátory výkonu</label>
                            {#each kpis as kpi}
                            <div class="pb-2 flex-row flex items-center">
                                <label for="deviceType" class="block mb-1 text-base font-medium text-gray-700">Parameter:</label>
                                <div class="font-semibold text-base pl-2">
                                    <select bind:value={kpi.parameterId}
                                        class="border border-gray-300 rounded-xl p-2 hover:cursor-pointer" >
                                        {#each parameters as parameter (parameter.id)}
                                            <option value={parameter.id}>{parameter.name}</option>
                                        {/each}
                                    </select>
                                </div>
                                <label for="min" class=" px-3 block mb-1 text-base font-medium text-gray-700">Funkce:</label>
                                <div class="font-semibold text-base pl-2">
                                    <select bind:value={kpi.functionType}
                                        class="border border-gray-300 rounded-xl p-2 hover:cursor-pointer" >
                                        {#each Object.keys(KpiFunction) as key (key)}
                                        <option value={KpiFunction[key]}>{mapKpiFunctionToString(KpiFunction[key])}</option>
                                        {/each}
                                    </select>
                                </div>
                                <label for="min" class=" px-3 block mb-1 text-base font-medium text-gray-700">Hodnota:</label>
                                <input bind:value={kpi.value} type="number" required class="border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer" />
                            </div>
                            {/each}
                            <div class="flex  w-1/3 justify-start">
                                <button 
                                    on:click={() => addKpi(null, null, null)}
                                    class="px-2 py-2 rounded-3xl bg-slate-500 hover:bg-slate-700 text-white">
                                    <img src={New} alt="New" class="h-5 w-5"/>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  