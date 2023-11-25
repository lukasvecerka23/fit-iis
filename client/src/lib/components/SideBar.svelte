<script>
    import SystemLogo from "../../assets/system.svg";
    import DevicesLogo from "../../assets/devices.svg";
    import LogoutLogo from  "../../assets/log_out.svg";
    import UsersLogo from "../../assets/users_white.svg";
    import NavButton from "./NavButton.svelte";
    import { Link} from "svelte-routing";
    import {user} from "../../store.js";

    $: activepage = location;

    function isActivePage(page) {
        return activepage.pathname === page;
    }
</script> 

<nav class="bg-slate-500 w-20 text-white flex flex-col ">
        <div class="flex-col">
            <NavButton linkPath="/"
            text="Systémy"
            logoUrl={SystemLogo}
            classBackgroundColor={(isActivePage("/") || activepage.pathname.includes("/systems/")) ? "bg-slate-600" : ""}
            />
            {#if $user}
            <NavButton linkPath="/devices"
            text="Zařízení"
            logoUrl={DevicesLogo}
            classBackgroundColor={(isActivePage("/devices") || activepage.pathname.includes("/devices/")) ? "bg-slate-600" : ""}
            />
                {#if $user.role === "Admin"}
                <NavButton linkPath="/users"
                text="Uživatelé"
                logoUrl={UsersLogo}
                classBackgroundColor={isActivePage("/users") ? "bg-slate-600" : ""}
                />
                {/if}
            {/if}
        </div>
  </nav>
