<template>
  <div class="text-xl font-medium pb-5 flex items-center space-x-2">
    <svg class="h-5 w-5" fill="#000000" height="800px" width="800px" version="1.1" id="Capa_1" viewBox="0 0 458.18 458.18" xml:space="preserve">
      <g>
        <path d="M36.09,5.948c-18.803,0-34.052,15.248-34.052,34.051c0,18.803,15.249,34.052,34.052,34.052   c18.803,0,34.052-15.25,34.052-34.052C70.142,21.196,54.893,5.948,36.09,5.948z"/>
        <path d="M147.537,80h268.604c22.092,0,40-17.908,40-40s-17.908-40-40-40H147.537c-22.092,0-40,17.908-40,40S125.445,80,147.537,80z   "/>
        <path d="M36.09,132.008c-18.803,0-34.052,15.248-34.052,34.051s15.249,34.052,34.052,34.052c18.803,0,34.052-15.249,34.052-34.052   S54.893,132.008,36.09,132.008z"/>
        <path d="M416.142,126.06H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,126.06,416.142,126.06z"/>
        <path d="M36.09,258.068c-18.803,0-34.052,15.248-34.052,34.051c0,18.803,15.249,34.052,34.052,34.052   c18.803,0,34.052-15.249,34.052-34.052C70.142,273.316,54.893,258.068,36.09,258.068z"/>
        <path d="M416.142,252.119H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,252.119,416.142,252.119z"/>
        <path d="M36.09,384.128c-18.803,0-34.052,15.248-34.052,34.051s15.249,34.053,34.052,34.053c18.803,0,34.052-15.25,34.052-34.053   S54.893,384.128,36.09,384.128z"/>
        <path d="M416.142,378.18H147.537c-22.092,0-40,17.908-40,40s17.908,40,40,40h268.604c22.092,0,40-17.908,40-40   S438.233,378.18,416.142,378.18z"/>
      </g>
    </svg>
    <span>Записи блога</span>
  </div>
  <div class="p-4 border text-sm">
    <button class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mb-4">
      Создать запись
    </button>
    <div class="flex flex-col">
      <div class="grid grid-cols-7 gap-0 grid-cols-custom font-semibold">
        <div class="bg-gray-300 px-3 py-2 border">#</div>
        <div class="bg-gray-300 px-3 py-2 border">Категория</div>
        <div class="bg-gray-300 px-3 py-2 border">Заголовок</div>
        <div class="bg-gray-300 px-3 py-2 border">Просмотры</div>
        <div class="bg-gray-300 px-3 py-2 border">Дата создания</div>
        <div class="bg-gray-300 px-3 py-2 border">Активность</div>
        <div class="bg-gray-300 px-3 py-2 border">Действия</div>
      </div>
      <div v-for="(blog, index) in filteredBlogs" :key="blog.id" class="grid grid-cols-7 gap-0 grid-cols-custom">
        <div class="px-3 py-3 border flex-1">{{ (currentPage - 1) * pageSize + index + 1 }}</div>
        <div class="px-3 py-3 border flex-1 break-words">{{ blog.category_name }}</div>
        <div class="px-3 py-3 border flex-1 break-words">{{ cleanTitle(blog.title) }}</div>
        <div class="px-3 py-3 border flex-1">{{ blog.views }}</div>
        <div class="px-3 py-3 border flex-1 break-words">{{ formatDateBlogs(blog.publish_date) }}</div>
        <div class="px-3 py-3 border flex-1 flex items-center justify-center">
          <button class= "text-white font-bold py-2 px-2 w-[40px] flex items-center justify-center"
                  :class="{ 'bg-blue-500 hover:bg-blue-600': blog.is_active, 'bg-gray-400 hover:bg-gray-500': !blog.is_active}">
            <svg v-if="blog.is_active" class="h-5 w-5" fill="#ffffff" height="24" width="24" version="1.1" viewBox="0 0 512 512" enable-background="new 0 0 512 512">
              <g>
              <path d="m494.8,241.4l-50.6-49.4c-50.1-48.9-116.9-75.8-188.2-75.8s-138.1,26.9-188.2,75.8l-50.6,49.4c-11.3,12.3-4.3,25.4 0,29.2l50.6,49.4c50.1,48.9 116.9,75.8 188.2,75.8s138.1-26.9 188.2-75.8l50.6-49.4c4-3.8 11.7-16.4 0-29.2zm-238.8,84.4c-38.5,0-69.8-31.3-69.8-69.8 0-38.5 31.3-69.8 69.8-69.8 38.5,0 69.8,31.3 69.8,69.8 0,38.5-31.3,69.8-69.8,69.8zm-195.3-69.8l35.7-34.8c27-26.4 59.8-45.2 95.7-55.4-28.2,20.1-46.6,53-46.6,90.1 0,37.1 18.4,70.1 46.6,90.1-35.9-10.2-68.7-29-95.7-55.3l-35.7-34.7zm355,34.8c-27,26.3-59.8,45.1-95.7,55.3 28.2-20.1 46.6-53 46.6-90.1 0-37.2-18.4-70.1-46.6-90.1 35.9,10.2 68.7,29 95.7,55.4l35.6,34.8-35.6,34.7z"/>
              </g>
            </svg>
            <svg v-else class="h-5 w-5"  fill="#ffffff" width="24" height="24" viewBox="0 0 512 512">
              <path fill-rule="evenodd" d="M47.0849493,2.84217094e-14 L185.740632,138.655563 C194.095501,134.657276 203.45297,132.418278 213.333333,132.418278 C248.679253,132.418278 277.333333,161.072358 277.333333,196.418278 C277.333333,206.299034 275.094157,215.656855 271.095572,224.011976 L409.751616,362.666662 L379.581717,392.836561 L320.374817,333.628896 C291.246618,353.329494 255.728838,367.084945 213.333333,367.084945 C64,367.084945 7.10542736e-15,196.418278 7.10542736e-15,196.418278 C7.10542736e-15,196.418278 22.862032,135.452859 73.1408088,86.3974274 L16.9150553,30.169894 L47.0849493,2.84217094e-14 Z M103.440016,116.694904 C74.7091717,144.512844 55.9626236,177.598744 46.7136,196.424891 C64.7370667,233.114811 119.071573,324.418278 213.333333,324.418278 C242.440012,324.418278 267.739844,315.712374 289.339919,302.595012 L240.926035,254.180993 C232.571166,258.17928 223.213696,260.418278 213.333333,260.418278 C177.987413,260.418278 149.333333,231.764198 149.333333,196.418278 C149.333333,186.537915 151.572331,177.180445 155.570618,168.825577 Z M213.333333,25.7516113 C362.666667,25.7516113 426.666667,196.418278 426.666667,196.418278 C426.666667,196.418278 412.428071,234.387867 381.712212,274.508373 L351.151213,243.941206 C364.581948,225.697449 374.142733,208.239347 379.954347,196.410385 C361.9296,159.721745 307.595093,68.418278 213.333333,68.418278 C201.495833,68.418278 190.287983,69.858232 179.702584,72.449263 L145.662385,38.4000762 C165.913597,30.494948 188.437631,25.7516113 213.333333,25.7516113 Z" transform="translate(42.667 59.582)"/>
            </svg>
          </button>
        </div>
        <div class="px-3 py-3 border flex flex-col">
          <button class="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-2 w-[40px] flex items-center justify-center" @click="blogEdit(blog.id)">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />  <line x1="13.5" y1="6.5" x2="17.5" y2="10.5" /></svg>
          </button>
          <button class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-2 w-[40px]  flex items-center justify-center">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="4" y1="7" x2="20" y2="7" />  <line x1="10" y1="11" x2="10" y2="17" />  <line x1="14" y1="11" x2="14" y2="17" />  <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />  <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
          </button>
        </div>
      </div>
    </div>   
    <Pagination :currentPage="currentPage" :totalPages="totalPages" @page-changed="fetchBlogs" />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import ApiService from '@/services/api-service.js';
