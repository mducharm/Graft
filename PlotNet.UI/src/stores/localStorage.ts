import { writable } from "svelte/store";

const config = JSON.parse(localStorage.getItem("config"));

export const storage = writable<Config>(config || {
    showFilter: false
});

storage.subscribe(value => {
    localStorage.setItem("config", JSON.stringify(value));
});

export type Config = {
    showFilter: boolean
}