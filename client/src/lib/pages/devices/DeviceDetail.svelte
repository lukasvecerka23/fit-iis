<!-- SystemDetail.svelte -->

<script>
    import { onMount, onDestroy } from 'svelte';
    import { navigate} from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import Eye from '../../../assets/eye.svg';
    import EyeDark from '../../../assets/eye_dark.svg';
    import UserCompSystemDetail from '../../components/UserCompSystemDetail.svelte';
    import {selectedParameterId, user} from '../../../store';
    import New from '../../../assets/new.svg';
    import Remove from '../../../assets/remove.svg';
    import Edit from '../../../assets/edit_black.svg';
    import Kpis from '../../../assets/kpis.svg';
    import KpisDark from '../../../assets/kpis_dark.svg';
    import Measurement from '../../../assets/measurement.svg';
    import MeasurementDark from '../../../assets/measurement_dark.svg';
    import ParametersCard from '../../components/ParametersCardDeviceDetail.svelte';
    import KpisCard from '../../components/KpisCardDeviceDetail.svelte';
    import MeasurementsCard from '../../components/MeasurementsCardDeviceDetail.svelte';
    import RemoveChoice from '../../../assets/status_bad.svg';
    import TrashBin from '../../../assets/trash.svg';
    import config from '../../../config.js';
  
    export let id;

    let intervalId;
    let device = null;
    let isLoading = true;
    let activeCard = 'kpi';
    let isSmallScreen = false;
    let parameters = {};
    let isPopupOpen = false;
    let systems = [];
    let selectedSystem = null;

    function openPopup() {
        getSystems();
        isPopupOpen = true;
    }

    function closePopup() {
    isPopupOpen = false;
    }

    function deleteSystem() {
    // Add logic for deleting the system here
    closePopup();
    }

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
        isLoading = false;
    }

    async function updateSystem(){
        device.systemId =selectedSystem;
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
        closePopup();

    }

    function removeSystem(){
        selectedSystem = null;
        updateSystem();
    }

    async function deleteDevice() {
        // Add logic for deleting the system here
        try {
            const response = await fetch(`${config.apiUrl}/api/devices/${id}`, {
                method: 'DELETE',
                credentials: 'include',
            });

            if (response.ok) {
            } else {
                console.error('Error deleting device:', await response.text());
            }
        } catch (error) {
            console.error('Error deleting device:', error);
        }
        MoveToList();
    }

    async function fetchDeviceDetail() {
        try {
            const resp = await fetch(`${config.apiUrl}/api/devices/${id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
            });
            if (resp.ok){
                device = await resp.json();
                await fetchParameters();
            } else {
                throw new Error('Error')
            }
        } catch (err){
            console.log('Error')
        } finally {
            isLoading = false;
        }
    }

    async function fetchParameters(){
        
        const params = new URLSearchParams({
            deviceTypeId: device.deviceTypeId,
            deviceId: device.id,
        });
        const resp = await fetch(`${config.apiUrl}/api/parameters/status?${params}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
        })
        if (resp.ok){
            parameters = await resp.json();
        }
    }

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

    onMount( () => {
        selectedParameterId.set(null);
        fetchDeviceDetail()

        intervalId = setInterval(fetchParameters, 5000);

    });

    onDestroy( () => {
        clearInterval(intervalId);
    })

    function switchCard(card) {
        activeCard = card;
    }

    function MoveToUpdate(){
      navigate(`/devices/${id}/update`);
    }

    function MoveToList(){
        navigate(`/devices/`);
    }

    function SetChoiceNull(){
        selectedParameterId.set(null);
    }


</script>

