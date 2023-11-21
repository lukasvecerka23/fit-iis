<script>
    export let name;
    export let description;
    export let creatorName;
  </script>
  
  <div class="bg-white rounded-lg p-4 shadow-md my-2">
    <div class="flex flex-row py-2 h-1/2">
        <div class="flex flex-col justify-end">
            <h3 class="text-xl text-gray-700 pl-4">{name}</h3>
        </div>
        <div class="flex flex-col justify-end">
            <p class="text-sm text-gray-700 pl-4">{`Admin: ${creatorName}`}</p>
        </div>
        <div class="flex flex-col justify-end ml-auto">
            <p class="text-sm text-gray-600">{description}</p>
        </div>
    </div>
  </div>