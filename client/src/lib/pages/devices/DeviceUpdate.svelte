<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import QuestionMark from '../../../assets/question_mark.svg';
    import New from '../../../assets/new.svg';
    import {user} from "../../../store";
    import { Link, navigate } from "svelte-routing";
    import {KpiFunction, mapKpiFunctionToString} from "../../../utils"
    import ParameterCompDeviceDetail from '../../components/ParameterCompDeviceDetail.svelte';
    import TrashBin from '../../../assets/trash.svg';
    import config from '../../../config.js';

    export let id;

    let isLoading = true;
    let activeCard = 'parameters';
    let isSmallScreen = false;
    let systems = [];
    let deviceTypes = [];
    let existingKpis = [];
    let showDescription = false;
    let data = null;
    const parameter = {
        parameterId: null,
        function: null,
        value: null,
    };
    const device = {
        userAlias: null,
        description: null,
        systemId: null,
        deviceTypeId: null,
        creatorId: null
    }
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
            const response = await fetch(`${config.apiUrl}/api/systems`, {
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
            const response = await fetch(`${config.apiUrl}/api/deviceTypes`, {
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
            const resp = await fetch(`${config.apiUrl}/api/devices/${id}`, {
                method: 'GET',
                credentials: 'include',
            });
            if (resp.ok){
                data = await resp.json();
                device.creatorId = data.creatorId;
                device.description = data.description;
                device.deviceTypeId = data.deviceTypeId;
                device.systemId = data.systemId;
                device.userAlias = data.userAlias;
                isLoading = false;
                await fetchParameters();
            } else {
                throw new Error('Error')
            }
    }

    async function fetchParameters(){
            const resp = await fetch(`${config.apiUrl}/api/deviceTypes/${device.deviceTypeId}`, {
                method: 'GET',
                credentials: 'include',
            });
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
        function: functionType,
        error: false,
        deviceId: id,
        parameterId,
        creatorId: $user.userId,
        value
    };
    kpis = [...kpis, newKpi];
  }

  async function deleteAllKpis()
  {
    const urlBase = '${config.apiUrl}/api/kpis';

    try {
        const requests = existingKpis.map(async (kpi) => {
        const url = `${urlBase}/${kpi.id}`;

        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
            'Content-Type': 'application/json',
            credentials: 'include',
            },
        });

        if (!response.ok) {
            throw new Error(`Failed to delete KPI with ID ${kpi.id}`);
        }

        console.log(`KPI with ID ${kpi.id} deleted successfully`);
        });

        await Promise.all(requests); // Wait for all requests to complete

    } catch (error) {
        console.error('Error deleting existing KPIs:', error.message);
    }
  }

  async function createKpis()
  {
    await deleteAllKpis();
    const url = `${config.apiUrl}/api/kpis`;

    try {
        const requests = kpis.map(async (kpi) => {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify(kpi),
            credentials: 'include',
        });

        if (!response.ok) {
            throw new Error(`Failed to update KPI.`);
        }

        const result = await response.json();
        console.log(`KPI updated successfully:`, result);
        });

        await Promise.all(requests); // Wait for all requests to complete

    } catch (error) {
        console.error('Error updating KPIs:', error.message);
    }
  }

  function moveToDetail()
  {
    navigate(`/devices/${id}`);
  }

  async function updateDevice()
  {
    const url = `${config.apiUrl}/api/devices/${id}`;

    try {
        const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(device),
        credentials: 'include',
        });

        if (!response.ok) {
        throw new Error(`Failed to update device with ID ${id}`);
        }

        const result = await response.json();
        console.log(`Device with ID ${id} updated successfully:`, result);

    } catch (error) {
        console.error(`Error updating device with ID ${id}:`, error.message);
    }

    createKpis();
    moveToDetail();
  }

    function removeKpi(index)
    {
        kpis = [...kpis.slice(0, index), ...kpis.slice(index + 1)];
    }

    async function fetchExistingKpis() {
        const params = new URLSearchParams({
            deviceId: id
        });
        const resp = await fetch(`${config.apiUrl}/api/kpis/search?${params}`, {
            method: 'GET',
            credentials: 'include',
        });
        if (resp.ok){
            const data = await resp.json();
            existingKpis = data.kpis;
            kpis = existingKpis;
            isLoading = false;
        }
    }

    onMount(() => {
        getSystems();
        getDeviceTypes();
        fetchDeviceDetail();
        fetchExistingKpis();
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
                                <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Uživatelský alias *</label>
                            </div>
                            <input type="text" id="username" bind:value={device.userAlias} class="w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Přidejte uživatelský alias..."/>
                        </div>
                        <div class="mb-4 w-full">
                            <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                            <textarea id="device-description" bind:value={device.description} class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis zařízení..."></textarea>
                        </div>
                        <div class="font-semibold text-base w-full mb-4">
                            <label for="deviceType" class="block mb-1 text-lg font-medium text-gray-700">Typ zařízení *</label>
                            <select 
                                bind:value={device.deviceTypeId}
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
                                bind:value={device.systemId}
                                class="border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer" >
                                <option value={null}>Žádný systém</option>
                                {#each systems as system (system.id)}
                                    <option value={system.id}>{system.name}</option>
                                {/each}
                            </select>
                        </div>
                        <div class="flex  w-full justify-end">
                            <button 
                                on:click={() => updateDevice()}
                                class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                                Uložit
                            </button>
                        </div>
                    </div>
                    <div class="w-2/3 flex-col flex justify-start">
                        <div class="mb-4 w-full ml-10">
                            <label for="parameters" class="block w-full  mb-1 text-lg font-medium text-gray-700">Klíčové identifikátory výkonu</label>
                            {#each kpis as kpi, index}
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
                                    <select bind:value={kpi.function}
                                        class="border border-gray-300 rounded-xl p-2 hover:cursor-pointer" >
                                        {#each Object.keys(KpiFunction) as key (key)}
                                        <option value={KpiFunction[key]}>{mapKpiFunctionToString(KpiFunction[key])}</option>
                                        {/each}
                                    </select>
                                </div>
                                <label for="min" class=" px-3 block mb-1 text-base font-medium text-gray-700">Hodnota:</label>
                                <input bind:value={kpi.value} type="number" required class="border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer" />
                                <button
                                on:click={() => removeKpi(index)}
                                    class="px-2 ml-2 py-2 rounded-3xl bg-slate-500 hover:bg-slate-700 text-white">
                                    <img src={TrashBin} alt="New" class="h-5 w-5"/>
                                </button>
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

  