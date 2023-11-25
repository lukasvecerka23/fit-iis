<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import DeviceComp from '../../components/DeviceCompDeviceList.svelte';
    import DeviceTypeComp from '../../components/DeviceTypeComp.svelte';
    import {devices} from '../../../store.js';
    import {deviceTypes} from '../../../store.js';
    import New from '../../../assets/new.svg';
    import Device from '../../../assets/device.svg';
    import DeviceDark from '../../../assets/device_dark.svg'
    import DeviceType from '../../../assets/device_type.svg';
    import DeviceTypeDark from '../../../assets/device_type_dark.svg';
    import DeviceTypesCardDeviceList from '../../components/DeviceTypesCardDeviceList.svelte';
  
    let searchTerm = '';
    let currentPageIndex = 0;
    const pageSize = 10;
    let totalPages = 0;
    let activeCard = 'devices';
    let isSmallScreen = false;

    async function fetchDevices() {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
        });
        if (searchTerm.length >= 3) {
            params.append('q', searchTerm);
        }

        const resp = await fetch(`https://localhost:7246/api/devices/search?${params}`);
        const data = await resp.json();
        devices.set(data.devices);
        totalPages = data.totalPages; // Update this based on your API response
    }

    async function fetchDeviceTypes() {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
        });
        if (searchTerm.length >= 3) {
            params.append('q', searchTerm);
        }

        const resp = await fetch(`https://localhost:7246/api/deviceTypes`);
        const data = await resp.json();
        deviceTypes.set(data);
        totalPages = data.totalPages; // Update this based on your API response
    }

        //for devices and users buttons description
    onMount(() => {
        const mediaQuery = window.matchMedia('(max-width: 1200px)');
        isSmallScreen = mediaQuery.matches;

        const updateScreenSize = () => {
            isSmallScreen = mediaQuery.matches;
        };

        mediaQuery.addListener(updateScreenSize);

        return () => {
            mediaQuery.removeListener(updateScreenSize);
        };
    });

  onMount(fetchDeviceTypes);
  onMount(fetchDevices);


  $: if (searchTerm.length >= 3 || searchTerm.length === 0) {
        currentPageIndex = 0;
        fetchDevices();
    }

    function goToPage(page) {
        currentPageIndex = page;
        fetchDevices();
    }

</script>

<div class="flex flex-col w-full h-screen bg-slate-400">
  <TopBar />
  <div class="flex flex-1 overflow-hidden">
    <Sidebar/>
    <div class="flex flex-1 bg-primary-white justify-center overflow-auto">
        <!-- Pole pro filtraci nad seznamem devices -->
        <div class="flex-col flex w-4/5 items-center">
            <div class = "flex-col flex w-full">
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-6 font-poppins-light text-left">Zařízení</h2>
                <div class="flex flex-row">
                    <div class="pb-4 w-1/3 self-start">
                        <input
                        type="text"
                        class="p-2 border border-gray-300 rounded-xl w-full"
                        bind:value={searchTerm}
                        placeholder="Filtrovat zařízení..."
                        />
                    </div>
                    <div class="pb-4 ml-auto rounded-xl">
                        <button class="bg-slate-500 hover:bg-slate-300  text-white font-semibold py-2 px-4 rounded-xl">
                            <div class="flex flex-row">
                                <img src={New} alt="New" class="w-6 h-6 mr-2 font-poppins-light">
                                <span>Nové</span>
                            </div>
                        </button>
                    </div>
                </div>
                <div class="flex-row items-start w-1/3">
                    <div class="grid w-full grid-cols-2 gap-2 rounded-3xl bg-gray-300 p-1">
                        <div>
                            <input type="radio" name="option" id="1" value="1" class=" peer hidden" checked on:click={() => (activeCard = 'devices')}/>
                            <label for="1" class="{activeCard === 'devices' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400'} radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                                <div class="flex-row flex justify-center">
                                    {#if activeCard === 'devices'}
                                    <img src={Device} alt="Device" class="w-6 h-6" />
                                    {:else}
                                    <img src={DeviceDark} alt="DeviceDark" class="w-6 h-6" />
                                    {/if}
                                    {#if !isSmallScreen}
                                        <p class="pl-2">Zařízení</p>
                                    {/if}
                                </div>
                            </label>
                        </div>
                
                        <div>
                            <input type="radio" name="option" id="2" value="2" class="peer hidden" on:click={() => (activeCard = 'devicetypes')}/>
                            <label for="2" class="{activeCard === 'devicetypes' ? 'bg-gray-800 text-white' : 'bg-gray-300 hover:bg-gray-400' } radio-label block cursor-pointer select-none rounded-3xl p-1 text-center ">
                                <div class="flex-row flex justify-center">
                                    {#if activeCard === 'devicetypes'}
                                    <img src={DeviceType} alt="DeviceTypes" class="w-6 h-6" />
                                    {:else}
                                    <img src={DeviceTypeDark} alt="DeviceTypesDark" class="w-6 h-6" />
                                    {/if}
                                    {#if !isSmallScreen}
                                        <p class="pl-2">Typy zařízení</p>
                                    {/if}
                                </div>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        
            
            <div class="w-full pt-4">
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
                    {#if activeCard === 'devices'}
                    <DeviceTypesCardDeviceList deviceTypes={deviceTypes}/>
                    {/if}
                </tbody>
            </table>
            <!-- Pagination Controls -->
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
          </div>

            </div>
        </div>
    </div>
  </div>
</div>
  
