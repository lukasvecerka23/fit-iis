<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import Eye from '../../../assets/eye.svg';
    import EyeDark from '../../../assets/eye_dark.svg';
    import UserCompSystemDetail from '../../components/UserCompSystemDetail.svelte';
    import New from '../../../assets/new.svg';
    import Remove from '../../../assets/remove.svg';
    import Edit from '../../../assets/edit_black.svg';
    import Kpis from '../../../assets/kpis.svg';
    import KpisDark from '../../../assets/kpis_dark.svg';
    import ParametersCard from '../../components/ParametersCardDeviceDetail.svelte';
    import KpisCard from '../../components/KpisCardDeviceDetail.svelte';
    import QuestionMark from '../../../assets/question_mark.svg';

    let isLoading = true;
    let isSmallScreen = false;
    let devices = [];
    let showDescription = false;
    let selectedDevices = [];

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

    async function getDevices(){
        try {
            const response = await fetch(`https://localhost:7246/api/devices`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                devices = await response.json();
                devices = devices.filter(device => device.systemId == null);
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
        isLoading = false;
    }

    function toggleDescription() {
    showDescription = !showDescription;
    }

    function toggleDeviceSelection(deviceId) {
    if (selectedDevices.includes(deviceId)) {
      selectedDevices = selectedDevices.filter(id => id !== deviceId);
    } else {
      selectedDevices = [...selectedDevices, deviceId];
    }
  }

    onMount(getDevices);



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
            <div class="flex-col flex w-1/3 items-start">
                <div class = "flex-col flex w-full">
                    <div class = "flex-row flex w-full items-center pt-10">
                        <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">Nový systém</h2>
                    </div>
                    <div class="mb-4 w-full">
                        <div class="flex-row flex">
                            <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Název</label>
                        </div>
                        <input type="text" id="username" class="w-full px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700" placeholder="Zadejte název systému..."/>
                    </div>
                    <div class="mb-4 w-full">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                        <textarea id="device-description" class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis systému..."></textarea>
                    </div>
                    <div class="flex  w-full justify-end">
                        <button 
                            class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                            Vytvořit
                        </button>
                    </div>
    
                </div>
            </div>
            <div class="font-semibold text-base w-1/2 pl-20 mt-20 pt-10 mb-4">
                <h3 class="block mb-1 text-lg font-medium text-gray-700">Přidat zařízení</h3>
                {#if devices.length == 0}
                        <p class="font-normal text-gray-700 italic">Momentálně neexistují zařízení bez systému.</p>
                {:else}
                <ul class="w-full text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg dark:bg-slate-700 dark:border-gray-600 dark:text-white">
                    {#each devices as device (device.id)}
                    <li class="w-full border-b border-gray-200 rounded-t-lg dark:border-slate-500">
                        <div class="flex items-center ps-3">
                            <input on:change={() => toggleDeviceSelection(device.id)} id="device-checkbox-{device.id}" type="checkbox" value="" class="w-4 h-4 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-700 dark:focus:ring-offset-gray-700 focus:ring-2 dark:bg-slate-500 dark:border-gray-500">
                            <label for="device-checkbox-{device.id}" class="w-full py-3 ms-2 text-sm font-medium text-gray-900 dark:text-gray-300"
                            >{device.userAlias}</label>
                        </div>
                    </li>
                    {/each}               
                </ul>
                {/if} 
              </div>
        </div>
    
    </div>
  </div>
</div>
{/if}

  