<template>
  <div class="content">
    <HeadTitle :title="articleTitle" />
    <div class="date-content">
      <div class="date-header" @click="toggleDateModal">
        <h2 class="selectArticleOfDate">Выбрать статьи по дате</h2>
        <div class="additional-btn">
          <a class="down-open-click">
            <svg v-if="isShowDateModal" class="w-4 h-4 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 8">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7 7.674 1.3a.91.91 0 0 0-1.348 0L1 7" />
            </svg>
            <svg v-else class="w-4 h-4 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 8">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 5.326 5.7a.909.909 0 0 0 1.348 0L13 1" />
            </svg>
          </a>
        </div>
      </div>
      <transition name="slide">
        <div class="search-date" v-if="isShowDateModal">
          <div class="begin-date col-md-3">
            <label>C</label>
            <VueDatePicker v-model="articleStore.beginDate" placeholder="yyyy-mm-dd" :format="articleStore.dateFormat" :clearable="false"></VueDatePicker>
          </div>
          <div class="end-date col-md-3">
            <label>По</label>
            <VueDatePicker v-model="articleStore.endDate" placeholder="yyyy-mm-dd" :format="articleStore.dateFormat" :clearable="false"></VueDatePicker>
          </div>
          <div class="search-btn col-md-3">
            <label>&nbsp;</label>
            <button class="btn" @click="searchByDate">Найти</button>
          </div>
        </div>
      </transition>
    </div>
    <CreateButton />
    <div class="article">
      <div class="article-content">
        <div class="relative shadow-md sm:rounded-lg border-none w-[calc(100%-7%)]">
          <div class="table w-[100%] text-sm text-left text-gray-500 dark:text-gray-400">
            <div class="table-header text-xs text-gray-700 uppercase bg-gray-100 border-b">
              <div class="table-row border-b flex">
                <div class="table-cell px-6 py-3 flex-first">#</div>
                <div class="table-cell px-6 py-3 flex-title">Заголовок</div>
                <div class="table-cell px-6 py-3 flex-date">Дата публикации</div>
                <div class="table-cell px-6 py-3 flex-category">Категории</div>
                <div class="table-cell px-6 py-3 flex-sefname">ЧПУ</div>
                <div class="table-cell px-6 py-3 flex-active">Видимость</div>
                <div class="table-cell px-6 py-3 flex-actions">Действия</div>
              </div>
              <div class="table-row flex">
                <div class="table-cell px-6 py-4 flex-first">&nbsp;</div>
                <div class="table-cell px-6 py-4 flex-title">
                  <div class="group">
                    <input type="text" class="inputByName" v-model="articleStore.searchByName" placeholder="Search by Title" />
                  </div>
                </div>
                <div class="table-cell px-6 py-4 flex-date">
                  <input type="text" v-model="articleStore.searchByPublishDate" placeholder="Search by Date" />
                </div>
                <div class="table-cell px-6 py-4 flex-category">
                  <select class="w-[100px]" v-model="articleStore.searchByCategory">
                    <option></option>
                    <option>2023</option>
                    <option>2021</option>
                    <option>2022</option>
                    <option>2020</option>
                  </select>
                </div>
                <div class="table-cell px-6 py-4 flex-sefname">&nbsp;</div>
                <div class="table-cell px-6 py-4 flex-active">&nbsp;</div>
                <div class="table-cell px-6 py-4 flex-actions">&nbsp;</div>
              </div>
            </div>
            <div class="table-body">
              <div class="table-row bg-white border-b dark:bg-gray-800 dark:border-gray-700 flex"
                   v-for="(article, index) in paginatedArticles"
                   :key="article.id"
                   @mouseover="hover = index"
                   @mouseleave="hover = null"
                   :class="{ 'bg-gray-200': hover === index }">
                <div class="table-cell px-6 py-4 flex-first">{{ index + (currentPage - 1) * pageSize + 1 }}</div>
                <div class="table-cell px-6 py-4 flex-title">{{ cleanTitle(article.title) }}</div>
                <div class="table-cell px-6 py-4 flex-date">{{ formatDate(article.publish_date) }}</div>
                <div class="table-cell px-6 py-4 flex-category">{{ article.category }}</div>
                <div class="table-cell px-6 py-4 flex-sefname">{{ article.sefname }}</div>
                <div class="table-cell px-6 py-4 flex-active">{{ article.active }}</div>
                <div class="table-cell px-6 py-4 flex-actions">
                  <button @click="deleteItem(article.id)" class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 mr-2">
                    <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="4" y1="7" x2="20" y2="7" />  <line x1="10" y1="11" x2="10" y2="17" />  <line x1="14" y1="11" x2="14" y2="17" />  <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />  <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                  </button>
                  <button @click="goToPageById('ArticlesEdit',article.id)" class="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4">
                    <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />  <line x1="13.5" y1="6.5" x2="17.5" y2="10.5" /></svg>
                  </button>
                </div>
              </div>
            </div>
            <Pagination :currentPage="currentPage" :totalPages="totalPages" @page-changed="fetchArticles" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { ref, computed, watch, onMounted } from "vue";
  import HeadTitle from "@/components/HeadTitle.vue";
  import CreateButton from "@/components/CreateButton.vue";
  import Pagination from "@/components/PageNavigationBar.vue";
  import VueDatePicker from "@vuepic/vue-datepicker";
  import "@vuepic/vue-datepicker/dist/main.css";
  import store from "@/store";
  import ApiService from '@/services/api-service.js';
  import { formatDate, isNullOrEmpty } from '@/utils/formatters';
  import { useRouter } from 'vue-router';

  const router = useRouter();
  const apiService = new ApiService();

  const articleTitle = "Статьи";
  const articleStore = computed(() => store.state.articleStore);
  let isShowDateModal = ref(false);

  const toggleDateModal = () => {
    isShowDateModal.value = !isShowDateModal.value;
  };

  let totalPages = ref(1);
  let currentPage = ref(1);
  const pageSize = 20;
  const articles = ref([]);

  const filteredArticles = computed(() => {
    return filterArticles();
  });

  const paginatedArticles = computed(() => {
    return getPaginatedArticles();
  });

  const filterArticles = () => {
    const searchQuery = articleStore.value.searchByName.toLowerCase();
    const searchByDate = articleStore.value.searchByPublishDate;
    const searchByCategory = articleStore.value.searchByCategory;

    return articles.value.filter(article => {
      const matchesName = article.title && article.title.toLowerCase().includes(searchQuery);
      const matchesDate = !searchByDate || (article.publish_date && article.publish_date.includes(searchByDate));
      const matchesCategory = !searchByCategory || article.category.toString() === searchByCategory;

      return matchesName && matchesDate && matchesCategory;
    });
  };

  const getPaginatedArticles = () => {
    const start = (currentPage.value - 1) * pageSize;
    const end = start + pageSize;
    return filteredArticles.value.slice(start, end);
  };

  const fetchArticles = async (page = 1) => {
    currentPage.value = page;
    try {
      const data = await apiService.fetchPartOfDataByTypeLangFiltered('IndexPaginated', 'text_pages', page, pageSize, 1, '');
      articles.value = Array.isArray(data.items) ? data.items : []; // Убедитесь, что это массив
      totalPages.value = Math.ceil(data.totalPages / pageSize);

      console.error('Articles fetchArticles, totalPages:', totalPages.value, ', data=', data, ', page=', page);

    } catch (error) {
      console.error('Error fetching data:', error);
      articles.value = []; // Убедитесь, что это массив даже при ошибке
    }
  };

  watch(
    () => [
      articleStore.value.searchByName,
      articleStore.value.searchByPublishDate,
      articleStore.value.searchByCategory
    ],
    () => {
      // Фильтруем статьи и обновляем пагинацию при изменении поисковых параметров
      filterArticles();
      getPaginatedArticles();
    },
    { deep: true }
  );

  onMounted(() => {
    fetchArticles();
  });

  const searchByDate = () => {
    // Добавьте вашу логику поиска по дате
  };

  const cleanTitle = (title) => {
    return isNullOrEmpty(title) ? title : title.replace(/&nbsp;/g, ' ');
  };

  function goToPageById(url, id) {
    router.push({ name: url, params: { id: id } });
  }

  async function deleteItem(id) {
    try {
      var req = `Delete?type=text_pages&id=${id}`;

      await apiService.sendData(req, null, 'post')
        .then(response => {
          console.log("deleteItem response=", response);
        })
        .catch(error => {
          console.log("deleteItem, sendData error=", error);
        });

      articles.value = articles.value.filter(item => item.id !== id);

    } catch (error) {
      console.log("deleteItem, sendData error = ", error);
    }
  }
