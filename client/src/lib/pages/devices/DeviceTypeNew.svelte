<!-- SystemDetail.svelte -->

<script>
    import { onMount } from 'svelte';
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import { navigate, useLocation } from 'svelte-routing';
    import New from '../../../assets/new.svg';
    import QuestionMark from '../../../assets/question_mark.svg';
    import TrashBin from '../../../assets/trash.svg';
    import config from "../../../config.js";

    let isLoading = true;
    let isSmallScreen = false;
    let systems = [];
    let deviceTypes = [];
    let parameters = [];
    let measurementValue = null;
    let deviceTypeId = null;
    let showDescription = false;
    const deviceType = {
        name: null
    };
    let deviceTypeNull = false;
    let checkDone = false;

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

    function addParameter() {
        const parameter = {
            name: null,
            lowerLimit: null,
            upperLimit: null,
        }
        parameters = [...parameters, parameter];
        console.log(parameters);
    }

    function removeParameter(index)
    {
        parameters = [...parameters.slice(0, index), ...parameters.slice(index + 1)];
    }

    function checkMandatoryFields()
    {
        if (deviceType.name === null || deviceType.name === "")
        {
            deviceTypeNull = true;
            return false;
        } else {
            deviceTypeNull = false;
        }

        for(const parameter of parameters) {
            if (parameter.name === null || parameter.name === "" || (parameter.lowerLimit === null && parameter.upperLimit === null))
            {
                checkDone = true;
                return false;
            }
            if (parameter.lowerLimit !== null && parameter.upperLimit !== null && parameter.lowerLimit > parameter.upperLimit)
            {
                checkDone = true;
                return false;
            }
        }
    return true;
    }

    async function CreateParameters()
    {
        const url = `${config.apiUrl}/api/parameters`;

        for (const parameter of parameters) {
            // Add the current deviceTypeId to the parameter
            parameter.deviceTypeId = deviceTypeId;

            try {
                const response = await fetch(url, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(parameter),
                });

                if (!response.ok) {
                    throw new Error('Failed to create parameter');
                }

                const result = await response.json();
                console.log('Parameter created successfully:', result);

            } catch (error) {
                console.error('Error creating parameter:', error.message);
            }
        }
    }

    function MoveToList(){
      navigate(`/devices/`);
    }

    async function createDeviceType(){
        if (checkMandatoryFields() === false)
        {
            return;
        }

        const url = `${config.apiUrl}/api/deviceTypes`;

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(deviceType),
            });

            if (!response.ok) {
                throw new Error('Failed to create device type');
            }

            const result = await response.json();
            deviceTypeId = result.id;
            console.log('Device type created successfully:', result);

        } catch (error) {
            console.error('Error creating device:', error.message);
        }
        CreateParameters();
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
                    <h2 class="text-3xl font-bold font-poppins-light text-left pb-10">Nový typ zařízení</h2>
                </div>
                <div class="mb-4 w-full">
                    <div class="flex-row flex">
                        <label for="username" class="block mb-1 text-lg font-medium text-gray-700">Jméno*</label>
                    </div>
                    <input bind:value={deviceType.name} type="text" id="username" class={`w-1/3 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700  ${deviceTypeNull ? 'border-red-500 border-2' : ''}`} placeholder="Přidejte jméno..."/>
                </div>
                <div class="mb-4 w-full">
                    <div class = "flex-row flex items-start">
                        <label for="parameters" class="block mb-1 text-lg font-medium text-gray-700">Parametry</label>
                        {#if parameters.length !== 0}
                        <img src="{QuestionMark}" alt="QuestionMark" class="pl-1 h-5 w-5" on:blur={toggleDescription} on:mouseover={toggleDescription} on:focus={toggleDescription} on:mouseout={toggleDescription}>
                        {#if showDescription === true}
                        <div class="pl-2 pr-2  rounded-xl text-sm text-gray-600">
                            <p>Při vytváření parametru musí být zadána alespoň jedna z hodnot.</p>
                        </div>
                        {/if}
                        {/if}
                    </div>
                    {#each parameters as parameter, index}
                    <div class="pb-2 flex-row flex items-center">
                        <input type="text" id="name" bind:value={parameter.name} class={`w-1/4 px-3 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-slate-700 ${checkDone && (parameter.name === null || parameter.name === "")  ? 'border-red-500 border-2' : ''}`} placeholder="Název parametru*"/>
                        <label for="min" class=" px-3 w-1/4 block mb-1 text-base font-medium text-gray-700 text-right">Min. hodnota:</label>
                        <input type="number" bind:value={parameter.lowerLimit} required class={`border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer ${parameter.lowerLimit != null && parameter.upperLimit != null && parameter.lowerLimit > parameter.upperLimit ? 'border-red-500 border-2' : ''} ${checkDone && (parameter.lowerLimit === null && parameter.upperLimit === null)  ? 'border-red-500 border-2' : ''}`} />
                        <label for="min" class=" px-3 w-1/4 block mb-1 text-base font-medium text-gray-700 text-right">Max. hodnota:</label>
                        <input type="number" bind:value={parameter.upperLimit} required class={`border border-gray-300 rounded-xl p-2 w-1/5 hover:cursor-pointer ${parameter.lowerLimit != null && parameter.upperLimit != null && parameter.lowerLimit > parameter.upperLimit ? 'border-red-500 border-2 ' : ''} ${checkDone && (parameter.lowerLimit === null && parameter.upperLimit === null)  ? 'border-red-500 border-2 ' : ''}`}/>
                        <button
                        on:click={() => removeParameter(index)}
                            class="px-2 ml-2 py-2 rounded-3xl bg-slate-500 hover:bg-slate-700 text-white">
                            <img src={TrashBin} alt="New" class="h-5 w-5"/>
                        </button>
                    </div>
                    {#if parameter.lowerLimit > parameter.upperLimit && parameter.lowerLimit !== null && parameter.upperLimit !== null}
                    <p class="italic text-sm text-right" >Minimální hodnota musí být menší než maximální.</p>
                    {/if}
                    {/each}
                    <div class="flex  w-1/3 justify-start">
                        <button 
                            on:click={addParameter}
                            class="px-2 py-2 rounded-3xl bg-slate-500 hover:bg-slate-700 text-white">
                            <img src={New} alt="New" class="h-5 w-5"/>
                        </button>
                    </div>
                </div>
                <div class="flex  w-full justify-end">
                    <div class="w-2/3">

                    </div>
                    <button 
                        on:click={() => createDeviceType()}
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

  