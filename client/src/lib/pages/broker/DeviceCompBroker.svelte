<script>
    import TrashBin from "../../../assets/trash.svg";
    import {devices} from "../../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../../assets/users.svg";
    import Devices from "../../../assets/device.svg";
    import Edit from "../../../assets/edit.svg";
    export let device;

    function MoveToDetail(deviceId){
      navigate(`/broker/${deviceId}`);
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-base text-gray-300">
      <button class="hover:cursor-pointer hover:underline" on:click={() => MoveToDetail(device.id)}>
        <p>{device.userAlias}</p>
      </button>
    </td>
    <td class="py-4 px-6">{device.deviceTypeName}</td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
            {device.systemName || "-"}
        </div>
      </div>
    </td>
    <td class="py-4 px-6">
      <div class="flex-row flex items-center justify-center">
        <div class="">
          {device.creatorName}
        </div>
      </div>
    </td>
</tr>