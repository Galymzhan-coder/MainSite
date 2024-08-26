<template>
  <div class="p-4 border">
    <button @click="addItem" class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 mb-4">
      Добавить
    </button>

    <template>
      <div class="products-index">
        <div class="page-heading">
          <h1><i class="icon-list-1"></i> {{ title }}</h1>
        </div>
        <p>
          <button @click="createFaqCategories" class="btn btn-success">
            Создать вопрос-ответ
          </button>
        </p>
        <div class="widget">
          <div class="widget-content">
            <table>
              <thead>
                <tr>
                  <th>#</th>
                  <th>Категория</th>
                  <th>Вопрос</th>
                  <th>Ответ</th>
                  <th>Активность</th>
                  <th>Действия</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(manager, index) in managers" :key="manager.id">
                  <td>{{ index + 1 }}</td>
                  <td>{{ manager.full_name }}</td>
                  <td>{{ manager.title }}</td>
                  <td>{{ manager.position }}</td>
                  <td>
                    <input type="checkbox"
                           :checked="manager.is_active"
                           @change="toggleActive(manager)" />
                  </td>
                  <td>
                    <button @click="editManager(manager.id)">Редактировать</button>
                    <button @click="deleteManager(manager.id)">Удалить</button>
                  </td>
                </tr>
              </tbody>
            </table>
            <div v-if="loading" class="loading">Загрузка...</div>
            <pagination v-if="pagination"
                        :pagination="pagination"
                        @page-changed="fetchManagers" />
          </div>
        </div>
      </div>
    </template>
        

    <div class="flex flex-col">
      <div class="grid grid-cols-6 gap-0" style="grid-template-columns: 0.5fr 6fr 2fr 2fr 2fr 0.5fr">
        <div class="bg-gray-300 p-2 border">№</div>
        <div class="bg-gray-300 p-2 border">Категория</div>
        <div class="bg-gray-300 p-2 border">Вопрос</div>
        <div class="bg-gray-300 p-2 border">Ответ</div>
        <div class="bg-gray-300 p-2 border">Активность</div>
        <div class="bg-gray-300 p-2 border">Действия</div>
      </div>

      <div v-for="(item, index) in items" :key="item.id" class="grid grid-cols-6 gap-0" style="grid-template-columns: 0.5fr 6fr 2fr 2fr 2fr">
        <div class="p-2 border">{{ index + 1 }}</div>
        <!--<div class="p-2 border">{{ item.title }}</div>-->
        <!--div class="p-2 border" :style="{ paddingLeft: (10 + (item.level * 50)) + 'px' }">{{ item.indented_title }}</!--div-->
        <div class="p-2 border" :style="{ paddingLeft: (10 + (item.level * 50)) + 'px' }" v-html="item.indented_title"></div>

        <div class="p-2 border">
          <button @click="moveUp(index)" :disabled="index === 0" class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 mr-2">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <circle cx="12" cy="12" r="9" />  <line x1="12" y1="8" x2="8" y2="12" />  <line x1="12" y1="8" x2="12" y2="16" />  <line x1="16" y1="12" x2="12" y2="8" /></svg>
          </button>
          <button @click="moveDown(index)" :disabled="index === items.length - 1" class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <circle cx="12" cy="12" r="9" />  <line x1="8" y1="12" x2="12" y2="16" />  <line x1="12" y1="8" x2="12" y2="16" />  <line x1="16" y1="12" x2="12" y2="16" /></svg>
          </button>
        </div>
        <div class="p-2 border">
          <input type="checkbox" v-model="item.is_active" @change="() => ChangeData(item)" />
        </div>
        <div class="p-2 border">
          <button @click="deleteItem(index)" class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 mr-2">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="4" y1="7" x2="20" y2="7" />  <line x1="10" y1="11" x2="10" y2="17" />  <line x1="14" y1="11" x2="14" y2="17" />  <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />  <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
          </button>
          <button @click="goToPageById('categoryEdit',item.id)" class="bg-yellow-500 hover:bg-yellow-700 text-white font-bold py-2 px-4">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />  <line x1="13.5" y1="6.5" x2="17.5" y2="10.5" /></svg>
          </button>
        </div>
      </div>
    </div>
    <categoryEdit v-if="showForm"
                  :item="selectedItem"
                  @save="saveItem"
                  @cancel="cancelForm" />


  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import ApiService from '../../services/api-service.js';

  const items = ref([]);
  const apiService = new ApiService();

  onMounted(async () => {
    try {
      //const data = await apiService.fetchData('Index');
      const data = await apiService.fetchDataByTypeLang('Index', 'category', 1); //await apiService.fetchDataByType('Index','category');

      items.value = data;//data.sort((a,b) => a.id - b.id);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  });

  function addItem() {
    items.value.push({ id: items.value.length + 1, name: `Item ${items.value.length + 1}`, active: true });
  }

  function moveUp(index) {
    if (index > 0) {
      [items.value[index - 1], items.value[index]] = [items.value[index], items.value[index - 1]];
    }
  }

  function moveDown(index) {
    if (index < items.value.length - 1) {
      [items.value[index], items.value[index + 1]] = [items.value[index + 1], items.value[index]];
    }
  }


  const selectedItem = ref(null);
  const showForm = ref(false);
  /*
  function editItem(index) {
    selectedItem.value = { ...items.value[index] };
    showForm.value = true;
  }
  */
  function saveItem(updatedItem) {
    // Сохранение обновленных данных в items
    const index = items.value.findIndex((item) => item.id === updatedItem.id);
    if (index !== -1) {
      items.value.splice(index, 1, updatedItem);
    }
    // Закрываем форму после сохранения
    showForm.value = false;
  }

  function cancelForm() {
    // Закрываем форму при отмене
    showForm.value = false;
  }

  const router = useRouter();
  /*
  function goToPage(url) {
    router.push(url);
  }
  */
  function goToPageById(url, id) {
    //router.push(`${url}/${id}`);
    //console.log('goToPageById id=', id);
    router.push({ name: url, params: { id: id } });
  }

  async function ChangeData(item) {
    try {
      console.log("ChaangeData, item = ", item);
      await apiService.sendData(`Update?type=category&id=${item.id}`, item, 'post')
    } catch (e) {
      console.log("ChaangeData, sendData error = ", e);
    }

  }

  async function deleteItem(id) {

    try {

      var req = `Delete?type=contents&id=${id}`;

      await apiService.sendData(req, null, 'post')
        .then(response => {
          console.log("saveItem response=", response);
        })
        .catch(error => {
          console.log("saveItem, sendData error=", error);
        });

      items.value = items.value.filter(item => item.id !== id);

    } catch (error) {
      console.log("saveItem, sendData error = ", error);
    }
  }
</script>

<style>
  .col-width {
    grid-template-columns: 40px 1fr 1fr 1fr 1fr; /* Пример пропорциональных ширин */
  }
</style>
