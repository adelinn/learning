<script lang="ts">
import { defineComponent } from 'vue'

interface RandAPIResponse {numbers: number[],timestamp: string}

const args = (<any>window).viteArgs;

export default defineComponent({
    props: {
        apiURL: String
    },
    data() {
        return {
            randRespDotNet: "",
            randRespNodeJS: "",
            dotnetTime: 0,
            nodejsTime: 0,
            APICallsNumber: 1,
            simultaneousRequests: 1,
            table: [[0]]
        }
    },
    methods: {
        async callNetAPI(i: number) {
            let startTime = performance.now();
            let randArr: RandAPIResponse[];
            try{
                randArr = await fetch(args.DOTNET_API+'random').then(x=>x.json());
            }catch{
                randArr = await fetch(args.DOTNET_API+'random').then(x=>x.json());
            }
            this.randRespDotNet = '['+randArr.map(x=>x.numbers.join(',')).join('],[')+']';
            this.dotnetTime = performance.now()-startTime;
            this.table[i-1][0]=this.dotnetTime;
        },
        async callNodeAPI(i: number) {
            let startTime = performance.now();
            let randArr: RandAPIResponse[];
            try{
                randArr = await fetch(args.NODE_API+'random').then(x=>x.json());
            }catch{
                randArr = await fetch(args.NODE_API+'random').then(x=>x.json());
            }
            this.randRespNodeJS = '['+randArr.map(x=>x.numbers.join(',')).join('],[')+']';
            this.nodejsTime = performance.now()-startTime;
            this.table[i-1][1]=this.nodejsTime;
        },
        async fetchRand() {
            let queue: Promise<void>[][] = [];
            this.table = []; (<any>window)._table=this.table; (<any>window)._calls=this.APICallsNumber;
            if(this.simultaneousRequests<1) throw new Error("There should be at least one simultaneous request... "+this.simultaneousRequests+" provided.");
            for(let i=1;i<=this.APICallsNumber;i++){
                queue.push([this.callNetAPI(i),this.callNodeAPI(i)]);
                this.table.push([]);
                if(i%this.simultaneousRequests==0) {
                    await Promise.all(queue.map(x=>Promise.all(x)));
                    queue = [];
                }
            }
            await Promise.all(queue.map(x=>Promise.all(x)));
        }
    }
})
</script>
<template>
    <div id="settings">
        Number of API calls:<br><input type="text" v-model="APICallsNumber" placeholder="calls"><br>
        Simultaneous requests:<br><input type="text" v-model="simultaneousRequests" placeholder="reqs">
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
    <div id="results">
        <table>
            <tr>
                <th>DotNet</th>
                <th>NodeJS</th>
            </tr>
            <tr v-for="row in table">
                <td>{{row[0]}}</td>
                <td>{{row[1]}}</td>
            </tr>
        </table>
    </div>
</template>
<style>
.numbers {
    max-height: 30%;
    overflow: scroll;
}
#settings > input {
    width: 90px;
}
</style>