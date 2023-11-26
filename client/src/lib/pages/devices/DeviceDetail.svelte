<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import Eye from '../../../assets/eye.svg';
    import EyeDark from '../../../assets/eye_dark.svg';
    import UserCompSystemDetail from '../../components/UserCompSystemDetail.svelte';
    import { systems } from '../../../store';
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
  
    export let id;

    let device = null;
    let isLoading = true;
    let activeCard = 'kpi';
    let isSmallScreen = false;

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

    onMount(fetchDeviceDetail);

    function switchCard(card) {
        activeCard = card;
    }

    function MoveToUpdate(){
      navigate(`/devices/${id}/update`);
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
                    <div class="">
                        <div class="pl-5">
                            <button class=" hover:bg-slate-200 p-1  text-white font-medium rounded-3xl" on:click={() => MoveToUpdate()}>
                                <img src={Edit} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                    </div>
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
                        <div class="pl-2 rounded-xl">
                            <button class=" bg-slate-300 hover:bg-slate-200  text-white font-medium rounded-3xl">
                                <img src={Remove} alt="New" class="w-6 h-6 font-poppins-light">
                            </button>
                        </div>
                        {:else}
                        <div class="rounded-xl pl-2">
                            <button class="bg-slate-500 hover:bg-slate-300  text-white font-small text-sm py-2 px-2 rounded-2xl">
                                <div class="flex flex-row">
                                    <img src={New} alt="New" class="w-4 h-4 pt-1  font-poppins-light">
                                    <span class="pr-1">Přidat do systému</span>
                                </div>
                            </button>
                        </div>
                        {/if}
                    </div>
                <div class="flex-row flex w-full items-center pt-6">
                        <img src={EyeDark} alt="EyeDark" class="w-8 h-8" />
                        <p class="pl-2 text-xl">Parametry</p>
                </div>
                <div class="py-4">
                    <ParametersCard parameters={device.parameters} />
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
  </div>
</div>
{/if}

  