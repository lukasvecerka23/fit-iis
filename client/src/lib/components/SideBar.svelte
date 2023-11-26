<script>
    import SystemLogo from "../../assets/system.svg";
    import DevicesLogo from "../../assets/device_white.svg";
    import LogoutLogo from  "../../assets/log_out.svg";
    import UsersLogo from "../../assets/users_white.svg";
    import BrokerLogo from "../../assets/broker.svg";
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

            <!-- User is logged in -->
            {#if $user}
                <!-- Everybody except broker -->
                {#if $user.role !== "Broker"}
                <NavButton linkPath="/"
                text="Systémy"
                logoUrl={SystemLogo}
                classBackgroundColor={(isActivePage("/") || activepage.pathname.includes("/systems/")) ? "bg-slate-600" : ""}
                />
                {/if}

                <!-- Managing devices only user or admin -->
                {#if $user.role === "Admin" || $user.role === "User"}
                <NavButton linkPath="/devices"
                text="Zařízení"
                logoUrl={DevicesLogo}
                classBackgroundColor={(isActivePage("/devices") || activepage.pathname.includes("/devices/")) ? "bg-slate-600" : ""}
                />
                {/if}

                <!-- User management only Admin -->
                {#if $user.role === "Admin"}
                <NavButton linkPath="/users"
                text="Uživatelé"
                logoUrl={UsersLogo}
                classBackgroundColor={(isActivePage("/users")  || activepage.pathname.includes("/users/"))? "bg-slate-600" : ""}
                />
                {/if}

                <!-- Broker only Admin or Broker -->
                {#if $user.role === "Admin" || $user.role === "Broker"}
                <NavButton linkPath="/broker"
                text="Měření"
                logoUrl={BrokerLogo}
                classBackgroundColor={(isActivePage("/broker") || activepage.pathname.includes("/broker/"))? "bg-slate-600" : ""}
                />
                {/if}
                <!-- Unregistered -->
            {:else}
                <NavButton linkPath="/"
                text="Systémy"
                logoUrl={SystemLogo}
                classBackgroundColor={(isActivePage("/") || activepage.pathname.includes("/systems/")) ? "bg-slate-600" : ""}
                />
            {/if}
        </div>
  </nav>
