<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
    props: {
        apiURL: String
    },
    data() {
        return {
            randArr: [],
            randRespDotNet: "",
            randRespNodeJS: "",
            dotnetTime: 0,
            nodejsTime: 0,
            APICallsNumber: 1,
            simultaneousRequests: 1
        }
    },
    methods: {
        async fetchRand() {
            interface RandAPIResponse {numbers: number[],timestamp: string}
            let startTime = performance.now();
            this.randArr = await fetch(import.meta.env.VITE_DOTNET_API+'random').then(x=>x.json());
            this.randRespDotNet = '['+((this.randArr as unknown) as RandAPIResponse[]).map(x=>x.numbers.join(',')).join('],[')+']';
            this.dotnetTime = performance.now()-startTime;

            startTime = performance.now();
            this.randArr = await fetch(import.meta.env.VITE_NODE_API+'random').then(x=>x.json());
            this.randRespNodeJS = '['+((this.randArr as unknown) as RandAPIResponse[]).map(x=>x.numbers.join(',')).join('],[')+']';
            this.nodejsTime = performance.now()-startTime;
        }
    }
})
</script>
<template>
    <div id="settings">
        Number of API calls:<br><input type="text" v-model="APICallsNumber">
        Simultaneous requests:<br><input type="text" v-model="simultaneousRequests">
    </div>
    <button @click="fetchRand">Fetch</button>
    <div v-if="randRespDotNet" id="dotnet" class="numbers">
        <p>DotNet Random API:</p>
        <p>Response time: {{dotnetTime}}ms</p>
        <p>{{randRespDotNet}}</p>
    </div>
    <div v-if="randRespNodeJS" id="nodejs" class="numbers">
        <p>NodeJS Random API:</p>
        <p>Response time: {{nodejsTime}}ms</p>
        <p>{{randRespNodeJS}}</p>
    </div>
</template>
<style>
.numbers {
    max-height: 50%;
    overflow-y: scroll;
    overflow-x: hidden;
}
</style>