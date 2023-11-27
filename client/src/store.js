import { writable } from "svelte/store";

export const systems = writable([]);
export const devices = writable([]);
export const deviceTypes = writable([]);
export const users = writable([]);
export const user = writable(null);
export const selectedDevices = writable({});

export const selectedParameterId = writable(null);
export const systemDetailActiveCard = writable(null);

export const reloadSystems = writable(false);

export async function loadUser() {
    const response = await fetch('https://localhost:7246/api/auth/user', {
        method: 'GET',
        credentials: 'include'
    });
    if (response.ok) {
        const data = await response.json();
        user.set(data);
    } else {
        user.set(null);
    }
}