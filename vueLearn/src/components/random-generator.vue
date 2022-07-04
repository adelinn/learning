<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    props: {
        apiURL: String
    },
    data() {
        return {
            randArr: [],
            randResp: ""
        }
    },
    methods: {
        async fetchRand() {
            interface RandAPIResponse {numbers: number[],timestamp: string}
            this.randArr = await fetch(import.meta.env.VITE_DOTNET_API+'random').then(x=>x.json());
            this.randResp = '['+((this.randArr as unknown) as RandAPIResponse[]).map(x=>x.numbers.join(',')).join('],[')+']';
        }
    }
})
</script>
<template>
    <button @click="fetchRand">Fetch</button>
    <p>{{randResp}}</p>
</template>