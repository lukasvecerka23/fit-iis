
<script>
  import { onMount } from 'svelte';
  import { navigate } from 'svelte-routing';
  import { user, loadUser } from '../../../store.js';
  import MainLogo from '../../../assets/mainlogo.svg';

  let username = '';
  let password = '';

  async function handleLogin() {
    const response = await fetch('https://localhost:7246/api/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      credentials: 'include',
      body: JSON.stringify({ username, password })
    });

    if (response.ok) {
        loadUser().then(() => {
            if ($user.role === 'Broker') {
                navigate('/broker', { replace: true });
            } else {
                navigate('/', { replace: true });
            }
        })
    } else {
      // Handle login error
    }
  }
</script>

<main class="flex flex-col items-center justify-center h-screen bg-slate-400">
  <div class="flex flex-col items-center justify-center w-1/3">
    <img src={MainLogo} alt="Logo" class="w-1/3 mb-4"/>
    <h1 class="text-4xl font-bold mb-4 font-poppins-light">Přihlášení</h1>
  </div>
  <form on:submit|preventDefault={handleLogin} class="w-1/3">
    <div class="mb-4">
      <label for="username" class="block mb-2 text-lg font-medium text-gray-700">Uživatelské jméno</label>
      <input type="text" id="username" bind:value={username} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700"/>
    </div>
    <div class="mb-4">
      <label for="password" class="block mb-2 text-lg font-medium text-gray-700">Heslo</label>
      <input type="password" id="password" bind:value={password} class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-700"/>
    </div>
    <button type="submit" class="w-full px-4 py-2 text-lg font-medium text-white bg-slate-500 rounded-md hover:bg-slate-700">Příhlásit se</button>
  </form>
</main>
