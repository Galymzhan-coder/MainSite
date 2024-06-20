<template>
    <HeadTitle :title="createTitle" />
    <p>Abs</p>
</template>

<script setup>
import HeadTitle from "@/components/HeadTitle.vue";
import { ref } from 'vue'

const createTitle = ref('Создание статьи')


</script>

<style>
</style>
