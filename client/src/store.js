import { writable } from "svelte/store";

export const systems = writable([]);
export const devices = writable([]);
export const user = writable(null);

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