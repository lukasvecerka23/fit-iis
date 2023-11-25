<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../assets/users.svg";
    import Edit from "../../assets/edit.svg";
    import StatusOk from '../../assets/status_ok.svg';
    import StatusWarning from '../../assets/status_warning.svg';
    import StatusBad from '../../assets/status_bad.svg';
    export let parameter;


    function MoveToDetail(parameterId){
      navigate(`/parameters/${parameterId}`);
    }
  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-gray-300 text-base">
      <button class="hover:cursor-pointer hover:underline" on:click={() => MoveToDetail(parameter.id)}>
        <p>{parameter.name}</p>
      </button>
    </td>
    <td class="py-4 px-6 text-center">
      <p>{parameter.lowerLimit}</p>
    </td>
    <td class="py-4 px-6 text-center">
      <p>{parameter.upperLimit}</p>
    </td>
    <td class="py-4 px-0 flex-row flex justify-center">
      <div class="bg-green-600 text-white font-semibold w-6 h-6 py-1 px-1 rounded-3xl">
        <img src={StatusOk} alt="StatusOk" class="" />
      </div>
    </td>
</tr>