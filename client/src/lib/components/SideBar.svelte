<script>
    import SystemLogo from "../../assets/system.svg";
    import DevicesLogo from "../../assets/devices.svg";
    import LogoutLogo from  "../../assets/log_out.svg";
    import NavButton from "./NavButton.svelte";
    import { Link} from "svelte-routing";
    $: activepage = location;

    function isActivePage(page) {
        return activepage.pathname === page;
    }
</script> 

<nav class="bg-slate-500 w-20 text-white flex flex-col ">
        <div class="flex-col">
            <NavButton linkPath="/systems"
            text="Systémy"
            logoUrl={SystemLogo}
            classBackgroundColor={(isActivePage("/systems") || activepage.pathname.includes("/systemDetail/")) ? "bg-slate-600" : ""}
            />
            <NavButton linkPath="/devices"
            text="Zařízení"
            logoUrl={DevicesLogo}
            classBackgroundColor={isActivePage("/devices") ? "bg-slate-600" : ""}
            />
        </div>
        <div class="flex-col">
            <NavButton linkPath="/"
            text="Odhlásit"
            logoUrl={LogoutLogo}
            classBackgroundColor={isActivePage("/") ? "bg-slate-600" : ""}
            />
        </div>
  </nav>
