<script>
    import TrashBin from "../../assets/trash.svg";
    import {systems} from "../../store.js";
    import { Link, navigate } from "svelte-routing";
    import Users from "../../assets/users.svg";
    import Edit from "../../assets/edit.svg";
    import StatusOk from '../../assets/status_ok.svg';
    import StatusWarning from '../../assets/status_warning.svg';
    import StatusBad from '../../assets/status_bad.svg';
    import {mapKpiFunctionToString} from '../../utils.js';
    export let measurement;

  </script>
  
<tr class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-700">
    <td class="py-4 px-6 text-left font-semibold text-gray-300 text-base">
      <button class="hover:cursor-pointer hover:underline">
        <p>{measurement.parameterName}</p>
      </button>
    </td>
    <td class="py-4 px-6 text-center">
      <p>{measurement.timeStamp}</p>
    </td>
    <td class="py-4 px-6 text-center">
      <p>{measurement.value}</p>
    </td>
</tr>