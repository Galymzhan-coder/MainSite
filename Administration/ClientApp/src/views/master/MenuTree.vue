<template>
  <ul class="bg-gray-800 cursor-pointer">
    <li v-for="menuItem in menuItems" :key="menuItem.id">
      <div @click="toggleChildren(menuItem)" v-if="!menuItem.children">
        <router-link :to="menuItem.path" :class="menuItem.routerClass" @click="toggleChildren(menuItem)">
          <div v-html="menuItem.svg"></div>
          <div style="width: 150px; text-align: left;" class="ml-1">{{ menuItem.label }}</div>
        </router-link>
      </div>
      <div :class="menuItem.routerClass" @click="toggleChildren(menuItem)"  v-else>
        <div v-html="menuItem.svg"></div>
        <div style="width: 150px; text-align: left;" class="ml-1">{{ menuItem.label }}</div>
        <div class="menu-down-open" :class="{'transform rotate-180': menuItem.showChildren, 'transform rotate-0': !menuItem.showChildren, 'transition-transform duration-300': true}">
          <svg class="h-7 w-7 text-white-500" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" />
            <polyline points="6 9 12 15 18 9" />
          </svg>
        </div>
      </div>
        <MenuTree :menuItems="menuItem.children" v-if="menuItem.children && menuItem.showChildren" />
    </li>
  </ul>
</template>


<script>
  export default {
    name: 'MenuTree',
    props: {
      menuItems: {
        type: Array,
        required: true
      },
      level: {
        type: Number,
        default: 0
      }
    },
    data() {
      return {
        showChildren: false,
        lastMenuChildren: null,
      };
    },
    methods: {
      toggleChildren(menuItem) {
        if (this.lastMenuChildren && this.lastMenuChildren !== menuItem) {
          this.lastMenuChildren.showChildren = false;
        }
        // Переключаем текущее состояние элемента
        if (menuItem.children) {
          menuItem.showChildren = !menuItem.showChildren;
          // Обновляем ссылку на последний открытый элемент
          this.lastMenuChildren = menuItem.showChildren ? menuItem : null;
        }
      },
    },
  };
</script>

<style scoped>
  @import '../../assets/css/app.css';

  li {
    margin: 0;
  }
  .menu-down-open {
    margin-left: auto;
  }

.menu-item-enter-active, .menu-item-leave-active {
  transition: max-height 0.5s ease;
}

.menu-item-enter, .menu-item-leave-to {
  max-height: 0;
  overflow: hidden;
}


</style>
