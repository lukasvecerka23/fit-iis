<script>
  import {onMount} from 'svelte'

  let devices = []

  onMount(async () => {
    const resp = await fetch('https://localhost:7246/api/devices')
    devices = await resp.json()
  })
</script>

<ul>
  {#each devices as device (device.id)}
    <li class="text-red-700">{device.userAlias}</li>
  {/each}
</ul>
