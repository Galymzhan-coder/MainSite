<template>
  <transition name="page" mode="out-in">
    <router-view />
  </transition>
</template>

<style>
.page-enter-active,
.page-leave-active {
    transition: 0.5s;
}

.page-enter,
.page-leave-to {
    opacity: 30%;
}
html, body {
  overflow-x: hidden;
}
</style>
