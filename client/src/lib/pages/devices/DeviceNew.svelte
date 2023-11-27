<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import {user} from "../../../store";
    import { navigate, useLocation } from 'svelte-routing';
    import QuestionMark from '../../../assets/question_mark.svg';
    import config from "../../../config.js";

    let isLoading = true;
    let activeCard = 'parameters';
    let isSmallScreen = false;
    let systems = [];
    let deviceTypes = [];
    let errorId = false;
    let showDescription = false;
    const device = {
        userId: null,
        userAlias: null,
        description: null,
        systemId: null,
        deviceTypeId: null,
        creatorId: null
    }
    let userAliasNull = false;
    let deviceTypeIdNull = false;

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
                systems = systems.filter(system => system.creatorId === $user.userId || $user.role === "Admin");
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
        isLoading = false;
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
        isLoading = false;
    }

    function toggleDescription() {
    showDescription = !showDescription;
    }

    function MoveToList(){
      navigate(`/devices/`);
    }

    function checkMandatoryFields()
    {
        if (device.userId === null || device.userId === "" || device.deviceTypeId === null)
        {
            if(device.userId === null || device.userId === "")
            {
                userAliasNull=true;
            }
            if (device.deviceTypeId === null)
            {
                deviceTypeIdNull = true;
            }
            return false;
        }
        return true;
    }

    async function createDevice(){
        if (checkMandatoryFields() === false)
        {
            return;
        }
        if (device.userAlias === null || device.userAlias === "")
        {
            device.userAlias = "Bez názvu";
        }

        device.creatorId = $user.userId;
            const url = `${config.apiUrl}/api/devices`;

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(device),
                credentials: 'include',
            });

            if (!response.ok) {
                errorId = true;
                return;
            }

            const result = await response.json();
            console.log('Device created successfully:', result);

            // You can handle the response or perform additional actions here
        } catch (error) {
            console.error('Error creating device:', error.message);
            // You can handle errors or show an error message to the user
        }
        MoveToList();
    }

    onMount(getSystems);
    onMount(getDeviceTypes);



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
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">Nové zařízení</h2>
                </div>

                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Identifikátor *</label>
                    </div>
                    <input 
                    bind:value={device.userId}
                    required type="text" id="username" class={`w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700 ${userAliasNull || errorId ? 'border-red-500 border-2' : ''}`} placeholder="Přidejte uživatelský alias..."/>
                </div>
                {#if errorId}
                    <p class="text-red-500">Zařízení s tímto identifikátorem již existuje.</p>
                {/if}
                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Uživatelský alias</label>
                        <img src="{QuestionMark}" alt="QuestionMark" class="pl-1 h-5 w-5" on:blur={toggleDescription} on:mouseover={toggleDescription} on:focus={toggleDescription} on:mouseout={toggleDescription}>
                        {#if showDescription === true}
                        <div class="pl-2 pr-2  rounded-xl text-sm text-gray-600">
                            <p>Pod tímto názvem se bude zařízení zobrazovat v seznamu zařízení.</p>
                        </div>
                        {/if}
                    </div>
                    <input 
                    bind:value={device.userAlias}
                    required type="text" id="username" class={`w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700`} placeholder="Přidejte uživatelský alias..."/>
                </div>
                <div class="mb-4 w-1/3">
                    <label for="device-description" class="block mb-1 text-lg font-medium text-gray-700">Popis</label>
                    <textarea bind:value={device.description} id="device-description" class="border border-gray-300 rounded-xl p-2 w-full h-40 resize-none" placeholder="Přidejte popis zařízení..."></textarea>
                </div>
                <div class="font-semibold text-base w-1/3 mb-4">
                    <label for="deviceType" class="block mb-1 text-lg font-medium text-gray-700">Typ zařízení *</label>
                    <select 
                        bind:value={device.deviceTypeId}
                        required
                        class={`border border-gray-300 rounded-xl p-2 w-full hover:cursor-pointer ${deviceTypeIdNull ? 'border-red-500 border-2' : ''}`} >
                        <option value={null}>Vyberte typ zařízení</option>
                        {#each deviceTypes as deviceType (deviceType.id)}
                            <option value={deviceType.id}>{deviceType.name}</option>
                        {/each}
                    </select>
                </div>
                <div class="font-semibold text-base w-1/3 mb-4">
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
                <div class="flex  w-1/3 justify-end">
                    <button 
                        on:click={() => createDevice()}
                        type="submit"
                        class="px-10 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white">
                        Vytvořit
                    </button>
                </div>

            </div>
        </div>
    </div>
  </div>
</div>
{/if}

  