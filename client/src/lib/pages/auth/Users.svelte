<script>
    import {onMount} from 'svelte'
    import Sidebar from '../../components/SideBar.svelte';
    import TopBar from '../../components/TopBar.svelte';
    import UserComp from '../../components/UserComp.svelte';
    import {users} from '../../../store.js';
    import New from '../../../assets/new.svg';
    import config from '../../../config.js';
    import { Link, navigate } from "svelte-routing";
  
    let searchTerm = '';
    let currentPageIndex = 0;
    const pageSize = 10;
    let totalPages = 0;
    let updatedRole = false;

    let roles = [];
    let roleChanges = {};

    async function fetchUsers() {
        const params = new URLSearchParams({
            p: currentPageIndex,
            size: pageSize,
        });
        if (searchTerm.length >= 3) {
            params.append('q', searchTerm);
        }

        const resp = await fetch(`${config.apiUrl}/api/users/search?${params}`);
        const data = await resp.json();
        users.set(data.users);
        totalPages = data.totalPages; // Update this based on your API response
    }

    async function getRoles(){
        try {
            const response = await fetch(`${config.apiUrl}/api/roles`, {
                method: 'GET',
                credentials: 'include',
            });

            if (response.ok) {
                roles = await response.json();
            } else {
                console.error('Error getting roles:', await response.text());
            }
        } catch (error) {
            console.error('Error getting roles:', error);
        }
    }

    async function updateUserRole(userId, newRoleId){
        const updatedUser = $users.find(u => u.id === userId);
        try {
            const resp = await fetch(`${config.apiUrl}/api/users/${updatedUser.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ 
                    name: updatedUser.name, 
                    surname: updatedUser.surname, 
                    username: updatedUser.username, 
                    roleId: newRoleId
                }),
                credentials: 'include',
            });
            if (resp.ok) {
                // Remove the system from the local array
                users.update(currentUsers => {
                    return currentUsers.map(user => 
                        user.id === updatedUser.id ? { ...user, roleId: newRoleId } : user
                )});
            } else {
                console.error('Error updating user:', await resp.text());
            }
        } catch (error) {
            console.error('Error updating user:', error);
        }
    }

    // Function to save changes
    async function saveAllChanges() {
        for (const [userId, newRoleId] of Object.entries(roleChanges)) {
            await updateUserRole(userId, newRoleId);
        }

        roleChanges = {};
        updatedRole = false;
    }

  onMount(() => {
    fetchUsers();
    getRoles();
  });

  $: if (searchTerm.length >= 3 || searchTerm.length === 0) {
        currentPageIndex = 0;
        fetchUsers();
    }

    function goToPage(page) {
        currentPageIndex = page;
        fetchUsers();
    }

    function MoveToNew(){
        navigate(`/users/new`);
    }

    function handleRoleChange(userId, newRoleId) {
        if ($users.find(u => u.id === userId).roleId !== newRoleId) {
            roleChanges[userId] = newRoleId;
        } else {
            delete roleChanges[userId];
        }
        updatedRole = true;
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
                <h2 class="text-3xl font-bold mb-0 pt-10 pb-6 font-poppins-light text-left">Uživatelé</h2>
                <div class="flex flex-row">
                    <div class="pb-4 w-1/3 self-start">
                        <input
                        type="text"
                        class="p-2 border border-gray-300 rounded-xl w-full"
                        bind:value={searchTerm}
                        placeholder="Filtrovat uživatele..."
                        />
                    </div>
                    <div class="pb-4 ml-auto rounded-xl">
                        <button class="bg-slate-500 hover:bg-slate-700  text-white font-semibold py-2 px-4 rounded-xl" on:click={() => MoveToNew()}>
                            <div class="flex flex-row">
                                <img src={New} alt="New" class="w-6 h-6 mr-2 font-poppins-light">
                                <span>Nový</span>
                            </div>
                        </button>
                    </div>
                </div>
            </div>
            <div class="w-full">
              <table class="w-full text-sm text-center text-gray-500 dark:text-gray-400 rounded-xl overflow-hidden">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr>
                        <th scope="col" class="py-3 px-6 text-left">Uživatelské jméno</th>
                        <th scope="col" class="py-3 px-6">Jméno a příjmení</th>
                        <th scope="col" class="py-3 px-6">Role</th>
                        <th scope="col" class="py-3 px-6"></th>
                        <th scope="col" class="py-3 px-6"></th>
                    </tr>
                </thead>
                <tbody>
                    {#each $users as user (user.id)}
                      <UserComp user={user} roles={roles} on:roleChange={detail => handleRoleChange(user.id, detail.detail.newId)}/>
                    {/each}
                </tbody>
            </table>
            <!-- Pagination Controls -->
            <div class="flex justify-between">
                <div class="flex gap-2 items-center my-4">
                    <button 
                        class="px-4 py-2 rounded-xl bg-slate-500 text-white disabled:hidden" 
                        on:click={goToPage(currentPageIndex - 1)} 
                        disabled={currentPageIndex === 0}>
                        Zpět
                    </button>
                    <button 
                        class="px-4 py-2 rounded-xl bg-slate-500 text-white disabled:hidden" 
                        on:click={goToPage(currentPageIndex + 1)} 
                        disabled={!totalPages ? true : currentPageIndex === totalPages - 1}>
                        Další
                    </button>
                </div>
                <div class="flex gap-2 items-center my-4">
                    <button 
                        class="px-4 py-2 rounded-xl bg-slate-500 hover:bg-slate-700 text-white disabled:hidden"
                        disabled={!updatedRole}
                        on:click={() => saveAllChanges()}>
                        Uložit
                    </button>
                </div>
            </div>


            </div>
        </div>
    </div>
  </div>
</div>
  