import Pagination from "@/components/PageNavigationBar";
import { formatDateBlogs, isNullOrEmpty } from '@/utils/formatters';
import { useRouter } from 'vue-router';

const router = useRouter();

let totalPages = ref(1);
let currentPage = ref(1);
const pageSize = 20;
const filteredBlogs = ref([]);
const apiService = new ApiService();

const cleanTitle = (title) => {
    return isNullOrEmpty(title) ? title : title.replace(/&nbsp;/g, ' ');
  };
const fetchBlogs = async (page = 1) => {
  currentPage = page;
  try {
      const data = await apiService.fetchPartOfDataByTypeLang('IndexPaginated', 'blog', page, pageSize, 1);
      filteredBlogs.value = Array.isArray(data.items) ? data.items : []; // Убедитесь, что это массив
      totalPages = Math.ceil(data.totalPages / pageSize);
    } catch (error) {
      console.error('Error fetching data:', error);
      filteredBlogs.value = []; // Убедитесь, что это массив даже при ошибке
    }
};

const blogEdit = (id) => {
  router.push({ name: 'blog-update', params: { id: id } });
}



onMounted(() => {
  fetchBlogs();
});
</script>


<style scoped>
.grid-cols-custom {
    grid-template-columns: 4% 10% 37% 12% 18% 12% 8%;
}
  
</style>
  