<script>
    import {systems, selectedParameterId} from "../../../store.js";
    import { Link, navigate } from "svelte-routing";

    export let parameter;

    function selectParameter(parameterId){

      selectedParameterId.set(parameterId);
    }

  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-gray-300 text-base">
      <button class="hover:cursor-pointer hover:underline" on:click={() => selectParameter(parameter.id)}>
        <p>{parameter.name}</p>
      </button>
    </td>
    <td class="py-4 px-6 text-center w-1/6">
      <p>{parameter.lowerLimit}</p>
    </td>
    <td class="py-4 px-6 text-center w-1/6">
      <p>{parameter.upperLimit}</p>
    </td>
</tr>