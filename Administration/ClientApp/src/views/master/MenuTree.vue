<template>
  <ul class="bg-gray-800 cursor-pointer">
    <li v-for="menuItem in menuItems" :key="menuItem.id" :class="{'bg-gray-900 text-white relative': menuItem === activeMenuItem}">
      <div @click="toggleChildren(menuItem)" v-if="!menuItem.children">
        <router-link :to="menuItem.path" :class="menuItem.routerClass" @click="toggleChildren(menuItem)">
          <div class="w-2 h-2 bg-white rounded-full absolute left-1" v-if="menuItem === activeMenuItem"></div>
          <div v-html="menuItem.svg"></div>
          <div style="width: 150px; text-align: left;" class="ml-1">{{ menuItem.label }}</div>
        </router-link>
      </div>
      <div :class="menuItem.routerClass" @click="toggleChildren(menuItem)" v-else>
        <div v-html="menuItem.svg"></div>
        <div style="width: 150px; text-align: left;" class="ml-1">{{ menuItem.label }}</div>
        <div class="menu-down-open" :class="{'transform rotate-180': menuItem.showChildren, 'transform rotate-0': !menuItem.showChildren, 'transition-transform duration-300': true}">
          <svg class="h-7 w-7 text-white-500" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" />
            <polyline points="6 9 12 15 18 9" />
          </svg>
        </div>
      </div>
      <transition
        name="slide-fade"
        @before-enter="beforeEnter"
        @enter="enter"
        @after-enter="afterEnter"
        @before-leave="beforeLeave"
        @leave="leave"
      >
        <div v-if="menuItem.showChildren" class="overflow-hidden">
          <MenuTree :menuItems="menuItem.children" class="menu-lists"/>
        </div>
      </transition>
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
        activeMenuItem: null,
      };
    },
    computed: {
      activePath() {
        return this.$route.path;
      }
    },
    watch: {
      activePath() {
        this.setActiveMenuItem();
      }
    },
    mounted() {
      this.setActiveMenuItem();
    },
    methods: {
      setActiveMenuItem() {
        const findActiveMenuItem = (items, parent = null) => {
          for (const item of items) {
            if (item.path === this.activePath) {
              this.activeMenuItem = item;
              this.openParentItems(parent);
              break;
            } else if (item.children) {
              findActiveMenuItem(item.children, item);
            }
          }
        };
        findActiveMenuItem(this.menuItems);
      },
      openParentItems(item) {
        if (item) {
          item.showChildren = true;
          if (item.parent) {
            this.openParentItems(item.parent);
          }
        }
      },
      toggleChildren(menuItem) {
        // Закрываем все открытые элементы
        const closeAllOpenItems = (items) => {
          items.forEach(item => {
            if (item !== menuItem && item.showChildren) {
              item.showChildren = false;
            }
            if (item.children) {
              closeAllOpenItems(item.children);
            }
          });
        };
        closeAllOpenItems(this.menuItems);

        // Переключаем текущее состояние элемента
        if (menuItem.children) {
          menuItem.showChildren = !menuItem.showChildren;
          // Обновляем ссылку на последний открытый элемент
          this.lastMenuChildren = menuItem.showChildren ? menuItem : null;
        }
        this.activeMenuItem = menuItem;
      },

      /*toggleChildren(menuItem) {
        if (this.lastMenuChildren && this.lastMenuChildren !== menuItem) {
          this.lastMenuChildren.showChildren = false;
        }
        // Переключаем текущее состояние элемента
        if (menuItem.children) {
          menuItem.showChildren = !menuItem.showChildren;
          // Обновляем ссылку на последний открытый элемент
          this.lastMenuChildren = menuItem.showChildren ? menuItem : null;
        }
        this.activeMenuItem = menuItem;

      },*/
      beforeEnter(el) {
        el.style.maxHeight = '0';
      },
      enter(el) {
        el.style.maxHeight = `${el.scrollHeight}px`;
        el.style.transition = 'max-height 0.5s ease-in-out';
      },
      afterEnter(el) {
        el.style.maxHeight = 'none';
      },
      beforeLeave(el) {
        el.style.maxHeight = `${el.scrollHeight}px`;
      },
      leave(el) {
        el.style.maxHeight = '0';
        el.style.transition = 'max-height 0.5s ease-in-out';
      }
    },
  };
</script>

<style scoped>
li {
  margin: 0;
}
.menu-down-open {
  margin-left: auto;
}
.slide-fade-enter-active, .slide-fade-leave-active {
  overflow: hidden;
}
.active-menu-item {
  background-color: #4A5568; /* Меняем цвет фона активного элемента */
  color: white; /* Меняем цвет текста активного элемента */
}

</style>
