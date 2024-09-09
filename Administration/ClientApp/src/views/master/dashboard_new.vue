<template>
  <div class="w-full h-full overflow-x-hidden">
    <!-- Side bar -->

    <div class="fixed w-[300px] h-[calc(100vh-25px)] text-white top-0 left-0 z-10 transform transition-transform duration-500 ease-in-out"
         :class="{ '-translate-x-full': !showSide, 'translate-x-0': showSide }">
      <div class="h-[50px] bg-gray-700 w-[280px] border-2 border-gray-900 relative z-1 float-left flex items-center justify-center">
        <h2 class="font-bold text-xl w-full text-center">Admin dashboard</h2>
      </div>
      <div class="menu-container bg-gray-700 w-[280px] overflow-auto">
        <div class="flex flex-col justify-between space-y-[10px] mb-10">
          <div class="flex flex-col justify-between">
            <MenuTree :menuItems="menuItems" />
          </div>

          <!--<img :src="mySvgIcon" alt="MySvgIcon" />-->
          <!--
        <div class="h-[50px]">
          <div>
            <router-link
              to="/settings"
              class="inline-flex relative items-center py-[10px] px-[10px] w-full text-sm font-medium rounded-md border-gray-200 hover:bg-gray-300 hover:text-gray-800 transition duration-400 ease-in-out"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke-width="1.5"
                stroke="currentColor"
                class="w-6 h-6"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M12 9.75v6.75m0 0l-3-3m3 3l3-3m-8.25 6a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z"
                />
              </svg>
              Settings
            </router-link>
          </div>
        </div>
          -->
        </div>
      </div>
    </div>
    <div class="h-full w-full">
      <div class="top-0 fixed h-[50px] bg-gray-100 flex items-center shadow-sm px-[20px] py-[10px] border-b z-10 transform transition-transform duration-500 ease-in-out"
           :class="{
          'w-[calc(100%-280px)] translate-x-[280px]': showSide,
          'w-full translate-x-0': !showSide,
        }">
        <!-- Hamburger menu -->
        <div class="cursor-pointer w-[30px]" @click="toogleSideBar">
          <svg xmlns="http://www.w3.org/2000/svg"
               fill="none"
               viewBox="0 0 24 24"
               stroke-width="1.5"
               stroke="currentColor"
               class="w-6 h-6">
            <path stroke-linecap="round"
                  stroke-linejoin="round"
                  d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
          </svg>
        </div>
        <!-- Search bar -->
        <div class="w-[calc(100%-30px)] flex">
          <!-- Search bar -->
          <div class="w-[calc(100%-180px)] flex justify-center transition-width duration-500 ease-in-out">
            <form class="flex items-center w-[400px]">
              <label for="voice-search" class="sr-only">Search</label>
              <div class="relative w-full">
                <div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
                  <svg class="absolute text-slate-400 h-5 w-5"
                       viewBox="0 0 20 20"
                       fill="currentColor">
                    <path fill-rule="evenodd"
                          d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                          clip-rule="evenodd" />
                  </svg>
                </div>
                <input type="text"
                       id="voice-search"
                       class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-10 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                       placeholder="Search Mockups, Logos" />

              </div>
            </form>
          </div>
          <!-- User login-->
          <div class="w-[200px]">
            <div class="flex items-center justify-start space-x-4"
                 @click="toogleDrop">
              <img class="w-10 h-10 rounded-full border-2 border-gray-50"
                   src="https://yt3.ggpht.com/TIQQYIzlVLDqthkfjBefvxUaca6HFJVgHeN6Y63iND-pmSf0PJ5GzDh0HnuOyOy1Vrlc2BQp4g=s48-c-k-c0x00ffffff-no-rj" />
              <div class="font-semibold dark:text-white text-left">
                <div>Konilzhai, Dev</div>
                <div class="text-xs text-gray-500 dark:text-gray-400">
                  Admin
                </div>
              </div>
            </div>
            <!-- Drop down -->
            <div v-show="showDropDown"
                 class="absolute right-[10px] z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
              <div class="py-1 text-left" role="none">
                <a href="#"
                   class="text-gray-700 block px-4 py-2 text-sm"
                   role="menuitem"
                   tabindex="-1"
                   id="menu-item-0">Account settings</a>
                <a href="#"
                   class="text-gray-700 block px-4 py-2 text-sm"
                   role="menuitem"
                   tabindex="-1"
                   id="menu-item-1">Support</a>
                <a href="#"
                   class="text-gray-700 block px-4 py-2 text-sm"
                   role="menuitem"
                   tabindex="-1"
                   id="menu-item-2">License</a>
                <form method="POST" action="#" role="none">
                  <button type="submit"
                          class="text-gray-700 block w-full px-4 py-2 text-left text-sm"
                          role="menuitem"
                          tabindex="-1"
                          id="menu-item-3">
                    Sign out
                  </button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="h-full bg-gray-50 p-[20px] top-0">
        <div class="absolute border border-gray-300 p-[20px] h-full border-none top-[50px] transform transition-transform duration-500 ease-in-out"
             :class="{
            'w-[calc(100%-280px)] translate-x-[260px]': showSide,
            'w-[calc(100%-30px)] translate-x-0': !showSide,
          }">
          <transition name="page" mode="out-in">
            <router-view @handle-delete-modal="handleShowDeleteModal"></router-view>
          </transition>
          <div class="border-t-2 pt-4 pb-10">
            <label class="">AO NSK © 2024</label>
          </div>
        </div>
      </div>
      <!-- Main -->
    </div>
    <delete-modal :isVisible="showModal"
                  :type="type"
                  :recordId="recordId"
                  @handle-delete-modal="handleShowDeleteModal"/>
  </div>
</template>

<script>
import MenuTree from "./MenuTree.vue";
  import MySvgIcon from "@/assets/images/insurance-svgrepo-com.svg";
  import menuItems from "@/utils/menu-items";
export default {
  components: {
    MenuTree,
  },
  data() {
    return {
      showModal: false,
      type: null,
      recordId: null,
      showDropDown: false,
      showSide: true,
      mySvgIcon: MySvgIcon,
      menuItems: menuItems
    };
  },
  methods: {
    //hide show side bar
    toogleSideBar() {
      this.showSide = !this.showSide;
    },
    //toogle user
    toogleDrop() {
      this.showDropDown = !this.showDropDown;
    },
    handleShowDeleteModal(show, type, id) {
      this.type = type;
      this.recordId = id;
      this.showModal = show;
    },

  },
};
</script>

<style scoped>
.page-enter-active,
.page-leave-active {
  transition: 0.5s;
}

.page-enter,
.page-leave-to {
  opacity: 30%;
}
.menu-container {
  height: calc(100vh - 3rem); /* Высота экрана минус 2rem (20px) отступа */
}
</style>
