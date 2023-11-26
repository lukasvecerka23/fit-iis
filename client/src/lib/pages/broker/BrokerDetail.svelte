<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import {selectedParameterId} from '../../../store.js';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import New from '../../../assets/new.svg';
    import Eye from '../../../assets/eye.svg';
    import EyeDark from '../../../assets/eye_dark.svg';
    import MeasurementDark from '../../../assets/measurement_dark.svg';
    import ParametersCard from './ParametersCardBroker.svelte';
    import MeasurementsCard from '../../components/MeasurementsCardDeviceDetail.svelte';
  
    export let id;

    let device = null;
    let isLoading = true;
    let isSmallScreen = false;
    let selectedParameter = {};
    let measurementValue = 0;

    async function fetchDeviceDetail() {
        try {
            const resp = await fetch(`https://localhost:7246/api/devices/${id}`);
            if (resp.ok){
                device = await resp.json();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }

    onMount(() => {
        selectedParameterId.set(null);
        fetchDeviceDetail()
    });

    async function createMeasurement() {
        const resp = await fetch(`https://localhost:7246/api/measurements`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                deviceId: id,
                parameterId: selectedParameter.id,
                value: measurementValue,
                timestamp: new Date().toISOString()
            }),
            credentials: 'include'
        })
        if (resp.ok) {
            console.log('Measurement created')
            isLoading = true;
            await fetchDeviceDetail()
            selectedParameter = {};
            measurementValue = 0;
        }
    }
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
                    <h2 class="text-3xl font-bold font-poppins-light text-left">{device.userAlias}</h2>
                </div>
                <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{device.description}</h1>
                <div class="flex-row flex pb-2">
                    <h1 class=" text-lg text-black font-poppins-light text-left font-semibold">Vytvořil:</h1>
                    <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">{device.creatorName}</h1>
                </div>
                <div class="flex-row flex pb-2">
                    <h1 class=" text-lg font-semibold text-black font-poppins-light text-left">Typ zařízení:</h1>
                    <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">{device.deviceTypeName}</h1>
                </div>
                    <div class="flex-row flex pb-4 items-center">
                        <h1 class=" text-lg font-semibold text-black font-poppins-light text-left">V systému:</h1>
                        {#if device.systemId != undefined}
                            <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">{device.systemName}</h1>
                        {:else}
                        <h1 class=" pl-2 text-lg font-medium text-gray-700 font-poppins-light text-left">-</h1>
                        {/if}
                    </div>
                <div class="flex-row flex w-full items-center pt-6">
                        <img src={EyeDark} alt="EyeDark" class="w-8 h-8" />
                        <p class="pl-2 text-xl">Parametry</p>
                </div>
                <div class="py-4">
                    <ParametersCard parameters={device.parameters} />
                </div>

                <div class="flex-row flex w-full items-center pt-6">
                        <img src={MeasurementDark} alt="KpisDark" class="w-8 h-8" />
                        <p class="pl-2 text-xl">Měření</p>
                </div>

                <div class="pt-4">
                    <form on:submit|preventDefault={createMeasurement} class="flex py-4 justify-start w-1/2">
                        <div class="mr-4 w-1/4">
                            <label class="block text-gray-700 text-sm font-bold mb-2" for="parameter">
                                Parametr
                            </label>
                            <select bind:value={selectedParameter} class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" required>
                                {#each device.parameters as parameter (parameter.id)}
                                    <option value={parameter}>{parameter.name}</option>
                                {/each}
                            </select>
                        </div>
                        <div class="mr-4 w-1/4">
                            <label class="block text-gray-700 text-sm font-bold mb-2" for="value">
                                Hodnota
                            </label>
                            <input type="number" min={selectedParameter.lowerLimit} max={selectedParameter.upperLimit} bind:value={measurementValue} required class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
                        </div>
                        <div class="flex justify-center items-end">
                            <button type="submit" class="w-1/2 h-1/2 rounded-full bg-gray-500 hover:bg-gray-700 text-white font-bold p-5 focus:outline-none focus:shadow-outline flex justify-center items-center">
                                <img src={New} alt="Eye" class="w-20" />
                            </button>
                        </div>
                    </form>
                    <MeasurementsCard deviceId={device.id}/>
                </div>
            </div>
        </div>
    </div>
  </div>
</div>
{/if}