{#if isPopupOpen}
    <div class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
      <div class="bg-white p-8 rounded-md text-center">
        <p class="mb-4">Přidat zařízení do systému</p>
        <select 
            bind:value={selectedSystem}
            class="border border-gray-300 rounded-xl p-2 mb-2 w-full hover:cursor-pointer" >
            <option value={null}>Žádný systém</option>
            {#each systems as system (system.id)}
                <option value={system.id}>{system.name}</option>
            {/each}
        </select>
        <button on:click={closePopup} class="bg-gray-300 text-gray-700 px-4 py-2 rounded">Zrušit</button>
        <button on:click={updateSystem} class="bg-green-600 text-white px-4 py-2 rounded mr-2">Uložit</button>
      </div>
    </div>
{/if}

<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    {#if !isLoading}
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <div class = "flex-row flex w-full items-center pt-10">
                    <h2 class="text-3xl font-bold font-poppins-light text-left">{device.userAlias}</h2>
                    <div class="">
                        <div class="pl-5">
                            <button class=" hover:bg-slate-200 p-1  text-white font-medium rounded-3xl disabled:hidden"
                                disabled={!($user.role === "Admin" || $user.userId === device.creatorId)}
                                on:click={() => MoveToUpdate()}>
                                <img src={Edit} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                    </div>
                    <div class="">
                        <div class="pl-5">
                            <button class=" hover:bg-slate-200 p-1   text-white font-medium rounded-3xl disabled:hidden" 
                                disabled={!($user.role === "Admin" || $user.userId === device.creatorId)}
                                on:click={() => deleteDevice()}>
                                <img src={TrashBin} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                    </div>
                </div>
                <h1 class=" text-lg font-medium text-gray-800 font-poppins text-left">{device.userId}</h1>
                {#if device.description !== null}
                    <h1 class=" text-lg font-medium text-gray-700 pb-10 font-poppins-light text-left">{device.description}</h1>
                {:else}
                    <h1 class=" text-lg font-medium italic text-gray-700 pb-10 font-poppins-light text-left">Popis není k dispozici.</h1>
                {/if}
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
                        <div class="pl-2 rounded-xl">
                            <button on:click={() => removeSystem()} class=" bg-slate-300 hover:bg-slate-200  text-white font-medium rounded-3xl disabled:hidden"
                                disabled={!($user.role === "Admin" || $user.userId === device.creatorId)}>                                
                                <img src={Remove} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                        {:else}
                            {#if ($user.role === "Admin" || $user.userId === device.creatorId)}
                            <div class="rounded-xl pl-2">
                                <button on:click={() => openPopup()} class="bg-slate-500 hover:bg-slate-300  text-white font-small text-sm py-2 px-2 rounded-2xl">
                                    <div class="flex flex-row">
                                        <img src={New} alt="New" class="w-4 h-4 pt-1  font-poppins-light">
                                        <span class="pr-1">Přidat do systému</span>
                                    </div>
                                </button>
                            </div>
                            {:else}
                            <p class="text-gray-700 italic pl-2">Zařízení momentálně není v žádném systému.</p>
                            {/if}
                        {/if}
                    </div>
                <div class="flex-row flex w-full items-center pt-6 pb-1">
                        <img src={EyeDark} alt="EyeDark" class="w-8 h-8" />
                        <p class="pl-2 text-xl">Parametry</p>
                        {#if $selectedParameterId !== null}
                        <div class=" ml-auto rounded-xl">
                            <button class="bg-slate-500 hover:bg-slate-600  text-white font-normal text-sm py-1  px-2 rounded-xl" on:click={() => SetChoiceNull()}>
                                <div class="flex flex-row">
                                    <img src={RemoveChoice} alt="New" class="w-5 h-5 mr-2 font-poppins-light">
                                    <span>Zrušit výběr</span>
                                </div>
                            </button>
                        </div>
                        {/if}
                </div>
                <div class="py-4">
                    <ParametersCard parameters={parameters} />
                </div>
                <!-- <div class="flex-row flex w-full items-center pt-4">
                    <img src={KpisDark} alt="KpisDark" class="w-8 h-8" />
                    <p class="pl-2 text-xl">Klíčové identifikátory výkonu</p>
                </div> -->
                <div class="flex-row items-start w-1/3">
                    <div class="grid w-full grid-cols-2 gap-2 rounded-3xl bg-gray-300 p-1">
                        <div>
                            <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={async () => await switchCard('kpi')}/>
                            <label for="1" class="{activeCard === 'kpi' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                                <div class="flex-row flex justify-center">
                                    {#if activeCard === 'kpi'}
                                    <img src={Kpis} alt="Device" class="w-6 h-6" />
                                    {:else}
                                    <img src={KpisDark} alt="DeviceDark" class="w-6 h-6" />
                                    {/if}
                                    {#if !isSmallScreen}
                                        <p class="pl-2">KPI</p>
                                    {/if}
                                </div>
                            </label>
                        </div>
                
                        <div>
                            <input type="radio" name="option" id="2" value="2" class="peer hidden" on:click={() => switchCard('measurements')}/>
                            <label for="2" class="{activeCard === 'measurements' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400' } radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                                <div class="flex-row flex justify-center">
                                    {#if activeCard === 'measurements'}
                                    <img src={Measurement} alt="Measurements" class="w-6 h-6" />
                                    {:else}
                                    <img src={MeasurementDark} alt="MeasurementsDark" class="w-6 h-6" />
                                    {/if}
                                    {#if !isSmallScreen}
                                        <p class="pl-2">Měření</p>
                                    {/if}
                                </div>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="pt-4">
                    {#if activeCard === 'kpi'}
                        <KpisCard deviceId={device.id}/>
                    {:else if activeCard === 'measurements'}
                        <MeasurementsCard deviceId={device.id}/>
                    {/if}

 

                </div>
            </div>
        </div>
    </div>
    {/if}
  </div>
</div>


  