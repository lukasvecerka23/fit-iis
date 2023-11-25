
<script>
    import { onMount } from 'svelte';
    import { navigate } from 'svelte-routing';
    import { user, loadUser } from '../../../store.js';
    import MainLogo from '../../../assets/mainlogo.svg';
  
    let username = '';
    let password = '';
    let name = '';
    let surname = '';
  
    async function handleRegister() {
      const response = await fetch('https://localhost:7246/api/auth/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password, name, surname }),
        credentials: 'include'
      });
  
      if (response.ok) {
        await loadUser();
        navigate('/');
      } else {
        // Handle login error
      }
    }
  </script>
  
  <main class="flex flex-col items-center justify-center h-screen bg-slate-400">
    <div class="flex flex-col items-center justify-center w-1/3">
      <img src={MainLogo} alt="Logo" class="w-1/3 mb-4"/>
      <h1 class="text-4xl font-bold mb-4 font-poppins-light">Registrace</h1>
    </div>
    <form on:submit|preventDefault={handleRegister} class="w-1/3">
        <div class="mb-4">
            <label for="username" class="block mb-2 text-lg font-medium text-gray-700">Jméno <span class="text-red-500">*</span></label>
            <input type="text" id="username" bind:value={name} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700" required/>
        </div>
        <div class="mb-4">
            <label for="username" class="block mb-2 text-lg font-medium text-gray-700">Příjmení <span class="text-red-500">*</span></label>
            <input type="text" id="username" bind:value={surname} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700" required/>
        </div>
        <div class="mb-4">
            <label for="username" class="block mb-2 text-lg font-medium text-gray-700">Uživatelské jméno <span class="text-red-500">*</span></label>
            <input type="text" id="username" bind:value={username} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700" required/>
        </div>
        <div class="mb-4">
            <label for="password" class="block mb-2 text-lg font-medium text-gray-700">Heslo <span class="text-red-500">*</span></label>
            <input type="password" id="password" bind:value={password} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700" required/>
        </div>
        <button type="submit" class="w-full px-4 py-2 text-lg font-medium text-white bg-slate-500 rounded-md hover:bg-slate-700">Zaregistrovat</button>
    </form>
  </main>
  