</script>

<style scoped>
  .flex-first {
    width: 50px; /* Задайте необходимую ширину */
    flex: 0 0 50px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-title {
    width: 250px; /* Задайте необходимую ширину */
    flex: 0 0 250px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-date {
    width: 150px; /* Задайте необходимую ширину */
    flex: 0 0 150px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-category {
    width: 200px; /* Задайте необходимую ширину */
    flex: 0 0 200px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-sefname {
    width: 150px; /* Задайте необходимую ширину */
    flex: 0 0 150px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-active {
    width: 100px; /* Задайте необходимую ширину */
    flex: 0 0 100px; /* Это фиксирует ширину в Flexbox */
  }

  .flex-actions {
    width: 200px; /* Задайте необходимую ширину */
    flex: 0 0 200px; /* Это фиксирует ширину в Flexbox */
  }

  .content {
    position: relative;
    width: 97%;
    margin-left: 20px;
  }

  .article-content {
    margin-left: 20px;
  }

  select {
    border: 2px solid blue;
  }

  .date-content {
    position: static;
    width: 93%;
    margin-bottom: 20px;
  }

  .date-header {
    display: flex;
    margin-left: 20px;
    background-color: beige;
    height: 30px;
  }

  .search-date {
    display: flex;
    margin-left: 20px;
  }

  .dp__action_select {
    color: black;
  }

  .dp__button {
    display: none;
  }

  .dp__action_cancel {
    background-color: white !important;
  }

  .dp__action_button {
    line-height: 0px;
    background-color: cornflowerblue !important;
  }

  .additional-btn {
    margin-left: auto;
  }

  .search-date {
    background-color: beige;
  }

  .begin-date,
  .end-date {
    width: 200px;
    padding-right: 20px;
  }

  .selectArticleOfDate {
    font-size: 18px;
  }

  .search-btn {
    display: grid;
    place-items: center;
    height: 10px; /* Высота div */
  }

  .btn {
    background-color: lightskyblue;
    width: 100px;
    height: 30px;
  }

    .btn:hover {
      background-color: white;
    }

  .inputByName {
    width: 250px;
    height: 30px;
    background: none;
    display: block;
    border: none;
    border-bottom: 3px solid black;
  }

  .table {
    display: flex;
    flex-direction: column;
  }

  .table-header {
    display: flex;
    flex-direction: column;
  }

  .table-body {
    display: flex;
    flex-direction: column;
  }

  .table-row {
    display: flex;
  }

  .table-cell {
    flex: 1;
  }

  .table-row:hover {
    background-color: #f3f4f6; /* Цвет выделения строки при наведении */
  }

  .pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
  }

    .pagination button {
      margin: 0 5px;
      padding: 5px 10px;
    }

      .pagination button.active {
        font-weight: bold;
        background-color: #007bff;
        color: white;
      }

      .pagination button:disabled {
        opacity: 0.5;
        cursor: not-allowed;
      }
</style>
