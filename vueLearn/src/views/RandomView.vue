<script lang="ts">
import randomGenerator from '@/components/random-generator.vue'

export default {
  props: {
    settings: Object
  },
  components: {
    randomGenerator
  }
}
</script>

<template>
  <div class="rand">
    <randomGenerator :apiURL="settings.apiURL" />
  </div>
</template>

<style>
@media (min-width: 1024px) {
  .rand {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
